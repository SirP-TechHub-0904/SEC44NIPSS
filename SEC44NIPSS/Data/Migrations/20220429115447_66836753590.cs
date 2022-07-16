using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class _66836753590 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TokenId",
                table: "UserToNotifys",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Sent",
                table: "Notifications",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TokenId",
                table: "UserToNotifys");

            migrationBuilder.DropColumn(
                name: "Sent",
                table: "Notifications");
        }
    }
}
