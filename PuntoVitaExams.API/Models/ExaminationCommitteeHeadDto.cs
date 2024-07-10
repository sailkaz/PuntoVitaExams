using Microsoft.Build.Framework;

namespace PuntoVitaExams.API.Models
{
    public class ExaminationCommitteeHeadDto
    {
        //public ExaminationCommitteeHeadDto()
        //{
        //    ExaminationCommittees = new List<ExaminationCommitteeDto>();
        //}

        //public int ExaminationCommitteeHeadId { get; set; }
        public string? ExaminationCommitteeHeadFirstName { get; set; }

        [Required]
        public string? ExaminationCommitteeHeadLastName { get; set; } = string.Empty;

        //public List<ExaminationCommitteeDto> ExaminationCommittees { get; set; }
    }
}