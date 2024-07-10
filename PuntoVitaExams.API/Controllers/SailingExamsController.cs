using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PuntoVitaExams.API.Entities;
using PuntoVitaExams.API.Exceptions;
using PuntoVitaExams.API.Models;
using PuntoVitaExams.API.Services;

namespace PuntoVitaExams.API.Controllers
{
    [ApiController]
    [Route("api/sailingExams")]
    public class SailingExamsController : ControllerBase
    {
        private readonly IPuntoVitaExamRepository _puntovitaExamRepository;
        private readonly IMapper _mapper;


        public SailingExamsController(IPuntoVitaExamRepository puntoVitaExamRepository, IMapper mapper)
        {
            _puntovitaExamRepository = puntoVitaExamRepository ?? throw new ArgumentNullException(nameof(puntoVitaExamRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Creates a new exam.
        /// </summary>
        /// <param name="newExamDto"></param>
        /// <returns></returns>

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SailingExamForCreationDto>> CreateNewSailingExam(SailingExamForCreationDto newExamDto)
        {
            var newExamToCreate = _mapper.Map<SailingExam>(newExamDto);
            await _puntovitaExamRepository.CreateNewSailingExamAsync(newExamToCreate);
            var newExamToReturn = _mapper.Map<Models.SailingExamForCreationDto>(newExamToCreate);
            return CreatedAtRoute("GetExamWithStudentsAndCommittee",
                new
                {
                    sailingExamNumber = newExamToReturn.SailingExamNumber
                },
                newExamToReturn);
        }

        /// <summary>
        /// Gets a specific exam with students.
        /// </summary>
        /// <param name="sailingExamNumber"></param>
        /// <returns></returns>

        [HttpGet("{sailingExamNumber}", Name = "GetExamWithStudentsAndCommittee")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> GetExamWithStudentsAndCommittee(string sailingExamNumber)
        {
            if (!await _puntovitaExamRepository.ExamExistsAsync(sailingExamNumber))
            {
                throw new NotFoundException($"No exam with number {sailingExamNumber} was found");
            }
            var examEntity = await _puntovitaExamRepository.GetExamWithStudentsAndCommitteeAsync(sailingExamNumber);
            if (examEntity.Students.IsNullOrEmpty())
            {
                throw new NotFoundException($"Exam with number {sailingExamNumber} has no students or no committee");
            }
            return Ok(_mapper.Map<SailingExamDto>(examEntity));
        }

        /// <summary>
        /// Assigns the committee to the exam.
        /// </summary>
        /// <param name="sailingExamNumber"></param>
        /// <param name="examinationCommitteeId"></param>
        /// <returns></returns>

        [HttpGet("{examinationCommitteeId}/{sailingExamNumber}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> ConnectExamWithCommittee(int examinationCommitteeId, string sailingExamNumber)
        {
            var exam = await _puntovitaExamRepository.GetExamWithCommitteeAsync(sailingExamNumber);
            if (exam == null)
            {
                throw new NotFoundException($"No exam with number {sailingExamNumber} was found");
            }
            if (exam.ExaminationCommittee != null)
            {
                throw new BadRequestException("You are trying to add the second committee to this exam.");
            }
            var committee = await _puntovitaExamRepository.GetExaminationCommitteeWithExamsAsync(examinationCommitteeId);
            if (committee == null)
            {
                throw new NotFoundException($"No examination committee with id {examinationCommitteeId} was found");
            }
            committee.SailingExams.Add(exam);
            await _puntovitaExamRepository.SaveChangesAsync();
            return Ok($"Komisja o id = {examinationCommitteeId} została przypisana do egzaminu nr {sailingExamNumber}");
        }

        /// <summary>
        /// Deletes a specific exam.
        /// </summary>
        /// <param name="sailingExamNumber"></param>
        /// <returns></returns>

        [HttpDelete("{sailingExamNumber}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteSailingExam(string sailingExamNumber)
        {
            var exam = await _puntovitaExamRepository.GetExamAsync(sailingExamNumber);
            if (exam == null)
            {
                throw new NotFoundException($"No exam with number {sailingExamNumber} was found");
            }
            _puntovitaExamRepository.DeleteSailingExam(exam);
            await _puntovitaExamRepository.SaveChangesAsync();
            return Ok($"Egzamin nr {sailingExamNumber} został usunięty z bazy danych");
        }

    }

}

