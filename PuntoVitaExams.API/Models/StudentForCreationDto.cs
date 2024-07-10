using Microsoft.Build.Framework;

namespace PuntoVitaExams.API.Models
{
    public class StudentForCreationDto
    {
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; } = string.Empty;

        public string? Pesel { get; set; }

        public AddressDto? Address { get; set; }



    }
}
