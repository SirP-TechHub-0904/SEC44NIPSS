using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class _748930227890987654567834 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "QuestionsLoaded",
                table: "UserAnswers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SN",
                table: "AnswerLists",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionsLoaded",
                table: "UserAnswers");

            migrationBuilder.DropColumn(
                name: "SN",
                table: "AnswerLists");
        }
    }
}
