using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class _6683675359054 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserToNotifys_Profiles_ProfileId",
                table: "UserToNotifys");

            migrationBuilder.AlterColumn<long>(
                name: "ProfileId",
                table: "UserToNotifys",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_UserToNotifys_Profiles_ProfileId",
                table: "UserToNotifys",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserToNotifys_Profiles_ProfileId",
                table: "UserToNotifys");

            migrationBuilder.AlterColumn<long>(
                name: "ProfileId",
                table: "UserToNotifys",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserToNotifys_Profiles_ProfileId",
                table: "UserToNotifys",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
