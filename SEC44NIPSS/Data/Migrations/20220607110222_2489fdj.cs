using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class _2489fdj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreviewImage",
                table: "Questionners",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProfileId",
                table: "Questionners",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questionners_ProfileId",
                table: "Questionners",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questionners_Profiles_ProfileId",
                table: "Questionners",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questionners_Profiles_ProfileId",
                table: "Questionners");

            migrationBuilder.DropIndex(
                name: "IX_Questionners_ProfileId",
                table: "Questionners");

            migrationBuilder.DropColumn(
                name: "PreviewImage",
                table: "Questionners");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Questionners");
        }
    }
}
