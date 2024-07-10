namespace PuntoVitaExams.API.Models
{
    public class LoginResponseDto
    {
        public bool Success { get; set; } = false;
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenExpires { get; set; }
    }
}
