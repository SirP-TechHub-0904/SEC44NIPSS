using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class docUpdate4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BgColor",
                table: "ParlySubTwoCategories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BgColor",
                table: "ParlySubThreeCategories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BgColor",
                table: "ParlyReportSubCategories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BgColor",
                table: "ParlyReportCategories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BgColor",
                table: "ParlySubTwoCategories");

            migrationBuilder.DropColumn(
                name: "BgColor",
                table: "ParlySubThreeCategories");

            migrationBuilder.DropColumn(
                name: "BgColor",
                table: "ParlyReportSubCategories");

            migrationBuilder.DropColumn(
                name: "BgColor",
                table: "ParlyReportCategories");
        }
    }
}
