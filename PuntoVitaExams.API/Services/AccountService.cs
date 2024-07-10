using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PuntoVitaExams.API.DbContexts;
using PuntoVitaExams.API.Entities;
using PuntoVitaExams.API.Exceptions;
using PuntoVitaExams.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Reflection.Metadata;

namespace PuntoVitaExams.API.Services
{
    public interface IAccountService
    {
        void RegisterUser(UserDto dto);
        void RegisterManager(RegisterManagerDto dto);
        Task<LoginResponseDto> LoginAsync(LoginDto dto);
        string GetMyName();
        string GetMyRole();
        //Task DeleteAll(string rawUserEmail);
        Task<LoginResponseDto> RefreshTokenAsync();
        Task<User> GetUser(string email);
        Task<IEnumerable<User>> GetManagersAsync();
        void DeleteUsersAsync();
        void DeleteUserByEmail(User user);
        Task<bool> SaveChangesAsync();
    }


    public class AccountService : IAccountService
    {
        private readonly ExamContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(ExamContext context,

            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public void RegisterUser(UserDto dto)
        {
            var newUser = new User()
            {
                Name = dto.Name,
                Email = dto.Email,
                RoleId = 1
            };

            CreatePasswordHash(dto.Password, out byte[] passwordHash, out byte[] passwordSalt); 

            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;

            var newrefreshToken = CreateRefreshToken();

            newUser.RefreshToken = newrefreshToken.Token;

            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        public void RegisterManager(RegisterManagerDto dto)
        {
            var newUser = new User()
            {
                Name = dto.Name,
                Email = dto.Email,
                RoleId = dto.RoleId
            };

            CreatePasswordHash(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;

            var newrefreshToken = CreateRefreshToken();

            newUser.RefreshToken = newrefreshToken.Token;

            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        public string GetMyName()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result;
        }

        public string GetMyRole()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            }
            return result;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
        {

            var user = await _context.Users
                 .Include(u => u.Role)
                 .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user is null)
            {
                throw new BadRequestException("Invalid username or password");
            }

            if (!VerifyPasswordHash(dto.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new BadRequestException("Invalid username or password");
            }

            string token = CreateToken(user);

            var refreshToken = CreateRefreshToken();

            await SetRefreshToken(refreshToken, user);

            return new LoginResponseDto
            {
                Token = token,
                RefreshToken = refreshToken.Token,
                TokenExpires = refreshToken.Expires,
                Success = true
            };
        }


        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var authenticationSettings = new AuthenticationSettings();

            _configuration.GetSection("Authentication").Bind(authenticationSettings);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var expires = DateTime.Now.AddHours(authenticationSettings.JwtExpireHours);

            var token = new JwtSecurityToken(authenticationSettings.JwtIssuer,
                authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: creds);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        private static RefreshToken CreateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        public async Task<LoginResponseDto> RefreshTokenAsync()
        {
            var refreshToken = _httpContextAccessor?.HttpContext?.Request.Cookies["refreshToken"];
            var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user == null)
            {
                throw new BadRequestException("Invalid username or password");
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                throw new BadRequestException("Invalid username or password");
            }

            string token = CreateToken(user);

            var newRefreshToken = CreateRefreshToken();

            await SetRefreshToken(newRefreshToken, user);

            return new LoginResponseDto
            {
                Success = true,
                Token = token,
                RefreshToken = newRefreshToken.Token,
                TokenExpires = newRefreshToken.Expires
            };
        }

        private async Task SetRefreshToken(RefreshToken refreshToken, User user)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.Expires,
            };
            _httpContextAccessor?.HttpContext?.Response
                .Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);

            user.RefreshToken = refreshToken.Token;
            user.TokenCreated = refreshToken.Created;
            user.TokenExpires = refreshToken.Expires;

            await _context.SaveChangesAsync();
        }


        public async Task<User> GetUser(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<User>> GetManagersAsync()
        {
            return await _context.Users.Where(u => u.RoleId == 2).ToListAsync();
        }

        public async void DeleteUsersAsync()
        {
            await _context.Database.ExecuteSqlRawAsync("pDeleteUsersWithRoleId1");
        }

        public void DeleteUserByEmail(User user)
        {
            _context.Users.Remove(user);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }




    }
}
