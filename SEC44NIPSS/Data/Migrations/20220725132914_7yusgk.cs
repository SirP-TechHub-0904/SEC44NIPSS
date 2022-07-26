using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class _7yusgk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestionnerPages",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Instruction = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: false),
                    QuestionnerId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnerPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionnerPages_Questionners_QuestionnerId",
                        column: x => x.QuestionnerId,
                        principalTable: "Questionners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnerPages_QuestionnerId",
                table: "QuestionnerPages",
                column: "QuestionnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionnerPages");
        }
    }
}
