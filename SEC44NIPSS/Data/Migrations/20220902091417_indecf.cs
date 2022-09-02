using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class indecf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ManagingStaffs_Participants_ParticipantId",
                table: "ManagingStaffs");

            migrationBuilder.DropIndex(
                name: "IX_ManagingStaffs_ParticipantId",
                table: "ManagingStaffs");

            migrationBuilder.DropColumn(
                name: "ParticipantId",
                table: "ManagingStaffs");

            migrationBuilder.AddColumn<long>(
                name: "ProfileId",
                table: "ManagingStaffs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ManagingStaffs_ProfileId",
                table: "ManagingStaffs",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_ManagingStaffs_Profiles_ProfileId",
                table: "ManagingStaffs",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ManagingStaffs_Profiles_ProfileId",
                table: "ManagingStaffs");

            migrationBuilder.DropIndex(
                name: "IX_ManagingStaffs_ProfileId",
                table: "ManagingStaffs");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "ManagingStaffs");

            migrationBuilder.AddColumn<long>(
                name: "ParticipantId",
                table: "ManagingStaffs",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ManagingStaffs_ParticipantId",
                table: "ManagingStaffs",
                column: "ParticipantId");

            migrationBuilder.AddForeignKey(
                name: "FK_ManagingStaffs_Participants_ParticipantId",
                table: "ManagingStaffs",
                column: "ParticipantId",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
