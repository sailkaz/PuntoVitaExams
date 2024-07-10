using Microsoft.Build.Framework;

namespace PuntoVitaExams.API.Models
{
    public class RegisterManagerDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        [Required]
        public int RoleId { get; set; } = 1;
    }
}