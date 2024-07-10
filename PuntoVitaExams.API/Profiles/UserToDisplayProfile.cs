using AutoMapper;

namespace PuntoVitaExams.API.Profiles
{
    public class UserToDisplayProfile : Profile
    {
        public UserToDisplayProfile() 
        {
            CreateMap<Entities.User, Models.UserToDisplayDto>();
        }
    }
}
