using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using PuntoVitaExams.API.Exceptions;
using PuntoVitaExams.API.Models;
using PuntoVitaExams.API.Services;

namespace PuntoVitaExams.API.Controllers
{
    [ApiController]
    [Route("api/examinationCommitteeHead")]
    [Authorize(Roles = "Admin")]
    public class ExaminationCommitteeHeadsController : ControllerBase
    {
        private readonly IPuntoVitaExamRepository _puntovitaExamRepository;
        private readonly IMapper _mapper;

        public ExaminationCommitteeHeadsController(IPuntoVitaExamRepository puntoVitaExamRepository, IMapper mapper)
        {
            _puntovitaExamRepository = puntoVitaExamRepository ?? throw new ArgumentNullException(nameof(puntoVitaExamRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Creates a new committee head.
        /// </summary>
        /// <param name="newHeadDto"> </param>
        /// <returns></returns>

        [HttpPost]
        public async Task<ActionResult<ExaminationCommitteeHeadDto>> CreateNewExaminationCommitteeHead(
                   ExaminationCommitteeHeadDto newHeadDto)
        {
            var newHeadToCreate = _mapper.Map<Entities.ExaminationCommitteeHead>(newHeadDto);
            await _puntovitaExamRepository.CreateNewExaminationCommitteeHeadAsync(newHeadToCreate);
            var newHeadToReturn = _mapper.Map<Models.ExaminationCommitteeHeadDto>(newHeadToCreate);
            return CreatedAtRoute("GetExaminationCommitteeHead",
                new
                {
                    lastName = newHeadToReturn.ExaminationCommitteeHeadLastName
                },
                newHeadToReturn);
        }

        /// <summary>
        /// Gets all committee heads.
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExaminationCommitteeHeadDto>>> GetExaminationCommitteeHeads()
        {
            var examinationCommitteeHeads = await _puntovitaExamRepository.GetExaminationCommitteeHeadsAsync();
            return Ok(_mapper.Map<IEnumerable<ExaminationCommitteeHeadDto>>(examinationCommitteeHeads));
        }

        /// <summary>
        /// Gets a specific committee head.
        /// </summary>
        /// <param name="lastName"> </param>
        /// <returns></returns>

        [HttpGet("{lastName}", Name = "GetExaminationCommitteeHead")]
        public async Task<ActionResult> GetExaminationCommitteeHead(string lastName)
        {
            var examinationCommitteeHead = await _puntovitaExamRepository.GetExaminationCommitteeHeadAsync(lastName);
            if (examinationCommitteeHead == null)
            {
                throw new NotFoundException($"No committee Head named {lastName} was found");
            }
            return Ok(_mapper.Map<ExaminationCommitteeHeadDto>(examinationCommitteeHead));
        }

        /// <summary>
        /// Assigns the head to the committee.
        /// </summary>
        /// <param name="lastName"> </param>
        /// <param name="examinationCommitteeId"> </param>
        /// <returns></returns>

        [HttpGet("{examinationCommitteeId}/{lastName}")]
        public async Task<ActionResult> ConnectCommitteeHeadAndCommittee(
            int examinationCommitteeId, string lastName)

        {
            var head = await _puntovitaExamRepository.GetExaminationCommitteeHeadAsync(lastName);
            if (head == null)
            {
                throw new NotFoundException($"No committee Head named {lastName} was found");
            }
            var committee = await _puntovitaExamRepository.GetExaminationCommitteeWithExamsAsync(examinationCommitteeId);
            if (committee == null)
            {
                throw new NotFoundException($"No examination committee with id {examinationCommitteeId} was found");
            }
            if (committee.ExaminationCommitteeHead != null)
            {
                throw new BadRequestException("You are trying to add the second committee head.");
            }
            head.ExaminationCommittees.Add(committee);
            await _puntovitaExamRepository.SaveChangesAsync();
            return Ok($"{head.ExaminationCommitteeHeadFirstName} {head.ExaminationCommitteeHeadLastName} został(a) wyznaczony(a) " +
                $"na przewodniczącego(ą) komisji Id {examinationCommitteeId}");
        }

        /// <summary>
        /// Displays a specific committee head with exams.
        /// </summary>
        /// <param name="lastName"> </param>
        /// <returns></returns>

        [HttpGet("committeeHeadWithExams")]
        public async Task<ActionResult<IEnumerable<HeadByExamsDto>>> GetCommitteeHeadWithExams(string lastName)
        {
            var committeeHead = await _puntovitaExamRepository.GetExaminationCommitteeHeadAsync(lastName);
            if (committeeHead == null)
            {
                throw new NotFoundException($"No examination committee head named {lastName} was found");
            }

            var committeeHeadWithExams = await _puntovitaExamRepository.GetCommitteeHeadWithExamsAsync(lastName);
            
            return Ok(_mapper.Map<IEnumerable<HeadByExamsDto>>(committeeHeadWithExams));
        }

        /// <summary>
        /// Deletes a specific committee head.
        /// </summary>
        /// <param name="lastName"> </param>
        /// <returns></returns>


        [HttpDelete]
        public async Task<ActionResult> DeleteExaminationCommitteeHead(string lastName)
        {
            var committeeHead = await _puntovitaExamRepository.GetExaminationCommitteeHeadAsync(lastName);
            if (committeeHead == null)
            {
                throw new NotFoundException($"No committee Head named  {lastName}  was found");
            }
            _puntovitaExamRepository.DeleteExaminationCommitteeHead(committeeHead);
            await _puntovitaExamRepository.SaveChangesAsync();
            return Ok($"{committeeHead.ExaminationCommitteeHeadFirstName} {committeeHead.ExaminationCommitteeHeadLastName} został(a)" +
                $" usunięty(a) z bazy danych");
        }


    }

}

