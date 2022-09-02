using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class indec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ParticipantId",
                table: "Executive",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AlumniId",
                table: "Committees",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ParticipantDocumentCategories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantDocumentCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileId = table.Column<long>(nullable: true),
                    AlumniId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participants_Alumnis_AlumniId",
                        column: x => x.AlumniId,
                        principalTable: "Alumnis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Participants_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AlumniId = table.Column<long>(nullable: true),
                    StudyGroupId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tours_Alumnis_AlumniId",
                        column: x => x.AlumniId,
                        principalTable: "Alumnis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tours_StudyGroups_StudyGroupId",
                        column: x => x.StudyGroupId,
                        principalTable: "StudyGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SecPapers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    ParticipantDocumentCategoryId = table.Column<long>(nullable: false),
                    Powerpoint = table.Column<string>(nullable: true),
                    Report = table.Column<string>(nullable: true),
                    Script = table.Column<string>(nullable: true),
                    AlumniId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecPapers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecPapers_Alumnis_AlumniId",
                        column: x => x.AlumniId,
                        principalTable: "Alumnis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SecPapers_ParticipantDocumentCategories_ParticipantDocumentCategoryId",
                        column: x => x.ParticipantDocumentCategoryId,
                        principalTable: "ParticipantDocumentCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManagingStaffs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position = table.Column<string>(nullable: true),
                    SortOrder = table.Column<string>(nullable: true),
                    AlumniId = table.Column<long>(nullable: true),
                    ParticipantId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagingStaffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManagingStaffs_Alumnis_AlumniId",
                        column: x => x.AlumniId,
                        principalTable: "Alumnis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ManagingStaffs_Participants_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ParticipantDocuments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParticipantId = table.Column<string>(nullable: true),
                    ParticipantId1 = table.Column<long>(nullable: true),
                    ParticipantDocumentCategoryId = table.Column<long>(nullable: false),
                    Powerpoint = table.Column<string>(nullable: true),
                    Report = table.Column<string>(nullable: true),
                    Script = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParticipantDocuments_ParticipantDocumentCategories_ParticipantDocumentCategoryId",
                        column: x => x.ParticipantDocumentCategoryId,
                        principalTable: "ParticipantDocumentCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipantDocuments_Participants_ParticipantId1",
                        column: x => x.ParticipantId1,
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Executive_ParticipantId",
                table: "Executive",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_Committees_AlumniId",
                table: "Committees",
                column: "AlumniId");

            migrationBuilder.CreateIndex(
                name: "IX_ManagingStaffs_AlumniId",
                table: "ManagingStaffs",
                column: "AlumniId");

            migrationBuilder.CreateIndex(
                name: "IX_ManagingStaffs_ParticipantId",
                table: "ManagingStaffs",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantDocuments_ParticipantDocumentCategoryId",
                table: "ParticipantDocuments",
                column: "ParticipantDocumentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantDocuments_ParticipantId1",
                table: "ParticipantDocuments",
                column: "ParticipantId1");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_AlumniId",
                table: "Participants",
                column: "AlumniId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_ProfileId",
                table: "Participants",
                column: "ProfileId",
                unique: true,
                filter: "[ProfileId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SecPapers_AlumniId",
                table: "SecPapers",
                column: "AlumniId");

            migrationBuilder.CreateIndex(
                name: "IX_SecPapers_ParticipantDocumentCategoryId",
                table: "SecPapers",
                column: "ParticipantDocumentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_AlumniId",
                table: "Tours",
                column: "AlumniId");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_StudyGroupId",
                table: "Tours",
                column: "StudyGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Committees_Alumnis_AlumniId",
                table: "Committees",
                column: "AlumniId",
                principalTable: "Alumnis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Executive_Participants_ParticipantId",
                table: "Executive",
                column: "ParticipantId",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Committees_Alumnis_AlumniId",
                table: "Committees");

            migrationBuilder.DropForeignKey(
                name: "FK_Executive_Participants_ParticipantId",
                table: "Executive");

            migrationBuilder.DropTable(
                name: "ManagingStaffs");

            migrationBuilder.DropTable(
                name: "ParticipantDocuments");

            migrationBuilder.DropTable(
                name: "SecPapers");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropTable(
                name: "ParticipantDocumentCategories");

            migrationBuilder.DropIndex(
                name: "IX_Executive_ParticipantId",
                table: "Executive");

            migrationBuilder.DropIndex(
                name: "IX_Committees_AlumniId",
                table: "Committees");

            migrationBuilder.DropColumn(
                name: "ParticipantId",
                table: "Executive");

            migrationBuilder.DropColumn(
                name: "AlumniId",
                table: "Committees");
        }
    }
}
