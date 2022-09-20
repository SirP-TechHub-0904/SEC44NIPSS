using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class djksur : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ParlyReportSubCategoryId",
                table: "ParlyReportDocuments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ParlyReportSubCategories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    ParlyReportCategoryId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParlyReportSubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParlyReportSubCategories_ParlyReportCategories_ParlyReportCategoryId",
                        column: x => x.ParlyReportCategoryId,
                        principalTable: "ParlyReportCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParlyReportDocuments_ParlyReportSubCategoryId",
                table: "ParlyReportDocuments",
                column: "ParlyReportSubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ParlyReportSubCategories_ParlyReportCategoryId",
                table: "ParlyReportSubCategories",
                column: "ParlyReportCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParlyReportDocuments_ParlyReportSubCategories_ParlyReportSubCategoryId",
                table: "ParlyReportDocuments",
                column: "ParlyReportSubCategoryId",
                principalTable: "ParlyReportSubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParlyReportDocuments_ParlyReportSubCategories_ParlyReportSubCategoryId",
                table: "ParlyReportDocuments");

            migrationBuilder.DropTable(
                name: "ParlyReportSubCategories");

            migrationBuilder.DropIndex(
                name: "IX_ParlyReportDocuments_ParlyReportSubCategoryId",
                table: "ParlyReportDocuments");

            migrationBuilder.DropColumn(
                name: "ParlyReportSubCategoryId",
                table: "ParlyReportDocuments");
        }
    }
}
