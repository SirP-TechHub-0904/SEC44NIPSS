using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class _874h8985er : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DirectingStaffOne",
                table: "StudyGroups",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DirectingStaffTwo",
                table: "StudyGroups",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DirectingStaffOne",
                table: "StudyGroups");

            migrationBuilder.DropColumn(
                name: "DirectingStaffTwo",
                table: "StudyGroups");
        }
    }
}
