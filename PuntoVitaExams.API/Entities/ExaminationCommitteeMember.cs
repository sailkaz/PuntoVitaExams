using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVitaExams.API.Entities
{
    public class ExaminationCommitteeMember
    {
        public ExaminationCommitteeMember() 
        {
            ExaminationCommittees = new List<ExaminationCommittee>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int ExaminationCommitteeMemberId { get; set; }

        public string? ExaminationCommitteeMemberFirstName { get; set; }

        [Required]
        public string? ExaminationCommitteeMemberLastName { get; set; } = string.Empty;
        
                
        public List<ExaminationCommittee> ExaminationCommittees { get; set; }
    }
}