using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class jong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Documents_EventId",
                table: "Documents");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_EventId",
                table: "Documents",
                column: "EventId",
                unique: false,
                filter: "[EventId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Documents_EventId",
                table: "Documents");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_EventId",
                table: "Documents",
                column: "EventId");
        }
    }
}
