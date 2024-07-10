using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVitaExams.API.Entities
{
    public class ExaminationCommitteeHead
    {
        public ExaminationCommitteeHead() 
        {
            ExaminationCommittees = new List<ExaminationCommittee>();

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExaminationCommitteeHeadId { get; set; }

        public string? ExaminationCommitteeHeadFirstName { get;set; }

        [Required]
        public string? ExaminationCommitteeHeadLastName { get;set; } = string.Empty;


        public List<ExaminationCommittee> ExaminationCommittees { get;set; }

    }
}
