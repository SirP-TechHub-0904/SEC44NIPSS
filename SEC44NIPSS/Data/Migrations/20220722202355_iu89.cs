using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class iu89 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TicketStaffStatus",
                table: "TicketStaff",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TicketItemStatus",
                table: "TicketRequirements",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketStaffStatus",
                table: "TicketStaff");

            migrationBuilder.DropColumn(
                name: "TicketItemStatus",
                table: "TicketRequirements");
        }
    }
}
