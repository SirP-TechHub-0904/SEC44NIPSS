using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class jfd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "ParlySubTwoCategories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "ParlySubThreeCategories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "ParlyReportSubCategories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "ParlyReportDocuments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "ParlyReportCategories",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "ParlySubTwoCategories");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "ParlySubThreeCategories");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "ParlyReportSubCategories");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "ParlyReportDocuments");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "ParlyReportCategories");
        }
    }
}
