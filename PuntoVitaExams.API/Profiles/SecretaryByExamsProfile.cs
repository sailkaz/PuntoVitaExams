using AutoMapper;

namespace PuntoVitaExams.API.Profiles
{
    public class SecretaryByExamsProfile : Profile
    {
        public SecretaryByExamsProfile() 
        {
            CreateMap<Entities.SecretaryByExams, Models.SecretaryByExamsDto>();
        }
    }
}
