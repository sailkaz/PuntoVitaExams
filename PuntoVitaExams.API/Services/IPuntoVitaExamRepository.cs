using Microsoft.AspNetCore.Mvc;
using PuntoVitaExams.API.Entities;
using PuntoVitaExams.API.Models;

namespace PuntoVitaExams.API.Services
{
    public interface IPuntoVitaExamRepository
    {
        // ExaminationCommitteesController
        Task<IEnumerable<ExaminationCommittee>> GetExaminationCommitteesAsync();
        Task<ExaminationCommittee> GetExaminationCommitteeWithExamsAsync(int examinationCommiteeId);
        Task CreateNewExaminationCommitteeAsync(ExaminationCommittee newCommitteeToCreate);
        void DeleteExaminationCommittee(ExaminationCommittee examinationCommittee);


        // ExaminationCommitteeHeadsController
        Task<IEnumerable<ExaminationCommitteeHead>> GetExaminationCommitteeHeadsAsync();
        Task<ExaminationCommitteeHead> GetExaminationCommitteeHeadAsync(string lastName);
        Task<IEnumerable<HeadByExams>> GetCommitteeHeadWithExamsAsync(string lastName);
        Task CreateNewExaminationCommitteeHeadAsync(ExaminationCommitteeHead newHeadToCreate);
        void DeleteExaminationCommitteeHead(ExaminationCommitteeHead examinationCommitteeHead);


        // ExaminationCommitteeMembersController
        Task<IEnumerable<ExaminationCommitteeMember>> GetExaminationCommitteeMembersAsync();
        Task<ExaminationCommitteeMember> GetExaminationCommitteeMemberAsync(string lastName);
        Task<IEnumerable<MemberByExams>> GetCommitteeMemberWithExamsAsync(string lastName);
        Task CreateNewExaminationCommitteeMemberAsync(ExaminationCommitteeMember newMemberToCreate);
        void DeleteExaminationCommitteeMember(ExaminationCommitteeMember examinationCommitteeMember);


        // ExaminationCommitteeSecretariesController
        Task<IEnumerable<ExaminationCommitteeSecretary>> GetExaminationCommitteeSecretariesAsync();
        Task<ExaminationCommitteeSecretary> GetExaminationCommitteeSecretaryAsync(string lastName);
        Task<IEnumerable<SecretaryByExams>> GetCommitteeSecretaryWithExamsAsync(string lastName);
        Task CreateNewExaminationCommitteeSecretaryAsync(ExaminationCommitteeSecretary newSecretaryToCreate);
        void DeleteExaminationCommitteeSecretary(ExaminationCommitteeSecretary examinationCommitteeSecretary);


        // SailingExamsController
        Task<SailingExam?> GetExamWithStudentsAndCommitteeAsync(string sailingExamNumber);
        Task CreateNewSailingExamAsync(SailingExam newExamToCreate);
        Task<SailingExam> GetExamAsync(string sailingExamNumber);
        Task<SailingExam> GetExamWithCommitteeAsync(string sailingExamNumber);
        Task<SailingExam> GetExamWithStudentsAsync(string sailingExamNumber);
        void DeleteSailingExam(SailingExam exam);
        Task<bool> ExamExistsAsync(string sailingExamNumber);


        // StudentsController
        Task<Student> GetStudentWithAddressAndExamsAsync(string pesel);
        Task<Student> GetStudentAsync(string pesel);
        Task<IEnumerable<StudentsWhoPassedTheExam>> GetStudentsWhoPassedTheExamAsync(string examResult, string examNumber);
        Task CreateNewStudentWithAddressTogetherAsync(Student newStudentToCreate);
        void DeleteStudent(Student student);
        Task<bool> StudentExistsAsync(string pesel);



        Task<bool> SaveChangesAsync();

    }
}
