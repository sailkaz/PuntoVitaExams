using AutoMapper;

namespace PuntoVitaExams.API.Profiles
{
    public class ExaminationCommitteeSecretaryProfile : Profile
    {
        public ExaminationCommitteeSecretaryProfile() 
        {
            CreateMap<Entities.ExaminationCommitteeSecretary, Models.ExaminationCommitteeSecretaryDto>();
            CreateMap<Models.ExaminationCommitteeSecretaryDto, Entities.ExaminationCommitteeSecretary>();
        }
    }
}
