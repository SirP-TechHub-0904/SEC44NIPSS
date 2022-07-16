using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class _758685939556 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommitteeCategories",
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
                    table.PrimaryKey("PK_CommitteeCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommitteeCategories_Alumnis_AlumniId",
                        column: x => x.AlumniId,
                        principalTable: "Alumnis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Committees",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position = table.Column<string>(nullable: true),
                    SortOrder = table.Column<string>(nullable: true),
                    ProfileId = table.Column<long>(nullable: true),
                    CommitteeCategoryId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Committees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Committees_CommitteeCategories_CommitteeCategoryId",
                        column: x => x.CommitteeCategoryId,
                        principalTable: "CommitteeCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Committees_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommitteeCategories_AlumniId",
                table: "CommitteeCategories",
                column: "AlumniId");

            migrationBuilder.CreateIndex(
                name: "IX_Committees_CommitteeCategoryId",
                table: "Committees",
                column: "CommitteeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Committees_ProfileId",
                table: "Committees",
                column: "ProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Committees");

            migrationBuilder.DropTable(
                name: "CommitteeCategories");
        }
    }
}
