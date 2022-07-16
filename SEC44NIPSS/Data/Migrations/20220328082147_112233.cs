using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class _112233 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TourCategories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TourPostTypes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourPostTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TourSubCategories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Date = table.Column<string>(nullable: true),
                    TourCategoryId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourSubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourSubCategories_TourCategories_TourCategoryId",
                        column: x => x.TourCategoryId,
                        principalTable: "TourCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourPosts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    TourPostTypeId = table.Column<long>(nullable: false),
                    Photo = table.Column<string>(nullable: true),
                    TourSubCategoryId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourPosts_TourPostTypes_TourPostTypeId",
                        column: x => x.TourPostTypeId,
                        principalTable: "TourPostTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourPosts_TourSubCategories_TourSubCategoryId",
                        column: x => x.TourSubCategoryId,
                        principalTable: "TourSubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TourPosts_TourPostTypeId",
                table: "TourPosts",
                column: "TourPostTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TourPosts_TourSubCategoryId",
                table: "TourPosts",
                column: "TourSubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TourSubCategories_TourCategoryId",
                table: "TourSubCategories",
                column: "TourCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TourPosts");

            migrationBuilder.DropTable(
                name: "TourPostTypes");

            migrationBuilder.DropTable(
                name: "TourSubCategories");

            migrationBuilder.DropTable(
                name: "TourCategories");
        }
    }
}
