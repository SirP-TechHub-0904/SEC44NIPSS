using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class _7yusg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QPage",
                table: "Questions");

            migrationBuilder.AddColumn<int>(
                name: "PageNumber",
                table: "Questions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Questionners",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubTitle",
                table: "Questionners",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PageNumber",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Questionners");

            migrationBuilder.DropColumn(
                name: "SubTitle",
                table: "Questionners");

            migrationBuilder.AddColumn<int>(
                name: "QPage",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
