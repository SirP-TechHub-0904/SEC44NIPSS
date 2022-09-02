using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class indecfff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SecPapers_ParticipantDocumentCategories_ParticipantDocumentCategoryId",
                table: "SecPapers");

            migrationBuilder.DropIndex(
                name: "IX_SecPapers_ParticipantDocumentCategoryId",
                table: "SecPapers");

            migrationBuilder.DropColumn(
                name: "ParticipantDocumentCategoryId",
                table: "SecPapers");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SecPapers",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DocumentCategoryId",
                table: "SecPapers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_SecPapers_DocumentCategoryId",
                table: "SecPapers",
                column: "DocumentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SecPapers_DocumentCategories_DocumentCategoryId",
                table: "SecPapers",
                column: "DocumentCategoryId",
                principalTable: "DocumentCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SecPapers_DocumentCategories_DocumentCategoryId",
                table: "SecPapers");

            migrationBuilder.DropIndex(
                name: "IX_SecPapers_DocumentCategoryId",
                table: "SecPapers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "SecPapers");

            migrationBuilder.DropColumn(
                name: "DocumentCategoryId",
                table: "SecPapers");

            migrationBuilder.AddColumn<long>(
                name: "ParticipantDocumentCategoryId",
                table: "SecPapers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_SecPapers_ParticipantDocumentCategoryId",
                table: "SecPapers",
                column: "ParticipantDocumentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SecPapers_ParticipantDocumentCategories_ParticipantDocumentCategoryId",
                table: "SecPapers",
                column: "ParticipantDocumentCategoryId",
                principalTable: "ParticipantDocumentCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
