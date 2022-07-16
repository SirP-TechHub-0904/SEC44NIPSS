using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class idfj848445 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AddTimeFrame",
                table: "Questionners",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CloseTime",
                table: "Questionners",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Closed",
                table: "Questionners",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Email",
                table: "Questionners",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PhoneNumber",
                table: "Questionners",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "SendRespondantEmailAfterAttempt",
                table: "Questionners",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SendResponse",
                table: "Questionners",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShowReSubmitBotton",
                table: "Questionners",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Questionners",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddTimeFrame",
                table: "Questionners");

            migrationBuilder.DropColumn(
                name: "CloseTime",
                table: "Questionners");

            migrationBuilder.DropColumn(
                name: "Closed",
                table: "Questionners");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Questionners");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Questionners");

            migrationBuilder.DropColumn(
                name: "SendRespondantEmailAfterAttempt",
                table: "Questionners");

            migrationBuilder.DropColumn(
                name: "SendResponse",
                table: "Questionners");

            migrationBuilder.DropColumn(
                name: "ShowReSubmitBotton",
                table: "Questionners");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Questionners");
        }
    }
}
