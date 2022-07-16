using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class _47848 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProfileId",
                table: "Galleries",
                nullable: true,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Galleries",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "UseAsActivity",
                table: "Galleries",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Galleries_ProfileId",
                table: "Galleries",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Galleries_Profiles_ProfileId",
                table: "Galleries",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Galleries_Profiles_ProfileId",
                table: "Galleries");

            migrationBuilder.DropIndex(
                name: "IX_Galleries_ProfileId",
                table: "Galleries");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Galleries");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Galleries");

            migrationBuilder.DropColumn(
                name: "UseAsActivity",
                table: "Galleries");
        }
    }
}
