using Microsoft.EntityFrameworkCore;

namespace PuntoVitaExams.API.Entities
{
    [Keyless]
    public class HeadByExams
    {
        public string ExaminationCommitteeHeadFirstName { get; set; }

        public string ExaminationCommitteeHeadLastName { get; set; }

        public string? SailingExamNumber { get; set; }

        public DateTime SailingExamDate { get; set; }

        public string? SailingExamPlace { get; set; }
    }
}
