using Microsoft.Build.Framework;

namespace PuntoVitaExams.API.Models
{
    public class StudentForAddingExamResultDto
    {
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; } = string.Empty;
        
        public string? Pesel { get; set; }
       
        public string? Result { get; set; }
    }
}
