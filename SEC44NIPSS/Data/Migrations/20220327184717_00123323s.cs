using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class _00123323s : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyGroupMemebers_AspNetUsers_UserId",
                table: "StudyGroupMemebers");

            migrationBuilder.DropIndex(
                name: "IX_StudyGroupMemebers_UserId",
                table: "StudyGroupMemebers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StudyGroupMemebers");

            migrationBuilder.AddColumn<long>(
                name: "ProfileId",
                table: "StudyGroupMemebers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_StudyGroupMemebers_ProfileId",
                table: "StudyGroupMemebers",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudyGroupMemebers_Profiles_ProfileId",
                table: "StudyGroupMemebers",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyGroupMemebers_Profiles_ProfileId",
                table: "StudyGroupMemebers");

            migrationBuilder.DropIndex(
                name: "IX_StudyGroupMemebers_ProfileId",
                table: "StudyGroupMemebers");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "StudyGroupMemebers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "StudyGroupMemebers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudyGroupMemebers_UserId",
                table: "StudyGroupMemebers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudyGroupMemebers_AspNetUsers_UserId",
                table: "StudyGroupMemebers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
