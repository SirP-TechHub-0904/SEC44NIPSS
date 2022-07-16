using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class _992233445509876 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SecProjectExecutives_secProjects_SecProjectId",
                table: "SecProjectExecutives");

            migrationBuilder.AlterColumn<long>(
                name: "SecProjectId",
                table: "SecProjectExecutives",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SecProjectExecutives_secProjects_SecProjectId",
                table: "SecProjectExecutives",
                column: "SecProjectId",
                principalTable: "secProjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SecProjectExecutives_secProjects_SecProjectId",
                table: "SecProjectExecutives");

            migrationBuilder.AlterColumn<long>(
                name: "SecProjectId",
                table: "SecProjectExecutives",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_SecProjectExecutives_secProjects_SecProjectId",
                table: "SecProjectExecutives",
                column: "SecProjectId",
                principalTable: "secProjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
