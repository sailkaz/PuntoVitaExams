using AutoMapper;

namespace PuntoVitaExams.API.Profiles
{
    public class ExaminationCommitteeMemberProfile : Profile
    {
        public ExaminationCommitteeMemberProfile() 
        {
            CreateMap<Entities.ExaminationCommitteeMember, Models.ExaminationCommitteeMemberDto>();
            CreateMap<Models.ExaminationCommitteeMemberDto, Entities.ExaminationCommitteeMember>();
        }
    }
}
