using Microsoft.AspNetCore.Mvc;
using PuntoVitaExams.API.DbContexts;
using PuntoVitaExams.API.Entities;
using PuntoVitaExams.API.Models;
using PuntoVitaExams.API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using PuntoVitaExams.API.Exceptions;
using Azure.Core;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace PuntoVitaExams.API.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost("registerUser")]
        public ActionResult RegisterUser([FromBody] UserDto dto)
        {
            _accountService.RegisterUser(dto);
            return Ok("User has been registered");
        }

        [HttpPost("registerManager")]
        [Authorize(Roles = "Admin")]
        public ActionResult RegisterManager([FromBody] RegisterManagerDto dto)
        {
            _accountService.RegisterManager(dto);
            return Ok("Manager has been registered");
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto dto)
        {
            var loginResult = await _accountService.LoginAsync(dto);

            if (!loginResult.Success)

                throw new BadRequestException("Invalid username or password");

            return Ok(loginResult);

        }

        [HttpGet("logged")]
        [Authorize]
        public ActionResult GetMe()
        {
            var userName = _accountService.GetMyName();
            return Ok(userName);
        }

        [HttpGet("loggedRole")]
        [Authorize]
        public ActionResult GetRole()
        {
            var userName = _accountService.GetMyRole();
            return Ok(userName);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            return NoContent();
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var response = await _accountService.RefreshTokenAsync();
            if (response.Success)
                return Ok(response);

            throw new BadRequestException("Invalid username or password");;
        }

        /// <summary>
        /// Gets all managers.
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UserToDisplayDto>>> GetManagers()
        {
            var users = await _accountService.GetManagersAsync();
            return Ok(_mapper.Map<IEnumerable<UserToDisplayDto>>(users));
        }

        /// <summary>
        /// Deletes all users.
        /// </summary>
        /// <returns></returns>

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public ActionResult<IQueryable<User>> DeleteAllUsers()
        {
            _accountService.DeleteUsersAsync();
            return Ok("Users have been deleted");
        }


        /// <summary>
        /// Deletes a specific user, manager or admin.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>


        [HttpDelete("{email}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteUserByEmail(string email)
        {
            var user = await _accountService.GetUser(email);
            if (user == null)
            {
                throw new BadRequestException("Invalid username or password");
            }
            _accountService.DeleteUserByEmail(user);
            await _accountService.SaveChangesAsync();
            return Ok("User has been deleted");

        }
    }
}


