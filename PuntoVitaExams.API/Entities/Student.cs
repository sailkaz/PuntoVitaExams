using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuntoVitaExams.API.Entities
{
    public class Student

    {
        
        public Student()
        {
            SailingExams = new List<SailingExam>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }

        public string? FirstName { get; set; }
        [Required]
        public string LastName { get; set; } = string.Empty;

        public string? Pesel { get; set; }

        public string? Result { get; set; }

        
        public Address? Address { get; set; }
        
        public List<SailingExam> SailingExams { get; set; }
    }
}
