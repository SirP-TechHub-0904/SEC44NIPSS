using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class docUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AlumniId",
                table: "ParlyReportDocuments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocumentOwner",
                table: "ParlyReportDocuments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "StudyGroupId",
                table: "ParlyReportDocuments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParlyReportDocuments_AlumniId",
                table: "ParlyReportDocuments",
                column: "AlumniId");

            migrationBuilder.CreateIndex(
                name: "IX_ParlyReportDocuments_StudyGroupId",
                table: "ParlyReportDocuments",
                column: "StudyGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParlyReportDocuments_Alumnis_AlumniId",
                table: "ParlyReportDocuments",
                column: "AlumniId",
                principalTable: "Alumnis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ParlyReportDocuments_StudyGroups_StudyGroupId",
                table: "ParlyReportDocuments",
                column: "StudyGroupId",
                principalTable: "StudyGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParlyReportDocuments_Alumnis_AlumniId",
                table: "ParlyReportDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_ParlyReportDocuments_StudyGroups_StudyGroupId",
                table: "ParlyReportDocuments");

            migrationBuilder.DropIndex(
                name: "IX_ParlyReportDocuments_AlumniId",
                table: "ParlyReportDocuments");

            migrationBuilder.DropIndex(
                name: "IX_ParlyReportDocuments_StudyGroupId",
                table: "ParlyReportDocuments");

            migrationBuilder.DropColumn(
                name: "AlumniId",
                table: "ParlyReportDocuments");

            migrationBuilder.DropColumn(
                name: "DocumentOwner",
                table: "ParlyReportDocuments");

            migrationBuilder.DropColumn(
                name: "StudyGroupId",
                table: "ParlyReportDocuments");
        }
    }
}
