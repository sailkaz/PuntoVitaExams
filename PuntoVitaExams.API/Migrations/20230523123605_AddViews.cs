using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PuntoVitaExams.API.Migrations
{
    /// <inheritdoc />
    public partial class AddViews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                    @"CREATE VIEW HeadByExams AS
                    SELECT h.ExaminationCommitteeHeadFirstName,
                            h.ExaminationCommitteeHeadLastName,
                            e.SailingExamNumber,
                            e.SailingExamDate, 
                            e.SailingExamPlace
                    FROM ExaminationCommitteeHeads AS h
                    INNER JOIN ExaminationCommittees AS c
                    ON h.ExaminationCommitteeHeadId = c.ExaminationCommitteeHeadId
                    INNER JOIN SailingExams AS e
                    ON c.ExaminationCommitteeId = e.ExaminationCommitteeId;"
                 );
            migrationBuilder.Sql(
                    @"CREATE VIEW SecretaryByExams AS
                    SELECT s.ExaminationCommitteeSecretaryFirstName,
                            s.ExaminationCommitteeSecretaryLastName,
                            e.SailingExamNumber,
                            e.SailingExamDate, 
                            e.SailingExamPlace
                    FROM ExaminationCommitteeSecretaries AS s
                    INNER JOIN ExaminationCommittees AS c
                    ON s.ExaminationCommitteeSecretaryId = c.ExaminationCommitteeSecretaryId
                    INNER JOIN SailingExams AS e
                    ON c.ExaminationCommitteeId = e.ExaminationCommitteeId;"
                 );
            migrationBuilder.Sql(
                    @"CREATE VIEW MemberByExams AS
                    SELECT m.ExaminationCommitteeMemberFirstName,
                            m.ExaminationCommitteeMemberLastName,
                            e.SailingExamNumber,
                            e.SailingExamDate, 
                            e.SailingExamPlace
                    FROM ExaminationCommitteeMembers AS m
                    INNER JOIN ExaminationCommitteeExaminationCommitteeMember AS ccm
                    ON m.ExaminationCommitteeMemberId = ccm.ExaminationCommitteeMembersExaminationCommitteeMemberId 
                    INNER JOIN ExaminationCommittees AS c
                    ON ccm.ExaminationCommitteesExaminationCommitteeId = c.ExaminationCommitteeId 
                    INNER JOIN SailingExams AS e
                    ON c.ExaminationCommitteeId = e.ExaminationCommitteeId;"
                 );
            migrationBuilder.Sql(
                    @"CREATE VIEW StudentsWhoPassedTheExam AS
                    SELECT s.FirstName,
                            s.LastName, 
                            s.Pesel,
                            s.Result,
                            e.SailingExamNumber,
                            a.Street,
                            a.StreetNumber,
                            a.FlatNumber, 
                            a.PostalCode,
                            a.City
                    FROM    Students AS s
                    INNER JOIN SailingExamStudent AS es
                    ON s.StudentId = es.StudentsStudentId 
                    INNER JOIN SailingExams AS e
                    ON es.SailingExamsSailingExamId = e.SailingExamId
                    INNER JOIN Addressess AS a
                    ON s.AddressId = a.AddressId;"
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW HeadByExams");

            migrationBuilder.Sql("DROP VIEW MemberByExams");

            migrationBuilder.Sql("DROP VIEW SecretaryByExams");

            migrationBuilder.Sql("DROP VIEW StudentsWhoPassedTheExam");
        }
    }
}
