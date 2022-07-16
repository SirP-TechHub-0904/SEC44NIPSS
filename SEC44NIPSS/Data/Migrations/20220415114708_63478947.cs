using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class _63478947 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AlumniId",
                table: "TourCategories",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AlumniId",
                table: "StudyGroups",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AlumniId",
                table: "Executive",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Alumnis",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    DateRange = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true),
                    GeneralTopic = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alumnis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentCategories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    AlumniId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentCategories_Alumnis_AlumniId",
                        column: x => x.AlumniId,
                        principalTable: "Alumnis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "secProjects",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    AlumniId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_secProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_secProjects_Alumnis_AlumniId",
                        column: x => x.AlumniId,
                        principalTable: "Alumnis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    DocumentCategoryId = table.Column<long>(nullable: false),
                    ProfileId = table.Column<long>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    DocType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_DocumentCategories_DocumentCategoryId",
                        column: x => x.DocumentCategoryId,
                        principalTable: "DocumentCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documents_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecProjectExecutives",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileId = table.Column<long>(nullable: false),
                    Position = table.Column<string>(nullable: true),
                    SecProjectId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecProjectExecutives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecProjectExecutives_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SecProjectExecutives_secProjects_SecProjectId",
                        column: x => x.SecProjectId,
                        principalTable: "secProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TourCategories_AlumniId",
                table: "TourCategories",
                column: "AlumniId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyGroups_AlumniId",
                table: "StudyGroups",
                column: "AlumniId");

            migrationBuilder.CreateIndex(
                name: "IX_Executive_AlumniId",
                table: "Executive",
                column: "AlumniId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentCategories_AlumniId",
                table: "DocumentCategories",
                column: "AlumniId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DocumentCategoryId",
                table: "Documents",
                column: "DocumentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ProfileId",
                table: "Documents",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_SecProjectExecutives_ProfileId",
                table: "SecProjectExecutives",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_SecProjectExecutives_SecProjectId",
                table: "SecProjectExecutives",
                column: "SecProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_secProjects_AlumniId",
                table: "secProjects",
                column: "AlumniId",
                unique: true,
                filter: "[AlumniId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Executive_Alumnis_AlumniId",
                table: "Executive",
                column: "AlumniId",
                principalTable: "Alumnis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudyGroups_Alumnis_AlumniId",
                table: "StudyGroups",
                column: "AlumniId",
                principalTable: "Alumnis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TourCategories_Alumnis_AlumniId",
                table: "TourCategories",
                column: "AlumniId",
                principalTable: "Alumnis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Executive_Alumnis_AlumniId",
                table: "Executive");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyGroups_Alumnis_AlumniId",
                table: "StudyGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_TourCategories_Alumnis_AlumniId",
                table: "TourCategories");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "SecProjectExecutives");

            migrationBuilder.DropTable(
                name: "DocumentCategories");

            migrationBuilder.DropTable(
                name: "secProjects");

            migrationBuilder.DropTable(
                name: "Alumnis");

            migrationBuilder.DropIndex(
                name: "IX_TourCategories_AlumniId",
                table: "TourCategories");

            migrationBuilder.DropIndex(
                name: "IX_StudyGroups_AlumniId",
                table: "StudyGroups");

            migrationBuilder.DropIndex(
                name: "IX_Executive_AlumniId",
                table: "Executive");

            migrationBuilder.DropColumn(
                name: "AlumniId",
                table: "TourCategories");

            migrationBuilder.DropColumn(
                name: "AlumniId",
                table: "StudyGroups");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "AlumniId",
                table: "Executive");
        }
    }
}
