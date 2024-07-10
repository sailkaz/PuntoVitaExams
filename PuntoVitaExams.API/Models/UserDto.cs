using Microsoft.Build.Framework;

namespace PuntoVitaExams.API.Models
{
    public class UserDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
       
    }
}