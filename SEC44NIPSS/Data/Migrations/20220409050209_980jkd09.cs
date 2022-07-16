using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class _980jkd09 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectAnswers");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.CreateTable(
                name: "LegacyProjectAnswers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    Answer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegacyProjectAnswers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LegacyProjects",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    P1 = table.Column<string>(nullable: true),
                    P2 = table.Column<string>(nullable: true),
                    P3 = table.Column<string>(nullable: true),
                    P4 = table.Column<string>(nullable: true),
                    P5 = table.Column<string>(nullable: true),
                    P6 = table.Column<string>(nullable: true),
                    P7 = table.Column<string>(nullable: true),
                    P8 = table.Column<string>(nullable: true),
                    P9 = table.Column<string>(nullable: true),
                    P10 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegacyProjects", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LegacyProjectAnswers");

            migrationBuilder.DropTable(
                name: "LegacyProjects");

            migrationBuilder.CreateTable(
                name: "ProjectAnswers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectAnswers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    P1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    P10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    P2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    P3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    P4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    P5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    P6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    P7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    P8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    P9 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });
        }
    }
}
