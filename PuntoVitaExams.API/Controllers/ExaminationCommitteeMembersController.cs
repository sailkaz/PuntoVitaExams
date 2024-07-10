using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PuntoVitaExams.API.Exceptions;
using PuntoVitaExams.API.Models;
using PuntoVitaExams.API.Services;

namespace PuntoVitaExams.API.Controllers
{
    [ApiController]
    [Route("api/examinationCommitteeMember")]
    [Authorize(Roles = "Admin")]
    public class ExaminationCommitteeMembersController : ControllerBase
    {
        private readonly IPuntoVitaExamRepository _puntovitaExamRepository;
        private readonly IMapper _mapper;

        public ExaminationCommitteeMembersController(IPuntoVitaExamRepository puntoVitaExamRepository, IMapper mapper)
        {
            _puntovitaExamRepository = puntoVitaExamRepository ?? throw new ArgumentNullException(nameof(puntoVitaExamRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Creates a new committee member.
        /// </summary>
        /// <param name="newHeadDto"> </param>
        /// <returns></returns>

        [HttpPost]
        public async Task<ActionResult<ExaminationCommitteeMemberDto>> CreateNewExaminationCommitteeMember(
                   ExaminationCommitteeMemberDto newMemberDto)
        {
            var newMemberToCreate = _mapper.Map<Entities.ExaminationCommitteeMember>(newMemberDto);
            await _puntovitaExamRepository.CreateNewExaminationCommitteeMemberAsync(newMemberToCreate);
            var newMemberToReturn = _mapper.Map<Models.ExaminationCommitteeMemberDto>(newMemberToCreate);
            return CreatedAtRoute("GetExaminationCommitteeMember",
                new
                {
                    lastName = newMemberToReturn.ExaminationCommitteeMemberLastName
                },
                newMemberToReturn);
        }

        /// <summary>
        /// Gets all committee members.
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExaminationCommitteeMemberDto>>> GetExaminationCommitteeMembers()
        {
            var examinationCommitteeMembers = await _puntovitaExamRepository.GetExaminationCommitteeMembersAsync();
            return Ok(_mapper.Map<IEnumerable<ExaminationCommitteeMemberDto>>(examinationCommitteeMembers));
        }

        /// <summary>
        /// Gets a specific committee member.
        /// </summary>
        /// <param name="lastName"> </param>
        /// <returns></returns>

        [HttpGet("{lastName}", Name = "GetExaminationCommitteeMember")]
        public async Task<ActionResult<ExaminationCommitteeMemberDto>> GetExaminationCommitteeMember(string lastName)
        {
            var newMember = await _puntovitaExamRepository.GetExaminationCommitteeMemberAsync(lastName);
            if (newMember == null)
            {
                throw new NotFoundException($"No committee member named {lastName} was found");
            }
            return Ok(_mapper.Map<ExaminationCommitteeMemberDto>(newMember));
        }

        /// <summary>
        /// Assigns the member to the committee.
        /// </summary>
        /// <param name="lastName"> </param>
        /// /// <param name="examinationCommitteeId"> </param>
        /// <returns></returns>

        [HttpGet("{examinationCommitteeId}/{lastName}")]
        public async Task<ActionResult> ConnectCommitteeMemberAndCommittee(
            int examinationCommitteeId, string lastName)
        {
            var member = await _puntovitaExamRepository.GetExaminationCommitteeMemberAsync(lastName);
            if (member == null)
            {
                throw new NotFoundException($"No committee member named {lastName} was found");
            }
            var committee = await _puntovitaExamRepository.GetExaminationCommitteeWithExamsAsync(examinationCommitteeId);
            if (committee == null)
            {
                throw new NotFoundException($"No examination committee with id {examinationCommitteeId} was found");
            }
            member.ExaminationCommittees.Add(committee);
            await _puntovitaExamRepository.SaveChangesAsync();

            return Ok($"{member.ExaminationCommitteeMemberFirstName} {member.ExaminationCommitteeMemberLastName} został(a) wyznaczony(a) " +
                $"na sekretarza komisji Id {examinationCommitteeId}");
        }

        /// <summary>
        /// Displays a specific committee member with exams.
        /// </summary>
        /// <param name="lastName"> </param>
        /// <returns></returns>

        [HttpGet("committeeMemberWithExams")]
        public async Task<ActionResult<IEnumerable<MemberByExamsDto>>> GetCommitteeMemberWithExams(string lastName)
        {
            var committeeMember = await _puntovitaExamRepository.GetExaminationCommitteeMemberAsync(lastName);
            if (committeeMember == null)

            {
                throw new NotFoundException($"No examination committee member named {lastName} was found");
            }

            var committeeMemberByExams = await _puntovitaExamRepository.GetCommitteeMemberWithExamsAsync(lastName);
           
            return Ok(_mapper.Map<IEnumerable<MemberByExamsDto>>(committeeMemberByExams));
        }

        /// <summary>
        /// Deletes a specific committee member.
        /// </summary>
        /// <param name="lastName"></param>
        /// <returns></returns>

        [HttpDelete]
        public async Task<ActionResult> DeleteExaminationCommitteeMember(string lastName)
        {
            var committeeMember = await _puntovitaExamRepository.GetExaminationCommitteeMemberAsync(lastName);
            if (committeeMember == null)
            {
                throw new NotFoundException($"No committee member named {lastName} was found");
            }
            _puntovitaExamRepository.DeleteExaminationCommitteeMember(committeeMember);
            await _puntovitaExamRepository.SaveChangesAsync();
            return Ok($"{committeeMember.ExaminationCommitteeMemberFirstName} {committeeMember.ExaminationCommitteeMemberLastName} został(a) " +
                $" usunięty(a) z bazy danych");
        }
    }
}
