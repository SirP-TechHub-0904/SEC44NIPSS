using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class _1122339 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourPosts_TourSubCategories_TourSubCategoryId",
                table: "TourPosts");

            migrationBuilder.AlterColumn<long>(
                name: "TourSubCategoryId",
                table: "TourPosts",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TourPosts_TourSubCategories_TourSubCategoryId",
                table: "TourPosts",
                column: "TourSubCategoryId",
                principalTable: "TourSubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourPosts_TourSubCategories_TourSubCategoryId",
                table: "TourPosts");

            migrationBuilder.AlterColumn<long>(
                name: "TourSubCategoryId",
                table: "TourPosts",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_TourPosts_TourSubCategories_TourSubCategoryId",
                table: "TourPosts",
                column: "TourSubCategoryId",
                principalTable: "TourSubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
