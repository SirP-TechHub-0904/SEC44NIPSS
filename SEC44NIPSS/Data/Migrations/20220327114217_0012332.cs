using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class _0012332 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChangeDate",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PsChange",
                table: "Profiles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Recipient = table.Column<string>(nullable: true),
                    Mail = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Retries = table.Column<int>(nullable: false),
                    NotificationStatus = table.Column<int>(nullable: false),
                    NotificationType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropColumn(
                name: "ChangeDate",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "PsChange",
                table: "Profiles");
        }
    }
}
