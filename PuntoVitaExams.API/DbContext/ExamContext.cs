using Microsoft.EntityFrameworkCore;
using PuntoVitaExams.API.Entities;
using PuntoVitaExams.API.Models;

namespace PuntoVitaExams.API.DbContexts
{
    public class ExamContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Address> Addressess { get; set; }
        public DbSet<SailingExam> SailingExams { get; set; }
        public DbSet<ExaminationCommittee> ExaminationCommittees { get; set; }
        public DbSet<ExaminationCommitteeHead> ExaminationCommitteeHeads { get; set; }
        public DbSet<ExaminationCommitteeSecretary> ExaminationCommitteeSecretaries { get; set; }
        public DbSet<ExaminationCommitteeMember> ExaminationCommitteeMembers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<HeadByExams> HeadByExams { get; set; }
        public DbSet<SecretaryByExams> SecretaryByExams { get; set; }
        public DbSet<MemberByExams> MemberByExams { get; set; }
        public DbSet<StudentsWhoPassedTheExam> StudentsWhoPassedTheExam { get; set; }
        //public DbSet<RefreshToken> RefreshTokens { get; set; }

        public ExamContext(DbContextOptions<ExamContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "User" },
                new Role { Id = 2, Name = "Manager"},
                new Role { Id = 3, Name = "Admin"}
                    );

            modelBuilder.Entity<Student>().HasData(
                new Student { StudentId = 1, FirstName = "Jacek", LastName = "Przekora", Pesel = "258456963" });
            var studentList = new Student[]{
                new Student { StudentId = 2, FirstName = "Wojciech", LastName = "Zapas", Pesel = "369546129" },
                new Student { StudentId = 3, FirstName = "Paweł", LastName = "Zapas", Pesel = "746952167" },
                new Student { StudentId = 4, FirstName = "Grzegorz", LastName = "Niespodziany", Pesel = "974369542" }
                };
            modelBuilder.Entity<Student>().HasData(studentList);

            modelBuilder.Entity<ExaminationCommitteeHead>().HasData(
                new ExaminationCommitteeHead
                {
                    ExaminationCommitteeHeadId = 1,
                    ExaminationCommitteeHeadFirstName = "Wiesław",
                    ExaminationCommitteeHeadLastName = "Ogorzały",
                    
                },

                new ExaminationCommitteeHead
                {
                    ExaminationCommitteeHeadId = 2,
                    ExaminationCommitteeHeadFirstName = "Krzysztof",
                    ExaminationCommitteeHeadLastName = "Nieraczej",
                    
                });

            modelBuilder.Entity<ExaminationCommitteeSecretary>().HasData(
                new ExaminationCommitteeSecretary
                {
                    ExaminationCommitteeSecretaryId = 1,
                    ExaminationCommitteeSecretaryFirstName = "Karol",
                    ExaminationCommitteeSecretaryLastName = "Wyczesany",
                    
                },

                new ExaminationCommitteeSecretary
                {
                    ExaminationCommitteeSecretaryId = 2,
                    ExaminationCommitteeSecretaryFirstName = "Michał",
                    ExaminationCommitteeSecretaryLastName = "Zaradek",
                   
                });
            modelBuilder.Entity<ExaminationCommitteeMember>().HasData(
                new ExaminationCommitteeMember
                {
                    ExaminationCommitteeMemberId = 1,
                    ExaminationCommitteeMemberFirstName = "Maiola",
                    ExaminationCommitteeMemberLastName = "Rosochata",
                },
                new ExaminationCommitteeMember
                {
                    ExaminationCommitteeMemberId = 2,
                    ExaminationCommitteeMemberFirstName = "Renata",
                    ExaminationCommitteeMemberLastName = "Koniec"
                });
            modelBuilder.Entity<Address>().HasData(
                new Address
                {
                    AddressId = 1,
                    Street = "Gołębia",
                    StreetNumber = "37a",
                    FlatNumber = 15,
                    PostalCode = "38-143",
                    City = "Chorzów"
                },
                new Address
                {
                    AddressId = 2,
                    Street = "Podgórna",
                    StreetNumber = "12",
                    PostalCode = "96-813",
                    City = "Jelenia Góra"
                },
                new Address
                {
                    AddressId = 3,
                    Street = "Dębowa",
                    StreetNumber = "23",
                    FlatNumber = 29,
                    PostalCode = "03-413",
                    City = "Zamość"
                });
            var examinationCommitteeList = new ExaminationCommittee[]
            {
                new ExaminationCommittee {ExaminationCommitteeId = 1},
                new ExaminationCommittee {ExaminationCommitteeId = 2},
                new ExaminationCommittee {ExaminationCommitteeId = 3}
            };
            modelBuilder.Entity<ExaminationCommittee>().HasData(examinationCommitteeList);

            modelBuilder.Entity<SailingExam>().HasData(

                new SailingExam
                {
                    SailingExamId = 1,
                    SailingExamNumber = "PV/2/23",
                    SailingExamPlace = "Wilkasy",
                    SailingExamDate = new DateTime(2023,02,16),
                });

        }
    }
}
 

