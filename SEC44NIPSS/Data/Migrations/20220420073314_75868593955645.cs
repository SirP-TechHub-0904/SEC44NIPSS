using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class _75868593955645 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Committees_CommitteeCategories_CommitteeCategoryId",
                table: "Committees");

            migrationBuilder.AlterColumn<long>(
                name: "CommitteeCategoryId",
                table: "Committees",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Committees_CommitteeCategories_CommitteeCategoryId",
                table: "Committees",
                column: "CommitteeCategoryId",
                principalTable: "CommitteeCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Committees_CommitteeCategories_CommitteeCategoryId",
                table: "Committees");

            migrationBuilder.AlterColumn<long>(
                name: "CommitteeCategoryId",
                table: "Committees",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Committees_CommitteeCategories_CommitteeCategoryId",
                table: "Committees",
                column: "CommitteeCategoryId",
                principalTable: "CommitteeCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
