using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class _47848998i : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Galleries_Profiles_ProfileId",
                table: "Galleries");

            migrationBuilder.AlterColumn<long>(
                name: "ProfileId",
                table: "Galleries",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<bool>(
                name: "Private",
                table: "Galleries",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Galleries_Profiles_ProfileId",
                table: "Galleries",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Galleries_Profiles_ProfileId",
                table: "Galleries");

            migrationBuilder.DropColumn(
                name: "Private",
                table: "Galleries");

            migrationBuilder.AlterColumn<long>(
                name: "ProfileId",
                table: "Galleries",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Galleries_Profiles_ProfileId",
                table: "Galleries",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
