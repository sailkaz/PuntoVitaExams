using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PuntoVitaExams.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveExaminationCommitteeIdFromSailingExam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SailingExams_ExaminationCommittees_ExaminationCommitteeId",
                table: "SailingExams");

            migrationBuilder.AlterColumn<int>(
                name: "ExaminationCommitteeId",
                table: "SailingExams",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "SailingExams",
                keyColumn: "SailingExamId",
                keyValue: 1,
                column: "ExaminationCommitteeId",
                value: null);

            migrationBuilder.AddForeignKey(
                name: "FK_SailingExams_ExaminationCommittees_ExaminationCommitteeId",
                table: "SailingExams",
                column: "ExaminationCommitteeId",
                principalTable: "ExaminationCommittees",
                principalColumn: "ExaminationCommitteeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SailingExams_ExaminationCommittees_ExaminationCommitteeId",
                table: "SailingExams");

            migrationBuilder.AlterColumn<int>(
                name: "ExaminationCommitteeId",
                table: "SailingExams",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "SailingExams",
                keyColumn: "SailingExamId",
                keyValue: 1,
                column: "ExaminationCommitteeId",
                value: 2);

            migrationBuilder.AddForeignKey(
                name: "FK_SailingExams_ExaminationCommittees_ExaminationCommitteeId",
                table: "SailingExams",
                column: "ExaminationCommitteeId",
                principalTable: "ExaminationCommittees",
                principalColumn: "ExaminationCommitteeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
