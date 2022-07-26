using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class tfddil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProfileId",
                table: "TicketResponses",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "TicketRequirements",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketResponses_ProfileId",
                table: "TicketResponses",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketResponses_Profiles_ProfileId",
                table: "TicketResponses",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketResponses_Profiles_ProfileId",
                table: "TicketResponses");

            migrationBuilder.DropIndex(
                name: "IX_TicketResponses_ProfileId",
                table: "TicketResponses");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "TicketResponses");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "TicketRequirements");
        }
    }
}
