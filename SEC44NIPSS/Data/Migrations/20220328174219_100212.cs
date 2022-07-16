using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class _100212 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourSubCategories_StudyGroups_StudyGroupId",
                table: "TourSubCategories");

            migrationBuilder.AlterColumn<long>(
                name: "StudyGroupId",
                table: "TourSubCategories",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<int>(
                name: "PostFileType",
                table: "TourPosts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "TourCategories",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TourSubCategories_StudyGroups_StudyGroupId",
                table: "TourSubCategories",
                column: "StudyGroupId",
                principalTable: "StudyGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourSubCategories_StudyGroups_StudyGroupId",
                table: "TourSubCategories");

            migrationBuilder.DropColumn(
                name: "PostFileType",
                table: "TourPosts");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "TourCategories");

            migrationBuilder.AlterColumn<long>(
                name: "StudyGroupId",
                table: "TourSubCategories",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TourSubCategories_StudyGroups_StudyGroupId",
                table: "TourSubCategories",
                column: "StudyGroupId",
                principalTable: "StudyGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
