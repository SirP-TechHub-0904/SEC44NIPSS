using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class _221133221 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EventId",
                table: "Documents",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_EventId",
                table: "Documents",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Events_EventId",
                table: "Documents",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Events_EventId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_EventId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Documents");
        }
    }
}
