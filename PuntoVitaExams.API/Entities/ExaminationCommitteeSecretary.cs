using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVitaExams.API.Entities
{
    public class ExaminationCommitteeSecretary
    {
        public ExaminationCommitteeSecretary() 
        {
            ExaminationCommittees = new List<ExaminationCommittee>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExaminationCommitteeSecretaryId { get; set; }
        public string? ExaminationCommitteeSecretaryFirstName { get; set; }

        [Required]
        public string? ExaminationCommitteeSecretaryLastName { get; set; } = string.Empty;
        
       
        public List<ExaminationCommittee> ExaminationCommittees { get; set; }
    }
}
