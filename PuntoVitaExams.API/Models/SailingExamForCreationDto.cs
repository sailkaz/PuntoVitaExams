using Microsoft.Build.Framework;

namespace PuntoVitaExams.API.Models
{
    public class SailingExamForCreationDto
    {
        [Required]
        public string? SailingExamNumber { get; set; } = string.Empty;
        public DateTime SailingExamDate { get; set; }
        public string? SailingExamPlace { get; set; }
    }
}
