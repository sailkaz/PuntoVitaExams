using Microsoft.EntityFrameworkCore;

namespace PuntoVitaExams.API.Entities
{
    [Keyless]
    public class SecretaryByExams
    {
        public string ExaminationCommitteeSecretaryFirstName { get; set; }

        public string ExaminationCommitteeSecretaryLastName { get; set; }

        public string? SailingExamNumber { get; set; }

        public DateTime SailingExamDate { get; set; }

        public string? SailingExamPlace { get; set; }
    }
}
