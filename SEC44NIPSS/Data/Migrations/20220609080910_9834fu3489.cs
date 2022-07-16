using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class _9834fu3489 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LongNoteMaximumLength",
                table: "Options",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LongNoteMinimumLength",
                table: "Options",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MultipleOption1",
                table: "Options",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MultipleOption2",
                table: "Options",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MultipleOption3",
                table: "Options",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MultipleOption4",
                table: "Options",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MultipleOption5",
                table: "Options",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MultipleOption6",
                table: "Options",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MultipleOption7",
                table: "Options",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShortNoteMaximumLength",
                table: "Options",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShortNoteMinimumLength",
                table: "Options",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LongNoteMaximumLength",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "LongNoteMinimumLength",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "MultipleOption1",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "MultipleOption2",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "MultipleOption3",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "MultipleOption4",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "MultipleOption5",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "MultipleOption6",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "MultipleOption7",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "ShortNoteMaximumLength",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "ShortNoteMinimumLength",
                table: "Options");
        }
    }
}
