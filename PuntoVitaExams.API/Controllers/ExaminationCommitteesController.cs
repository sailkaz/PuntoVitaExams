using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PuntoVitaExams.API.Entities;
using PuntoVitaExams.API.Exceptions;
using PuntoVitaExams.API.Models;
using PuntoVitaExams.API.Services;

namespace PuntoVitaExams.API.Controllers
{
    /// <summary>
    /// Contains methods managing examination committees
    /// </summary>
    [ApiController]
    [Route("api/examinationCommittee")]
    [Authorize(Roles = "Admin")]
    public class ExaminationCommitteesController : ControllerBase

    {
        private readonly IPuntoVitaExamRepository _puntoVitaExamRepository;
        private readonly IMapper _mapper;

        public ExaminationCommitteesController(IPuntoVitaExamRepository puntoVitaExamRepository, IMapper mapper)
        {
            _puntoVitaExamRepository = puntoVitaExamRepository ?? throw new ArgumentNullException(nameof(puntoVitaExamRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Creates a new examination committee.
        /// </summary>
        /// <param name="newCommitteeDto"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<ActionResult<ExaminationCommitteeForCreationDto>> CreateNewExaminationCommittee(
                    ExaminationCommitteeForCreationDto newCommitteeDto)
        {
            var newCommitteeToCreate = _mapper.Map<Entities.ExaminationCommittee>(newCommitteeDto);
            await _puntoVitaExamRepository.CreateNewExaminationCommitteeAsync(newCommitteeToCreate);
            return Ok();
        }

        /// <summary>
        /// Gets all examination committees
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExaminationCommitteeDto>>> GetExaminationCommittees()
        {
            var examinationCommittees = await _puntoVitaExamRepository.GetExaminationCommitteesAsync();
            return Ok(_mapper.Map<IEnumerable<ExaminationCommitteeDto>>(examinationCommittees));
        }

        /// <summary>
        /// Gets a specific examinaiton committee.
        /// </summary>
        /// <param name="examinationCommitteeId"></param>
        /// <returns></returns>
        
        [HttpGet("{examinationCommitteeId}")]
        public async Task<ActionResult<ExaminationCommitteeDto>> GetExaminationCommittee(int examinationCommitteeId)
        {
            var examinationCommittee = await _puntoVitaExamRepository.GetExaminationCommitteeWithExamsAsync(examinationCommitteeId);
            if (examinationCommittee == null)
            {
                throw new NotFoundException($"No examination committee with id {examinationCommitteeId} was found");
            }

            else if (examinationCommittee.ExaminationCommitteeHead == null && examinationCommittee.ExaminationCommitteeSecretary == null)
            {
                throw new NotFoundException($"The examination committee with id {examinationCommitteeId} is not staffed");
            }
            return Ok(_mapper.Map<ExaminationCommitteeDto>(examinationCommittee));
        }

        /// <summary>
        /// Deletes a specific examination committee.
        /// </summary>
        /// <param name="examinationCommitteeId"></param>
        /// <returns></returns>

        [HttpDelete("{examinationCommitteeId}")]
        public async Task<ActionResult> DeleteExaminationCommittee(int examinationCommitteeId)
        {
            var examinationCommittee = await _puntoVitaExamRepository.GetExaminationCommitteeWithExamsAsync(examinationCommitteeId);

            if (examinationCommittee == null)
            {
                throw new NotFoundException($"No examination committee with id {examinationCommitteeId} was found");
            }
            _puntoVitaExamRepository.DeleteExaminationCommittee(examinationCommittee);
            await _puntoVitaExamRepository.SaveChangesAsync();
            return Ok($"Komisja Id = {examinationCommitteeId} została usunięta z bazy danych");
        }

    }
}
