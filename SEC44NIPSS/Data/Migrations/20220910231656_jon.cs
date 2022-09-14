using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class jon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantDocuments_ParticipantDocumentCategories_ParticipantDocumentCategoryId",
                table: "ParticipantDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantDocuments_Participants_ParticipantId1",
                table: "ParticipantDocuments");

            migrationBuilder.DropIndex(
                name: "IX_ParticipantDocuments_ParticipantId1",
                table: "ParticipantDocuments");

            migrationBuilder.DropColumn(
                name: "ParticipantId1",
                table: "ParticipantDocuments");

            migrationBuilder.AlterColumn<long>(
                name: "ParticipantId",
                table: "ParticipantDocuments",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ParticipantDocumentCategoryId",
                table: "ParticipantDocuments",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantDocuments_ParticipantId",
                table: "ParticipantDocuments",
                column: "ParticipantId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantDocuments_ParticipantDocumentCategories_ParticipantDocumentCategoryId",
                table: "ParticipantDocuments",
                column: "ParticipantDocumentCategoryId",
                principalTable: "ParticipantDocumentCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantDocuments_Participants_ParticipantId",
                table: "ParticipantDocuments",
                column: "ParticipantId",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantDocuments_ParticipantDocumentCategories_ParticipantDocumentCategoryId",
                table: "ParticipantDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantDocuments_Participants_ParticipantId",
                table: "ParticipantDocuments");

            migrationBuilder.DropIndex(
                name: "IX_ParticipantDocuments_ParticipantId",
                table: "ParticipantDocuments");

            migrationBuilder.AlterColumn<string>(
                name: "ParticipantId",
                table: "ParticipantDocuments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ParticipantDocumentCategoryId",
                table: "ParticipantDocuments",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ParticipantId1",
                table: "ParticipantDocuments",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantDocuments_ParticipantId1",
                table: "ParticipantDocuments",
                column: "ParticipantId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantDocuments_ParticipantDocumentCategories_ParticipantDocumentCategoryId",
                table: "ParticipantDocuments",
                column: "ParticipantDocumentCategoryId",
                principalTable: "ParticipantDocumentCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantDocuments_Participants_ParticipantId1",
                table: "ParticipantDocuments",
                column: "ParticipantId1",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
