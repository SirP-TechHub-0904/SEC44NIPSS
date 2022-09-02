using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class indecff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "StudyGroupId",
                table: "Participants",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DirectingStaffs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SortOrder = table.Column<string>(nullable: true),
                    AlumniId = table.Column<long>(nullable: true),
                    ProfileId = table.Column<long>(nullable: true),
                    StudyGroupId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectingStaffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DirectingStaffs_Alumnis_AlumniId",
                        column: x => x.AlumniId,
                        principalTable: "Alumnis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectingStaffs_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectingStaffs_StudyGroups_StudyGroupId",
                        column: x => x.StudyGroupId,
                        principalTable: "StudyGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participants_StudyGroupId",
                table: "Participants",
                column: "StudyGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectingStaffs_AlumniId",
                table: "DirectingStaffs",
                column: "AlumniId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectingStaffs_ProfileId",
                table: "DirectingStaffs",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectingStaffs_StudyGroupId",
                table: "DirectingStaffs",
                column: "StudyGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_StudyGroups_StudyGroupId",
                table: "Participants",
                column: "StudyGroupId",
                principalTable: "StudyGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_StudyGroups_StudyGroupId",
                table: "Participants");

            migrationBuilder.DropTable(
                name: "DirectingStaffs");

            migrationBuilder.DropIndex(
                name: "IX_Participants_StudyGroupId",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "StudyGroupId",
                table: "Participants");
        }
    }
}
