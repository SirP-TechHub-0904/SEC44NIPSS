using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class jongpt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Module",
                table: "Events",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Module",
                table: "Events");
        }
    }
}
