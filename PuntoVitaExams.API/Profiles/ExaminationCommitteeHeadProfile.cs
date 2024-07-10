using AutoMapper;

namespace PuntoVitaExams.API.Profiles
{
    public class ExaminationCommitteeHeadProfile : Profile
    {
        public ExaminationCommitteeHeadProfile()
        {
            CreateMap<Entities.ExaminationCommitteeHead, Models.ExaminationCommitteeHeadDto>();
            CreateMap<Models.ExaminationCommitteeHeadDto, Entities.ExaminationCommitteeHead>();
        }
    }
}
