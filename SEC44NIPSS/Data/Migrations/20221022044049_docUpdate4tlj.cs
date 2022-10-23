using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class docUpdate4tlj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EventId",
                table: "ParlyReportDocuments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParlyReportDocuments_EventId",
                table: "ParlyReportDocuments",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParlyReportDocuments_Events_EventId",
                table: "ParlyReportDocuments",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParlyReportDocuments_Events_EventId",
                table: "ParlyReportDocuments");

            migrationBuilder.DropIndex(
                name: "IX_ParlyReportDocuments_EventId",
                table: "ParlyReportDocuments");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "ParlyReportDocuments");
        }
    }
}
