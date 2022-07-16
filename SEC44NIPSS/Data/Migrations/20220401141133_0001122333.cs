using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class _0001122333 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ParticipantPhoto",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfileUpdateLevel",
                table: "Profiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StudyGroupRole",
                table: "Profiles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParticipantPhoto",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "ProfileUpdateLevel",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "StudyGroupRole",
                table: "Profiles");
        }
    }
}
