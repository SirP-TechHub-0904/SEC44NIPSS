using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class _9922334455 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AlumniId",
                table: "Profiles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_AlumniId",
                table: "Profiles",
                column: "AlumniId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Alumnis_AlumniId",
                table: "Profiles",
                column: "AlumniId",
                principalTable: "Alumnis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Alumnis_AlumniId",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_AlumniId",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "AlumniId",
                table: "Profiles");
        }
    }
}
