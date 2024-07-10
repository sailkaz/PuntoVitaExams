using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PuntoVitaExams.API.Entities;
using PuntoVitaExams.API.Models;
using PuntoVitaExams.API.Services;

namespace PuntoVitaExams.API.Controllers
{
    [ApiController]
    [Route("api/address")]
    public class AddressesController : ControllerBase
    {
        private readonly IPuntoVitaExamRepository _puntovitaExamRepository;
        private readonly IMapper _mapper;

        public AddressesController(IPuntoVitaExamRepository puntoVitaExamRepository, IMapper mapper)
        {
            _puntovitaExamRepository = puntoVitaExamRepository ?? throw new ArgumentNullException(nameof(puntoVitaExamRepository));
            _mapper = mapper?? throw new ArgumentNullException(nameof(mapper)); 
        }
        


        //[HttpPost]
        //public async Task<ActionResult<Address>> CreateNewAddressWithStudentTogether( StudentDto newStudentDto
        //    /*string firstName, string lastName, string pesel, string street, string streetNumber,
        //    int flatNumber, string postalCode, string city*/)
        //{
        //    var newStudentToCreate = _mapper.Map<Entities.Student>(newStudentDto);
        //    await _puntovitaExamRepository.CreateNewAddressWithStudentTogetherAsync(newStudentToCreate);
        //    await _puntovitaExamRepository.SaveChangesAsync();


            //var newAddress = new Address
            //{
            //    Street = street,
            //    StreetNumber = streetNumber,
            //    FlatNumber = flatNumber,
            //    PostalCode = postalCode,
            //    City = city
            //};
            //var newStudent = new Student()
            //{
            //    FirstName = firstName,
            //    LastName = lastName,
            //    Pesel = pesel
            //};
            // await _puntovitaExamRepository.CreateNewAddressWithStudentTogetherAsync(newAddress, newStudent);
            //return Ok();
        }

    
}
