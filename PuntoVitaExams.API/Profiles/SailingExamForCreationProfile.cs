using AutoMapper;

namespace PuntoVitaExams.API.Profiles
{
    public class SailingExamForCreationProfile : Profile
    {
        public SailingExamForCreationProfile() 
        {
            CreateMap<Models.SailingExamForCreationDto, Entities.SailingExam>();
            CreateMap<Entities.SailingExam, Models.SailingExamForCreationDto>();
        }
    }
}
