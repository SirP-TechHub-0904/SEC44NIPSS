using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class _769hdv3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudyGroupMemebers_ProfileId",
                table: "StudyGroupMemebers");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsExecutive",
                table: "NipssStaff",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_StudyGroupMemebers_ProfileId",
                table: "StudyGroupMemebers",
                column: "ProfileId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudyGroupMemebers_ProfileId",
                table: "StudyGroupMemebers");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "IsExecutive",
                table: "NipssStaff");

            migrationBuilder.CreateIndex(
                name: "IX_StudyGroupMemebers_ProfileId",
                table: "StudyGroupMemebers",
                column: "ProfileId");
        }
    }
}
