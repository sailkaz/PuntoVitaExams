using Microsoft.Build.Framework;

namespace PuntoVitaExams.API.Models
{
    public class ExaminationCommitteeSecretaryDto
    {
        //public ExaminationCommitteeSecretaryDto()
        //{
        //    ExaminationCommittees = new List<ExaminationCommitteeDto>();
        //}


        //public int ExaminationCommitteeSecretaryId { get; set; }
        public string? ExaminationCommitteeSecretaryFirstName { get; set; }

        [Required]
        public string? ExaminationCommitteeSecretaryLastName { get; set; } = string.Empty;


       // public List<ExaminationCommitteeDto> ExaminationCommittees { get; set; }
    }
}