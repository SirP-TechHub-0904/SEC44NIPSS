using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class jf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ParlySubThreeCategoryId",
                table: "ParlyReportDocuments",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ParlySubTwoCategoryId",
                table: "ParlyReportDocuments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ParlySubTwoCategories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    ParlyReportSubCategoryId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParlySubTwoCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParlySubTwoCategories_ParlyReportSubCategories_ParlyReportSubCategoryId",
                        column: x => x.ParlyReportSubCategoryId,
                        principalTable: "ParlyReportSubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ParlySubThreeCategories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    ParlySubTwoCategoryId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParlySubThreeCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParlySubThreeCategories_ParlySubTwoCategories_ParlySubTwoCategoryId",
                        column: x => x.ParlySubTwoCategoryId,
                        principalTable: "ParlySubTwoCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParlyReportDocuments_ParlySubThreeCategoryId",
                table: "ParlyReportDocuments",
                column: "ParlySubThreeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ParlyReportDocuments_ParlySubTwoCategoryId",
                table: "ParlyReportDocuments",
                column: "ParlySubTwoCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ParlySubThreeCategories_ParlySubTwoCategoryId",
                table: "ParlySubThreeCategories",
                column: "ParlySubTwoCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ParlySubTwoCategories_ParlyReportSubCategoryId",
                table: "ParlySubTwoCategories",
                column: "ParlyReportSubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParlyReportDocuments_ParlySubThreeCategories_ParlySubThreeCategoryId",
                table: "ParlyReportDocuments",
                column: "ParlySubThreeCategoryId",
                principalTable: "ParlySubThreeCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ParlyReportDocuments_ParlySubTwoCategories_ParlySubTwoCategoryId",
                table: "ParlyReportDocuments",
                column: "ParlySubTwoCategoryId",
                principalTable: "ParlySubTwoCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParlyReportDocuments_ParlySubThreeCategories_ParlySubThreeCategoryId",
                table: "ParlyReportDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_ParlyReportDocuments_ParlySubTwoCategories_ParlySubTwoCategoryId",
                table: "ParlyReportDocuments");

            migrationBuilder.DropTable(
                name: "ParlySubThreeCategories");

            migrationBuilder.DropTable(
                name: "ParlySubTwoCategories");

            migrationBuilder.DropIndex(
                name: "IX_ParlyReportDocuments_ParlySubThreeCategoryId",
                table: "ParlyReportDocuments");

            migrationBuilder.DropIndex(
                name: "IX_ParlyReportDocuments_ParlySubTwoCategoryId",
                table: "ParlyReportDocuments");

            migrationBuilder.DropColumn(
                name: "ParlySubThreeCategoryId",
                table: "ParlyReportDocuments");

            migrationBuilder.DropColumn(
                name: "ParlySubTwoCategoryId",
                table: "ParlyReportDocuments");
        }
    }
}
