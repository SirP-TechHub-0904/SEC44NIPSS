using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class _948576jf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "C1",
                table: "LegacyProjects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "C2",
                table: "LegacyProjects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "C3",
                table: "LegacyProjects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "C4",
                table: "LegacyProjects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "D1",
                table: "LegacyProjects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "D2",
                table: "LegacyProjects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "D3",
                table: "LegacyProjects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "D4",
                table: "LegacyProjects",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VotingType",
                table: "LegacyProjects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VotingType",
                table: "LegacyProjectAnswers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "C1",
                table: "LegacyProjects");

            migrationBuilder.DropColumn(
                name: "C2",
                table: "LegacyProjects");

            migrationBuilder.DropColumn(
                name: "C3",
                table: "LegacyProjects");

            migrationBuilder.DropColumn(
                name: "C4",
                table: "LegacyProjects");

            migrationBuilder.DropColumn(
                name: "D1",
                table: "LegacyProjects");

            migrationBuilder.DropColumn(
                name: "D2",
                table: "LegacyProjects");

            migrationBuilder.DropColumn(
                name: "D3",
                table: "LegacyProjects");

            migrationBuilder.DropColumn(
                name: "D4",
                table: "LegacyProjects");

            migrationBuilder.DropColumn(
                name: "VotingType",
                table: "LegacyProjects");

            migrationBuilder.DropColumn(
                name: "VotingType",
                table: "LegacyProjectAnswers");
        }
    }
}
