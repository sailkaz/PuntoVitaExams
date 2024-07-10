using AutoMapper;

namespace PuntoVitaExams.API.Profiles
{
    public class HeadByExamsProfile : Profile
    {
        public HeadByExamsProfile() 
        {
            CreateMap<Entities.HeadByExams, Models.HeadByExamsDto>();
        }
    }
}
