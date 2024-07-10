using Microsoft.Build.Framework;

namespace PuntoVitaExams.API.Models
{
    public class ExaminationCommitteeMemberDto
    {
        //public ExaminationCommitteeMemberDto()
        //{
        //    ExaminationCommittees = new List<ExaminationCommitteeDto>();
        //}

        //public int ExaminationCommitteeMemberId { get; set; }
        public string? ExaminationCommitteeMemberFirstName { get; set; }

        [Required]
        public string? ExaminationCommitteeMemberLastName { get; set; } = string.Empty;


        //public List<ExaminationCommitteeDto> ExaminationCommittees { get; set; }
    }
}