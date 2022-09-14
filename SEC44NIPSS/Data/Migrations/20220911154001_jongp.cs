using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class jongp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Show",
                table: "DocumentCategories",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Show",
                table: "DocumentCategories");
        }
    }
}
