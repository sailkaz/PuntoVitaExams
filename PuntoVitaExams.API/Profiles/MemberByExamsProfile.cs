using AutoMapper;

namespace PuntoVitaExams.API.Profiles
{
    public class MemberByExamsProfile : Profile
    {
        public MemberByExamsProfile() 
        {
            CreateMap<Entities.MemberByExams, Models.MemberByExamsDto>();
        }
    }
}
