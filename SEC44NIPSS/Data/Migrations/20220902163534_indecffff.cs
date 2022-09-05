using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class indecffff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParlyReportCategories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParlyReportCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParlyReportDocuments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Document = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    ProfileId = table.Column<long>(nullable: true),
                    ParlyReportCategoryId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParlyReportDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParlyReportDocuments_ParlyReportCategories_ParlyReportCategoryId",
                        column: x => x.ParlyReportCategoryId,
                        principalTable: "ParlyReportCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParlyReportDocuments_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParlyReportDocuments_ParlyReportCategoryId",
                table: "ParlyReportDocuments",
                column: "ParlyReportCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ParlyReportDocuments_ProfileId",
                table: "ParlyReportDocuments",
                column: "ProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParlyReportDocuments");

            migrationBuilder.DropTable(
                name: "ParlyReportCategories");
        }
    }
}
