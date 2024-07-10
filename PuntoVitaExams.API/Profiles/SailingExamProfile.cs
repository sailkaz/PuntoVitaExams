
using AutoMapper;
using PuntoVitaExams.API.Models;

namespace PuntoVitaExams.API.Profiles
{
    public class SailingExamProfile : Profile
    {
        public SailingExamProfile() 
        {
            CreateMap<Entities.SailingExam, SailingExamDto>();
            CreateMap<SailingExamDto, Entities.SailingExam>();
        }
    }
}
