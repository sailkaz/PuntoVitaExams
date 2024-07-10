using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVitaExams.API.Entities
{
    public class ExaminationCommittee
    {
        public ExaminationCommittee()
        {
            SailingExams = new List<SailingExam>();
            ExaminationCommitteeMembers = new List<ExaminationCommitteeMember>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExaminationCommitteeId { get; set; }

        public ExaminationCommitteeHead? ExaminationCommitteeHead { get; set; }

        public ExaminationCommitteeSecretary? ExaminationCommitteeSecretary { get; set; }

        public List<ExaminationCommitteeMember> ExaminationCommitteeMembers { get; set; }

        public List<SailingExam> SailingExams { get; set; }

    }
}