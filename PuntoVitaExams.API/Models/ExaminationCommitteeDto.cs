namespace PuntoVitaExams.API.Models
{
    public class ExaminationCommitteeDto
    {
        public ExaminationCommitteeDto()
        {
            SailingExams = new List<SailingExamDto>();
            ExaminationCommitteeMembers = new List<ExaminationCommitteeMemberDto>();
        }

        public int ExaminationCommitteeId { get; set; }
        public ExaminationCommitteeHeadDto? ExaminationCommitteeHead { get; set; }
        public ExaminationCommitteeSecretaryDto? ExaminationCommitteeSecretary { get; set; }

        public List<ExaminationCommitteeMemberDto> ExaminationCommitteeMembers { get; set; }

        public List<SailingExamDto> SailingExams { get; set; }

    }
}
