using Microsoft.AspNetCore.Mvc;
using PuntoVitaExams.API.Services;
using PuntoVitaExams.API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using PuntoVitaExams.API.Exceptions;

namespace PuntoVitaExams.API.Controllers
{
    [ApiController]
    [Route("api/examinationCommitteeSecretary")]
    [Authorize(Roles = "Admin")]
    public class ExaminationCommitteeSecretariesController : ControllerBase
    {
        private readonly IPuntoVitaExamRepository _puntovitaExamRepository;
        private readonly IMapper _mappeer;

        public ExaminationCommitteeSecretariesController(IPuntoVitaExamRepository puntoVitaExamRepository, IMapper mapper)
        {
            _puntovitaExamRepository = puntoVitaExamRepository ?? throw new ArgumentNullException(nameof(puntoVitaExamRepository));
            _mappeer = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Creates a new committee secretary.
        /// </summary>
        /// <param name="newSecretaryDto"> </param>
        /// <returns></returns>

        [HttpPost]
        public async Task<ActionResult<ExaminationCommitteeSecretaryDto>> CreateNewExaminationCommitteeSecretary(
                   ExaminationCommitteeSecretaryDto newSecretaryDto)
        {
            var newSecretaryToCreate = _mappeer.Map<Entities.ExaminationCommitteeSecretary>(newSecretaryDto);
            await _puntovitaExamRepository.CreateNewExaminationCommitteeSecretaryAsync(newSecretaryToCreate);
            var newSecretaryToReturn = _mappeer.Map<Models.ExaminationCommitteeSecretaryDto>(newSecretaryToCreate);
            return CreatedAtRoute("GetExaminationCommitteeSecretary",
                new
                {
                    lastName = newSecretaryToReturn.ExaminationCommitteeSecretaryLastName
                },
                newSecretaryToReturn);
        }


        /// <summary>
        /// Gets all committee secretaries.
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExaminationCommitteeSecretaryDto>>> GetExaminationCommitteeSecretaries()
        {
            var examinationCommitteeSecretaries = await _puntovitaExamRepository.GetExaminationCommitteeSecretariesAsync();
            return Ok(_mappeer.Map<IEnumerable<Models.ExaminationCommitteeSecretaryDto>>(examinationCommitteeSecretaries));
        }

        /// <summary>
        /// Gets a specific committee secretary.
        /// </summary>
        /// <param name="lastName"> </param>
        /// <returns></returns>

        [HttpGet("{lastName}", Name = "GetExaminationCommitteeSecretary")]
        public async Task<ActionResult<ExaminationCommitteeSecretaryDto>> GetExaminationCommitteeSecretary(string lastName)
        {
            var secretary = await _puntovitaExamRepository.GetExaminationCommitteeSecretaryAsync(lastName);
            if (secretary == null)
            {
                throw new NotFoundException($"No committee secretary named {lastName} was found");
            }
            return Ok(_mappeer.Map<Models.ExaminationCommitteeSecretaryDto>(secretary));
        }

        /// <summary>
        /// Assigns the secretary to the committee.
        /// </summary>
        /// <param name="lastName"> </param>
        /// /// <param name="examinationCommitteeId"> </param>
        /// <returns></returns>

        [HttpGet("{examinationCommitteeId}/{lastName}")]
        public async Task<ActionResult> ConnectCommiteSecretaryAndCommittee(
            int examinationCommitteeId, string lastName )

        {
            var secretary = await _puntovitaExamRepository.GetExaminationCommitteeSecretaryAsync(lastName);
            if (secretary == null)
            {
                throw new NotFoundException($"No committee secretary named {lastName} was found");
            }
            var committee = await _puntovitaExamRepository.GetExaminationCommitteeWithExamsAsync(examinationCommitteeId);
            if (committee == null)
            {
                throw new NotFoundException($"No committee secretary named {lastName} was found");
            }
            if (committee.ExaminationCommitteeSecretary != null)
            {
                throw new BadRequestException("You are trying to add the second committee secretary.");
            }
            secretary.ExaminationCommittees.Add(committee);
            await _puntovitaExamRepository.SaveChangesAsync();
            return Ok($"{secretary.ExaminationCommitteeSecretaryFirstName} {secretary.ExaminationCommitteeSecretaryLastName} został(a) wyznaczony(a) " +
                $"na sekretarza komisji Id {examinationCommitteeId}");
        }

        /// <summary>
        /// Displays a specific committee secretary with exams.
        /// </summary>
        /// <param name="lastName"> </param>
        /// <returns></returns>

        [HttpGet("committeeSecretaryWithExams")]
        public async Task<ActionResult<IEnumerable<SecretaryByExamsDto>>> GetCommitteeSecretaryWithExams(string lastName)
        {
            var committeeSecretary = await _puntovitaExamRepository.GetExaminationCommitteeSecretaryAsync(lastName);
            if (committeeSecretary == null)
            {
                throw new NotFoundException($"No examination committee secretary named {lastName} was found");
            }

            var committeeSecretaryByExams = await _puntovitaExamRepository.GetCommitteeSecretaryWithExamsAsync(lastName);
            
            return Ok(_mappeer.Map<IEnumerable<SecretaryByExamsDto>>(committeeSecretaryByExams));
        }

        /// <summary>
        /// Deletes a specific committee secretary.
        /// </summary>
        /// <param name="lastName"> </param>
        /// <returns></returns>

        [HttpDelete]
        public async Task<ActionResult> DeleteExaminationCommitteeSecretary(string lastName)
        {
            var committeeSecretary = await _puntovitaExamRepository.GetExaminationCommitteeSecretaryAsync(lastName);
            if (committeeSecretary == null)
            {
                throw new NotFoundException($"No committee secretary named  {lastName}  was found");
            }
            _puntovitaExamRepository.DeleteExaminationCommitteeSecretary(committeeSecretary);
            await _puntovitaExamRepository.SaveChangesAsync();
            return Ok($"{committeeSecretary.ExaminationCommitteeSecretaryFirstName} {committeeSecretary.ExaminationCommitteeSecretaryLastName} został(a)" +
                $" usunięty(a) z bazy danych");
        }
    }
}
