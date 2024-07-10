using Microsoft.Build.Framework;

namespace PuntoVitaExams.API.Models
{
    public class SailingExamDto
    {
        public SailingExamDto()
        {
            Students = new List<StudentDto>();
        }
        //public int SailingExamId { get; set; }

        public string? SailingExamNumber { get; set; } = string.Empty;

        public DateTime SailingExamDate { get; set; }

        public string? SailingExamPlace { get; set; }

        public List<StudentDto> Students { get; set; }

        public ExaminationCommitteeDto? ExaminationCommittee { get; set; }

    }
}