using AutoMapper;

namespace PuntoVitaExams.API.Profiles
{
    public class ExaminationCommitteeForCreationProfile : Profile
    {
        public ExaminationCommitteeForCreationProfile() 
        {
            CreateMap<Models.ExaminationCommitteeForCreationDto, Entities.ExaminationCommittee>();
        }

    }
}
