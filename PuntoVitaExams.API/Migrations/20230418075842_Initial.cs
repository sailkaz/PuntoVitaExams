using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PuntoVitaExams.API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addressess",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlatNumber = table.Column<int>(type: "int", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addressess", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "ExaminationCommitteeHeads",
                columns: table => new
                {
                    ExaminationCommitteeHeadId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExaminationCommitteeHeadFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExaminationCommitteeHeadLastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationCommitteeHeads", x => x.ExaminationCommitteeHeadId);
                });

            migrationBuilder.CreateTable(
                name: "ExaminationCommitteeMembers",
                columns: table => new
                {
                    ExaminationCommitteeMemberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExaminationCommitteeMemberFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExaminationCommitteeMemberLastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationCommitteeMembers", x => x.ExaminationCommitteeMemberId);
                });

            migrationBuilder.CreateTable(
                name: "ExaminationCommitteeSecretaries",
                columns: table => new
                {
                    ExaminationCommitteeSecretaryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExaminationCommitteeSecretaryFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExaminationCommitteeSecretaryLastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationCommitteeSecretaries", x => x.ExaminationCommitteeSecretaryId);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pesel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Students_Addressess_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addressess",
                        principalColumn: "AddressId");
                });

            migrationBuilder.CreateTable(
                name: "ExaminationCommittees",
                columns: table => new
                {
                    ExaminationCommitteeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExaminationCommitteeHeadId = table.Column<int>(type: "int", nullable: true),
                    ExaminationCommitteeSecretaryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationCommittees", x => x.ExaminationCommitteeId);
                    table.ForeignKey(
                        name: "FK_ExaminationCommittees_ExaminationCommitteeHeads_ExaminationCommitteeHeadId",
                        column: x => x.ExaminationCommitteeHeadId,
                        principalTable: "ExaminationCommitteeHeads",
                        principalColumn: "ExaminationCommitteeHeadId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExaminationCommittees_ExaminationCommitteeSecretaries_ExaminationCommitteeSecretaryId",
                        column: x => x.ExaminationCommitteeSecretaryId,
                        principalTable: "ExaminationCommitteeSecretaries",
                        principalColumn: "ExaminationCommitteeSecretaryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExaminationCommitteeExaminationCommitteeMember",
                columns: table => new
                {
                    ExaminationCommitteeMembersExaminationCommitteeMemberId = table.Column<int>(type: "int", nullable: false),
                    ExaminationCommitteesExaminationCommitteeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationCommitteeExaminationCommitteeMember", x => new { x.ExaminationCommitteeMembersExaminationCommitteeMemberId, x.ExaminationCommitteesExaminationCommitteeId });
                    table.ForeignKey(
                        name: "FK_ExaminationCommitteeExaminationCommitteeMember_ExaminationCommitteeMembers_ExaminationCommitteeMembersExaminationCommitteeMe~",
                        column: x => x.ExaminationCommitteeMembersExaminationCommitteeMemberId,
                        principalTable: "ExaminationCommitteeMembers",
                        principalColumn: "ExaminationCommitteeMemberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExaminationCommitteeExaminationCommitteeMember_ExaminationCommittees_ExaminationCommitteesExaminationCommitteeId",
                        column: x => x.ExaminationCommitteesExaminationCommitteeId,
                        principalTable: "ExaminationCommittees",
                        principalColumn: "ExaminationCommitteeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SailingExams",
                columns: table => new
                {
                    SailingExamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SailingExamNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SailingExamDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SailingExamPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExaminationCommitteeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SailingExams", x => x.SailingExamId);
                    table.ForeignKey(
                        name: "FK_SailingExams_ExaminationCommittees_ExaminationCommitteeId",
                        column: x => x.ExaminationCommitteeId,
                        principalTable: "ExaminationCommittees",
                        principalColumn: "ExaminationCommitteeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SailingExamStudent",
                columns: table => new
                {
                    SailingExamsSailingExamId = table.Column<int>(type: "int", nullable: false),
                    StudentsStudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SailingExamStudent", x => new { x.SailingExamsSailingExamId, x.StudentsStudentId });
                    table.ForeignKey(
                        name: "FK_SailingExamStudent_SailingExams_SailingExamsSailingExamId",
                        column: x => x.SailingExamsSailingExamId,
                        principalTable: "SailingExams",
                        principalColumn: "SailingExamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SailingExamStudent_Students_StudentsStudentId",
                        column: x => x.StudentsStudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Addressess",
                columns: new[] { "AddressId", "City", "FlatNumber", "PostalCode", "Street", "StreetNumber" },
                values: new object[,]
                {
                    { 1, "Chorzów", 15, "38-143", "Gołębia", "37a" },
                    { 2, "Jelenia Góra", null, "96-813", "Podgórna", "12" },
                    { 3, "Zamość", 29, "03-413", "Dębowa", "23" }
                });

            migrationBuilder.InsertData(
                table: "ExaminationCommitteeHeads",
                columns: new[] { "ExaminationCommitteeHeadId", "ExaminationCommitteeHeadFirstName", "ExaminationCommitteeHeadLastName" },
                values: new object[,]
                {
                    { 1, "Wiesław", "Ogorzały" },
                    { 2, "Krzysztof", "Nieraczej" }
                });

            migrationBuilder.InsertData(
                table: "ExaminationCommitteeMembers",
                columns: new[] { "ExaminationCommitteeMemberId", "ExaminationCommitteeMemberFirstName", "ExaminationCommitteeMemberLastName" },
                values: new object[,]
                {
                    { 1, "Maiola", "Rosochata" },
                    { 2, "Renata", "Koniec" }
                });

            migrationBuilder.InsertData(
                table: "ExaminationCommitteeSecretaries",
                columns: new[] { "ExaminationCommitteeSecretaryId", "ExaminationCommitteeSecretaryFirstName", "ExaminationCommitteeSecretaryLastName" },
                values: new object[,]
                {
                    { 1, "Karol", "Wyczesany" },
                    { 2, "Michał", "Zaradek" }
                });

            migrationBuilder.InsertData(
                table: "ExaminationCommittees",
                columns: new[] { "ExaminationCommitteeId", "ExaminationCommitteeHeadId", "ExaminationCommitteeSecretaryId" },
                values: new object[,]
                {
                    { 1, null, null },
                    { 2, null, null },
                    { 3, null, null }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "AddressId", "FirstName", "LastName", "Pesel", "Result" },
                values: new object[,]
                {
                    { 1, null, "Jacek", "Przekora", "258456963", null },
                    { 2, null, "Wojciech", "Zapas", "369546129", null },
                    { 3, null, "Paweł", "Zapas", "746952167", null },
                    { 4, null, "Grzegorz", "Niespodziany", "974369542", null }
                });

            migrationBuilder.InsertData(
                table: "SailingExams",
                columns: new[] { "SailingExamId", "ExaminationCommitteeId", "SailingExamDate", "SailingExamNumber", "SailingExamPlace" },
                values: new object[] { 1, 2, new DateTime(2023, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "PV/2/23", "Wilkasy" });

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationCommitteeExaminationCommitteeMember_ExaminationCommitteesExaminationCommitteeId",
                table: "ExaminationCommitteeExaminationCommitteeMember",
                column: "ExaminationCommitteesExaminationCommitteeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationCommittees_ExaminationCommitteeHeadId",
                table: "ExaminationCommittees",
                column: "ExaminationCommitteeHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationCommittees_ExaminationCommitteeSecretaryId",
                table: "ExaminationCommittees",
                column: "ExaminationCommitteeSecretaryId");

            migrationBuilder.CreateIndex(
                name: "IX_SailingExams_ExaminationCommitteeId",
                table: "SailingExams",
                column: "ExaminationCommitteeId");

            migrationBuilder.CreateIndex(
                name: "IX_SailingExamStudent_StudentsStudentId",
                table: "SailingExamStudent",
                column: "StudentsStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_AddressId",
                table: "Students",
                column: "AddressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExaminationCommitteeExaminationCommitteeMember");

            migrationBuilder.DropTable(
                name: "SailingExamStudent");

            migrationBuilder.DropTable(
                name: "ExaminationCommitteeMembers");

            migrationBuilder.DropTable(
                name: "SailingExams");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "ExaminationCommittees");

            migrationBuilder.DropTable(
                name: "Addressess");

            migrationBuilder.DropTable(
                name: "ExaminationCommitteeHeads");

            migrationBuilder.DropTable(
                name: "ExaminationCommitteeSecretaries");
        }
    }
}
