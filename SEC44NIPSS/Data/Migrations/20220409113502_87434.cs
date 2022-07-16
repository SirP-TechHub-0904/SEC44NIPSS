using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class _87434 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutProfile",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DontShow",
                table: "Profiles",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutProfile",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "DontShow",
                table: "Profiles");
        }
    }
}
