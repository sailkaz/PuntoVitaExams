using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVitaExams.API.Entities
{
    public class SailingExam
    {
        public SailingExam()
        {
            Students = new List<Student>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SailingExamId { get; set; }

        [Required]
        public string? SailingExamNumber { get; set; } = string.Empty;

        public DateTime SailingExamDate { get; set; }

        public string? SailingExamPlace { get; set; }

        public List<Student> Students { get; set; }

        public ExaminationCommittee? ExaminationCommittee { get; set; }
    }
}