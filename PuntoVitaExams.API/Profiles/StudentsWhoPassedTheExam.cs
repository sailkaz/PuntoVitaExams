using AutoMapper;

namespace PuntoVitaExams.API.Profiles
{
    public class StudentsWhoPassedTheExam : Profile
    {
        public StudentsWhoPassedTheExam()
        {
            CreateMap<Entities.StudentsWhoPassedTheExam, Models.StudentsWhoPassedTheExamDto>();
        }
    }
}
