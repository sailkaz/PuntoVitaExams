using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PuntoVitaExams.API.Exceptions;
using PuntoVitaExams.API.Models;
using PuntoVitaExams.API.Services;


namespace PuntoVitaExams.API.Controllers
{
    [ApiController]
    [Route("api/students")]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        private readonly IPuntoVitaExamRepository _puntoVitaExamRepository;
        private readonly IMapper _mapper;

        public StudentsController(IPuntoVitaExamRepository puntoVitaExamRepository, IMapper mapper)
        {
            _puntoVitaExamRepository = puntoVitaExamRepository ?? throw new ArgumentNullException(nameof(puntoVitaExamRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Creates new student.
        /// </summary>
        /// <param name="newStudentDto"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<ActionResult<StudentForCreationDto>> CreateStudentWithAddressTogether(StudentForCreationDto newStudentDto)
        {
            var newStudentToCreate = _mapper.Map<Entities.Student>(newStudentDto);
            await _puntoVitaExamRepository.CreateNewStudentWithAddressTogetherAsync(newStudentToCreate);
            var newStudentToReturn = _mapper.Map<Models.StudentForCreationDto>(newStudentToCreate);
            return CreatedAtRoute("GetStudentWithAddressAndExams",
                new
                {
                    pesel = newStudentToCreate.Pesel,
                },
                newStudentToReturn);
        }

        /// <summary>
        /// Gets a specific student with his exams.
        /// </summary>
        /// <param name="pesel"></param>
        /// <returns></returns>

        [HttpGet("{pesel}", Name = "GetStudentWithAddressAndExams")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> GetStudentWithAddrressAndExams(string pesel)
        {
            if (!await _puntoVitaExamRepository.StudentExistsAsync(pesel))
            {
                throw new NotFoundException($"No student with pesel {pesel} was found");
            }
            var studentEntity = await _puntoVitaExamRepository.GetStudentWithAddressAndExamsAsync(pesel);
            if (studentEntity.SailingExams.IsNullOrEmpty())
            {
                throw new NotFoundException($"Student with pesel {pesel} has taken no exams");
            }
            return Ok(_mapper.Map<StudentDto>(studentEntity));
        }

        /// <summary>
        /// Gets students and their addressess by the exam result
        /// </summary>
        /// <param name="examResult"></param>
        /// <param name="examNumber"></param>
        /// <returns></returns>

        [HttpGet("{examResult}/{examNumber}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<ActionResult> GetStudentsWhoPassedTheExam(string examResult, string examNumber)
        {
            var students = await _puntoVitaExamRepository.GetStudentsWhoPassedTheExamAsync(examResult, examNumber);
            return Ok(_mapper.Map<IEnumerable<StudentsWhoPassedTheExamDto>>(students));
        }

        /// <summary>
        /// Adds a specific student to the exam.
        /// </summary>
        /// <param name="pesel"></param>
        /// <param name="sailingExamNumber"></param>
        /// <returns></returns>

        [HttpPost("{pesel}/{sailingExamNumber}")]
        public async Task<ActionResult> AddStudentToExam(string pesel, string sailingExamNumber)
        {
            var student = await _puntoVitaExamRepository.GetStudentAsync(pesel);
            if (student == null)
            {
                throw new NotFoundException($"No student with pesel {pesel} was found");
            }
            var exam = await _puntoVitaExamRepository.GetExamWithStudentsAsync(sailingExamNumber);
            if (exam == null)
            {
                throw new NotFoundException($"No exam with number {sailingExamNumber} was found");
            }
            if (exam.Students.Contains(student))
            {
                throw new BadRequestException("This student has been already added to this exam.");
            }
            exam.Students.Add(student);
            await _puntoVitaExamRepository.SaveChangesAsync();
            return Ok($"{student.FirstName} {student.LastName} został(a) dopisany(a) do egzaminu {sailingExamNumber}");
        }

        /// <summary>
        /// Updates exam result.
        /// </summary>
        /// <param name="pesel"></param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>

        [HttpPatch("{pesel}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<ActionResult> UpdateExamResult(string pesel,
            JsonPatchDocument<StudentForAddingExamResultDto> patchDocument)
        {
            var studentEntity = await _puntoVitaExamRepository.GetStudentAsync(pesel);
            if (studentEntity == null)
            {
                throw new NotFoundException($"Nie odnaleziono studenta o numerze pesel  {pesel}");
            }
            var studentToPatch = _mapper.Map<StudentForAddingExamResultDto>(studentEntity);
            patchDocument.ApplyTo(studentToPatch, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(studentToPatch))
            {
                return BadRequest(ModelState);
            }
            _mapper.Map(studentToPatch, studentEntity);
            await _puntoVitaExamRepository.SaveChangesAsync();

            
            return Ok($"Wprowadzono wynik egzaminu dla {studentEntity.FirstName} {studentEntity.LastName}");
        }

        //[HttpPut("{pesel}/{result}")]
        //[Authorize(Roles = "Admin, Manager")]
        //public async Task<ActionResult> AddExamResult(string pesel, string result)
        //{
        //    var studentEntity = await _puntoVitaExamRepository.GetStudentAsync(pesel);
        //    if (studentEntity == null)
        //    {
        //        throw new NotFoundException($"No student with pesel  {pesel}  was found");
        //    }

        //    studentEntity.Result = result;
        //    await _puntoVitaExamRepository.SaveChangesAsync();
        //    return Ok(studentEntity);
        //}


        /// <summary>
        /// Deletes a specific student.
        /// </summary>
        /// <param name="pesel"></param>
        /// <returns></returns>

        [HttpDelete("{pesel}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<ActionResult> DeleteStudent(string pesel)
        {
            var student = await _puntoVitaExamRepository.GetStudentAsync(pesel);
            if (student == null)
            {
                throw new NotFoundException($"No Student with pesel {pesel} was found");
            }
            _puntoVitaExamRepository.DeleteStudent(student);
            await _puntoVitaExamRepository.SaveChangesAsync();
            return Ok($"{student.FirstName} {student.LastName} został usunięty z bazy danych.");
        }

    }
}
