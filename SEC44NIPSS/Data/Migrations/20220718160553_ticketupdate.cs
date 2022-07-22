using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class ticketupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ApprovedById",
                table: "Tickets",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedByTime",
                table: "Tickets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "ForwardedToId",
                table: "Tickets",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "ForwardedToTime",
                table: "Tickets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Fullname",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseOfficeNumber",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "JobCompletionCertifiedById",
                table: "Tickets",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "JobCompletionCertifiedBySignature",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "JobCompletionCertifiedByTime",
                table: "Tickets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "NoteApprovedBy",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoteByReceivedAndPassTo",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoteJobCompletionCertifiedBy",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ReceivedAndPassToId",
                table: "Tickets",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceivedAndPassToTime",
                table: "Tickets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RequestedBy",
                table: "Tickets",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TicketRequirements",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    QuantityRequired = table.Column<int>(nullable: false),
                    QuantityIssued = table.Column<int>(nullable: false),
                    Cost = table.Column<decimal>(nullable: false),
                    SIVno = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    TicketId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketRequirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketRequirements_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "TicketStaff",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkCarriedOut = table.Column<string>(nullable: true),
                    Signature = table.Column<string>(nullable: true),
                    ProfileId = table.Column<long>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    TicketId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketStaff_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TicketStaff_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "TicketSupervisor",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    SendEmail = table.Column<bool>(nullable: false),
                    SendPhone = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketSupervisor", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ApprovedById",
                table: "Tickets",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ForwardedToId",
                table: "Tickets",
                column: "ForwardedToId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_JobCompletionCertifiedById",
                table: "Tickets",
                column: "JobCompletionCertifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ReceivedAndPassToId",
                table: "Tickets",
                column: "ReceivedAndPassToId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketRequirements_TicketId",
                table: "TicketRequirements",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketStaff_ProfileId",
                table: "TicketStaff",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketStaff_TicketId",
                table: "TicketStaff",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Profiles_ApprovedById",
                table: "Tickets",
                column: "ApprovedById",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Profiles_ForwardedToId",
                table: "Tickets",
                column: "ForwardedToId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Profiles_JobCompletionCertifiedById",
                table: "Tickets",
                column: "JobCompletionCertifiedById",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Profiles_ReceivedAndPassToId",
                table: "Tickets",
                column: "ReceivedAndPassToId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
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

            migrationBuilder.DropTable(
                name: "TicketRequirements");

            migrationBuilder.DropTable(
                name: "TicketStaff");

            migrationBuilder.DropTable(
                name: "TicketSupervisor");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_ApprovedById",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_ForwardedToId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_JobCompletionCertifiedById",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_ReceivedAndPassToId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ApprovedByTime",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ForwardedToId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ForwardedToTime",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Fullname",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "HouseOfficeNumber",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "JobCompletionCertifiedById",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "JobCompletionCertifiedBySignature",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "JobCompletionCertifiedByTime",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "NoteApprovedBy",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "NoteByReceivedAndPassTo",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "NoteJobCompletionCertifiedBy",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ReceivedAndPassToId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ReceivedAndPassToTime",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "RequestedBy",
                table: "Tickets");
        }
    }
}
