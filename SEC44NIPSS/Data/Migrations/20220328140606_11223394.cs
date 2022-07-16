using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class _11223394 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "StudyGroupId",
                table: "TourSubCategories",
                nullable: true,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_TourSubCategories_StudyGroupId",
                table: "TourSubCategories",
                column: "StudyGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_TourSubCategories_StudyGroups_StudyGroupId",
                table: "TourSubCategories",
                column: "StudyGroupId",
                principalTable: "StudyGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourSubCategories_StudyGroups_StudyGroupId",
                table: "TourSubCategories");

            migrationBuilder.DropIndex(
                name: "IX_TourSubCategories_StudyGroupId",
                table: "TourSubCategories");

            migrationBuilder.DropColumn(
                name: "StudyGroupId",
                table: "TourSubCategories");
        }
    }
}
