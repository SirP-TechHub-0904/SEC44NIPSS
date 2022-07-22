using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class tf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Profiles_ApprovedById",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Profiles_ForwardedToId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Profiles_JobCompletionCertifiedById",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Profiles_ReceivedAndPassToId",
                table: "Tickets");

            migrationBuilder.AlterColumn<long>(
                name: "ReceivedAndPassToId",
                table: "Tickets",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Tickets",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "JobCompletionCertifiedById",
                table: "Tickets",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "Fullname",
                table: "Tickets",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ForwardedToId",
                table: "Tickets",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Tickets",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ApprovedById",
                table: "Tickets",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Profiles_ApprovedById",
                table: "Tickets",
                column: "ApprovedById",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Profiles_ForwardedToId",
                table: "Tickets",
                column: "ForwardedToId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Profiles_JobCompletionCertifiedById",
                table: "Tickets",
                column: "JobCompletionCertifiedById",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Profiles_ReceivedAndPassToId",
                table: "Tickets",
                column: "ReceivedAndPassToId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Profiles_ApprovedById",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Profiles_ForwardedToId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Profiles_JobCompletionCertifiedById",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Profiles_ReceivedAndPassToId",
                table: "Tickets");

            migrationBuilder.AlterColumn<long>(
                name: "ReceivedAndPassToId",
                table: "Tickets",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<long>(
                name: "JobCompletionCertifiedById",
                table: "Tickets",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Fullname",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<long>(
                name: "ForwardedToId",
                table: "Tickets",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<long>(
                name: "ApprovedById",
                table: "Tickets",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Profiles_ApprovedById",
                table: "Tickets",
                column: "ApprovedById",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Profiles_ForwardedToId",
                table: "Tickets",
                column: "ForwardedToId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Profiles_JobCompletionCertifiedById",
                table: "Tickets",
                column: "JobCompletionCertifiedById",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Profiles_ReceivedAndPassToId",
                table: "Tickets",
                column: "ReceivedAndPassToId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
