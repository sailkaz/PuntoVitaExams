using AutoMapper;

namespace PuntoVitaExams.API.Profiles
{
    public class StudentForCreationProfile : Profile
    {
        public StudentForCreationProfile()
        {
            CreateMap<Models.StudentForCreationDto, Entities.Student>();
            CreateMap<Entities.Student, Models.StudentForCreationDto>();
            CreateMap<Models.StudentForCreationDto, Entities.Address>();
        }


    }
}
