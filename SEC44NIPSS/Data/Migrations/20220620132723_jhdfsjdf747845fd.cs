using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class jhdfsjdf747845fd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SliderCategories_Sliders_SliderId",
                table: "SliderCategories");

            migrationBuilder.DropIndex(
                name: "IX_SliderCategories_SliderId",
                table: "SliderCategories");

            migrationBuilder.DropColumn(
                name: "SliderId",
                table: "SliderCategories");

            migrationBuilder.AddColumn<long>(
                name: "SliderCategoryId",
                table: "Sliders",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Sliders_SliderCategoryId",
                table: "Sliders",
                column: "SliderCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sliders_SliderCategories_SliderCategoryId",
                table: "Sliders",
                column: "SliderCategoryId",
                principalTable: "SliderCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sliders_SliderCategories_SliderCategoryId",
                table: "Sliders");

            migrationBuilder.DropIndex(
                name: "IX_Sliders_SliderCategoryId",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "SliderCategoryId",
                table: "Sliders");

            migrationBuilder.AddColumn<long>(
                name: "SliderId",
                table: "SliderCategories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_SliderCategories_SliderId",
                table: "SliderCategories",
                column: "SliderId");

            migrationBuilder.AddForeignKey(
                name: "FK_SliderCategories_Sliders_SliderId",
                table: "SliderCategories",
                column: "SliderId",
                principalTable: "Sliders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
