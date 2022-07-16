using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class _0099887711 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LGA",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OfficeAddress",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResidenceAddress",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortProfile",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StateOfOrigin",
                table: "Profiles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LGA",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "OfficeAddress",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "ResidenceAddress",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "ShortProfile",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "StateOfOrigin",
                table: "Profiles");
        }
    }
}
