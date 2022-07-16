using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class _987878643434545464566 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Answers");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Answers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Answers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Answers");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Answers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
