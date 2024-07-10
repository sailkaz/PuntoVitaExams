using AutoMapper;

namespace PuntoVitaExams.API.Profiles
{
    public class ExaminationCommitteeProfile : Profile
    {
        public ExaminationCommitteeProfile() 
        {
            CreateMap<Entities.ExaminationCommittee, Models.ExaminationCommitteeDto>();
        }
    }
}
