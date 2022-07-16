using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class idfj84844599L : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAnswer_Answers_AnswerId",
                table: "QuestionAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAnswer_Questions_QuestionId",
                table: "QuestionAnswer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionAnswer",
                table: "QuestionAnswer");

            migrationBuilder.DropIndex(
                name: "IX_QuestionAnswer_QuestionId",
                table: "QuestionAnswer");

            migrationBuilder.DropColumn(
                name: "AnswerContent",
                table: "QuestionAnswer");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "QuestionAnswer");

            migrationBuilder.RenameTable(
                name: "QuestionAnswer",
                newName: "QuestionAnswers");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionAnswer_AnswerId",
                table: "QuestionAnswers",
                newName: "IX_QuestionAnswers_AnswerId");

            migrationBuilder.AlterColumn<long>(
                name: "AnswerId",
                table: "QuestionAnswers",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "QuestionAnswers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "QuestionAnswers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "QuestionAnswers",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "QuestionnerId",
                table: "QuestionAnswers",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionAnswers",
                table: "QuestionAnswers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "QuestionResponses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<long>(nullable: true),
                    Answer = table.Column<string>(nullable: true),
                    QuestionAnswerId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionResponses_QuestionAnswers_QuestionAnswerId",
                        column: x => x.QuestionAnswerId,
                        principalTable: "QuestionAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionResponses_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswers_QuestionnerId",
                table: "QuestionAnswers",
                column: "QuestionnerId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionResponses_QuestionAnswerId",
                table: "QuestionResponses",
                column: "QuestionAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionResponses_QuestionId",
                table: "QuestionResponses",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAnswers_Answers_AnswerId",
                table: "QuestionAnswers",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAnswers_Questionners_QuestionnerId",
                table: "QuestionAnswers",
                column: "QuestionnerId",
                principalTable: "Questionners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAnswers_Answers_AnswerId",
                table: "QuestionAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAnswers_Questionners_QuestionnerId",
                table: "QuestionAnswers");

            migrationBuilder.DropTable(
                name: "QuestionResponses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionAnswers",
                table: "QuestionAnswers");

            migrationBuilder.DropIndex(
                name: "IX_QuestionAnswers_QuestionnerId",
                table: "QuestionAnswers");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "QuestionAnswers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "QuestionAnswers");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "QuestionAnswers");

            migrationBuilder.DropColumn(
                name: "QuestionnerId",
                table: "QuestionAnswers");

            migrationBuilder.RenameTable(
                name: "QuestionAnswers",
                newName: "QuestionAnswer");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionAnswers_AnswerId",
                table: "QuestionAnswer",
                newName: "IX_QuestionAnswer_AnswerId");

            migrationBuilder.AlterColumn<long>(
                name: "AnswerId",
                table: "QuestionAnswer",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnswerContent",
                table: "QuestionAnswer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "QuestionId",
                table: "QuestionAnswer",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionAnswer",
                table: "QuestionAnswer",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswer_QuestionId",
                table: "QuestionAnswer",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAnswer_Answers_AnswerId",
                table: "QuestionAnswer",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAnswer_Questions_QuestionId",
                table: "QuestionAnswer",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
