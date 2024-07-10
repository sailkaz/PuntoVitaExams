using Microsoft.Build.Framework;

namespace PuntoVitaExams.API.Models
{
    public class StudentDto
    {
        public StudentDto()
        {
            SailingExams = new List<SailingExamDto>();
        }

        public int StudentId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; } = string.Empty;

        public string? Pesel { get; set; }

        public string? Result { get; set; }

        public AddressDto Address { get; set; }

        public List<SailingExamDto> SailingExams { get; set; }
    }
}

