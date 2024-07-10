using PuntoVitaExams.API.Entities;
using PuntoVitaExams.API.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace PuntoVitaExams.API.Services
{
    public class PuntoVitaExamRepository : IPuntoVitaExamRepository
    {
        private readonly ExamContext _context;

        public PuntoVitaExamRepository(ExamContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        // ExaminationCommitteesController
        public async Task<IEnumerable<ExaminationCommittee>> GetExaminationCommitteesAsync()
        {
            return await _context.ExaminationCommittees
                .Include(e => e.ExaminationCommitteeHead)
                .Include(e => e.ExaminationCommitteeSecretary)
                .Include(e => e.ExaminationCommitteeMembers)
                .Include(e => e.SailingExams)
                .OrderBy(e => e.ExaminationCommitteeId).ToListAsync();
        }
        public async Task<ExaminationCommittee> GetExaminationCommitteeWithExamsAsync(int examinationCommiteeId)
        {
            return await _context.ExaminationCommittees
                .Include("ExaminationCommitteeHead")
                .Include("ExaminationCommitteeSecretary")
                .Include("ExaminationCommitteeMembers")
                .Include(s => s.SailingExams)
                .Where(e => e.ExaminationCommitteeId == examinationCommiteeId)
                .FirstOrDefaultAsync();
        }
        public async Task CreateNewExaminationCommitteeAsync(ExaminationCommittee newCommitteeToCreate)
        {
            await _context.ExaminationCommittees.AddAsync(newCommitteeToCreate);
            await _context.SaveChangesAsync();
        }
        public void DeleteExaminationCommittee(ExaminationCommittee examinationCommittee)
        {
            _context.ExaminationCommittees.Remove(examinationCommittee);
        }


        // ExaminatonCommitteeHeadsController
        public async Task<IEnumerable<ExaminationCommitteeHead>> GetExaminationCommitteeHeadsAsync()
        {
            return await _context.ExaminationCommitteeHeads.OrderBy(e => e.ExaminationCommitteeHeadLastName).ToListAsync();
        }
        public async Task<ExaminationCommitteeHead> GetExaminationCommitteeHeadAsync(string lastName)
        {
            return await _context.ExaminationCommitteeHeads.
                Where(e => e.ExaminationCommitteeHeadLastName == lastName).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<HeadByExams>> GetCommitteeHeadWithExamsAsync(string lastName)
        {
            return await _context.HeadByExams.Where(h => h.ExaminationCommitteeHeadLastName == lastName).ToListAsync();
        }
        public async Task CreateNewExaminationCommitteeHeadAsync(ExaminationCommitteeHead newHeadToCreate)
        {
            await _context.ExaminationCommitteeHeads.AddAsync(newHeadToCreate);
            await _context.SaveChangesAsync();
        }
        public void DeleteExaminationCommitteeHead(ExaminationCommitteeHead examinationCommitteeHead)
        {
            _context.ExaminationCommitteeHeads.Remove(examinationCommitteeHead);
        }


        // ExaminationCommitteeMembersController
        public async Task<IEnumerable<ExaminationCommitteeMember>> GetExaminationCommitteeMembersAsync()
        {
            return await _context.ExaminationCommitteeMembers.OrderBy(e => e.ExaminationCommitteeMemberLastName).ToListAsync();
        }
        public async Task<ExaminationCommitteeMember> GetExaminationCommitteeMemberAsync(string lastName)
        {
            return await _context.ExaminationCommitteeMembers.Where(m => m.ExaminationCommitteeMemberLastName == lastName).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<MemberByExams>> GetCommitteeMemberWithExamsAsync(string lastName)
        {
            return await _context.MemberByExams.Where(h => h.ExaminationCommitteeMemberLastName == lastName).ToListAsync();
        }
        public async Task CreateNewExaminationCommitteeMemberAsync(ExaminationCommitteeMember newMemberToCreate)
        {
            await _context.ExaminationCommitteeMembers.AddAsync(newMemberToCreate);
            await _context.SaveChangesAsync();
        }
        public void DeleteExaminationCommitteeMember(ExaminationCommitteeMember examinationCommitteeMember)
        {
            _context.ExaminationCommitteeMembers.Remove(examinationCommitteeMember);
        }


        // ExaminationCommitteeSecretariesController
        public async Task<IEnumerable<ExaminationCommitteeSecretary>> GetExaminationCommitteeSecretariesAsync()
        {
            return await _context.ExaminationCommitteeSecretaries.OrderBy(e => e.ExaminationCommitteeSecretaryLastName).ToListAsync();
        }
        public async Task<ExaminationCommitteeSecretary> GetExaminationCommitteeSecretaryAsync(string lastName)
        {
            return await _context.ExaminationCommitteeSecretaries.
                Where(e => e.ExaminationCommitteeSecretaryLastName == lastName).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<SecretaryByExams>> GetCommitteeSecretaryWithExamsAsync(string lastName)
        {
            return await _context.SecretaryByExams.Where(h => h.ExaminationCommitteeSecretaryLastName == lastName).ToListAsync();
        }
        public async Task CreateNewExaminationCommitteeSecretaryAsync(ExaminationCommitteeSecretary newSecretaryToCreate)
        {
            await _context.ExaminationCommitteeSecretaries.AddAsync(newSecretaryToCreate);
            await _context.SaveChangesAsync();
        }
        public void DeleteExaminationCommitteeSecretary(ExaminationCommitteeSecretary examinationCommitteeSecretary)
        {
            _context.ExaminationCommitteeSecretaries.Remove(examinationCommitteeSecretary);
        }


        // SailingExamsController
        public async Task<SailingExam> GetExamWithStudentsAndCommitteeAsync(string sailingExamNumber)
        {
            return await _context.SailingExams
                .Include(e => e.Students)  
                .Include(e => e.ExaminationCommittee.ExaminationCommitteeHead)
                .Include(e => e.ExaminationCommittee.ExaminationCommitteeSecretary)
                .Include(e => e.ExaminationCommittee.ExaminationCommitteeMembers)
                .Where(e => e.SailingExamNumber == sailingExamNumber)
                .FirstOrDefaultAsync();
        }
        public async Task<SailingExam> GetExamAsync(string sailingExamNumber)
        {
            return await _context.SailingExams.Where(e => e.SailingExamNumber == sailingExamNumber).FirstOrDefaultAsync();
        }
        public async Task<SailingExam> GetExamWithCommitteeAsync(string sailingExamNumber)
        {
            return await _context.SailingExams.Include(e => e.ExaminationCommittee).Where(e => e.SailingExamNumber == sailingExamNumber).FirstOrDefaultAsync();
        }
        public async Task<SailingExam> GetExamWithStudentsAsync(string sailingExamNumber)
        {
            return await _context.SailingExams.Include(e => e.Students).Where(e => e.SailingExamNumber == sailingExamNumber).FirstOrDefaultAsync();
        }
        public async Task CreateNewSailingExamAsync(SailingExam newExamToCreate)
        {
            await _context.SailingExams.AddAsync(newExamToCreate);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExamExistsAsync(string sailingExamNumber)
        {
            return await _context.SailingExams.AnyAsync(e => e.SailingExamNumber == sailingExamNumber);
        }
        public void DeleteSailingExam(SailingExam exam)
        {
            _context.SailingExams.Remove(exam);
        }


        // StudentsController
        public async Task<Student> GetStudentWithAddressAndExamsAsync(string pesel)
        {
            return await _context.Students.Include(s => s.Address).Include("SailingExams").Where(s => s.Pesel == pesel).FirstOrDefaultAsync();
        }
        public async Task<Student> GetStudentAsync(string pesel)
        {
            return await _context.Students.Where(s => s.Pesel == pesel).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<StudentsWhoPassedTheExam>> GetStudentsWhoPassedTheExamAsync(string examResult, string examNumber)
        {
            return await _context.StudentsWhoPassedTheExam.Where(s => s.Result == examResult && s.SailingExamNumber == examNumber)
                .ToListAsync();
        }
        public async Task CreateNewStudentWithAddressTogetherAsync(Student newStudentToCreate)
        {
            await _context.Students.AddAsync(newStudentToCreate);
            await _context.SaveChangesAsync();
        }
        public void DeleteStudent(Student student)
        {
            _context.Students.Remove(student);
        }
        public async Task<bool> StudentExistsAsync(string pesel)
        {
            return await _context.Students.AnyAsync(s => s.Pesel == pesel);
        }



        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }


    }
}
