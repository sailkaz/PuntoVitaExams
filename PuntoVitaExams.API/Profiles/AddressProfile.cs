using AutoMapper;
using PuntoVitaExams.API.Models;

namespace PuntoVitaExams.API.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile() 
        {
            CreateMap<Entities.Address, Models.AddressDto>();
            CreateMap<Models.AddressDto, Entities.Address>();
        }
    }
}
