using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class _7489302278909876545678 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "UserAnswers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_AnswerLists_RapidQuestionId",
                table: "AnswerLists",
                column: "RapidQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerLists_RapidQuestions_RapidQuestionId",
                table: "AnswerLists",
                column: "RapidQuestionId",
                principalTable: "RapidQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerLists_RapidQuestions_RapidQuestionId",
                table: "AnswerLists");

            migrationBuilder.DropIndex(
                name: "IX_AnswerLists_RapidQuestionId",
                table: "AnswerLists");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "UserAnswers");
        }
    }
}
