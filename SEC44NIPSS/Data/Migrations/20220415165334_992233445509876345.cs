using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class _992233445509876345 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fullname",
                table: "Executive");

            migrationBuilder.AddColumn<long>(
                name: "ProfileId",
                table: "Executive",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Executive_ProfileId",
                table: "Executive",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Executive_Profiles_ProfileId",
                table: "Executive",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Executive_Profiles_ProfileId",
                table: "Executive");

            migrationBuilder.DropIndex(
                name: "IX_Executive_ProfileId",
                table: "Executive");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Executive");

            migrationBuilder.AddColumn<string>(
                name: "Fullname",
                table: "Executive",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
