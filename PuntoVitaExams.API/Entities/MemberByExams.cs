using Microsoft.EntityFrameworkCore;

namespace PuntoVitaExams.API.Entities
{
    [Keyless]
    public class MemberByExams
    {
        public string ExaminationCommitteeMemberFirstName { get; set; }

        public string ExaminationCommitteeMemberLastName { get; set; }

        public string? SailingExamNumber { get; set; }

        public DateTime SailingExamDate { get; set; }

        public string? SailingExamPlace { get; set; }
    }
}
