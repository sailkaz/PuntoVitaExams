using AutoMapper;

namespace PuntoVitaExams.API.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile() 
        {
            CreateMap<Entities.Student, Models.StudentDto>();
            CreateMap<Models.StudentDto, Entities.Student>();
            CreateMap<Entities.Student, Models.StudentForAddingExamResultDto>();
            CreateMap<Models.StudentForAddingExamResultDto, Entities.Student>();
        }
    }
}
