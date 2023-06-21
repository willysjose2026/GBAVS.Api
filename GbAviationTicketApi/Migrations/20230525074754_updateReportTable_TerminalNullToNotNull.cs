using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GbAviationTicketApi.Migrations
{
    /// <inheritdoc />
    public partial class updateReportTable_TerminalNullToNotNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TICKETS_REPORT_TERMINALS_TerminalId",
                table: "TICKETS_REPORT");

            migrationBuilder.DropIndex(
                name: "IX_TICKETS_REPORT_TerminalId",
                table: "TICKETS_REPORT");

            migrationBuilder.DropColumn(
                name: "TerminalId",
                table: "TICKETS_REPORT");

            migrationBuilder.AddColumn<int>(
                name: "TerminalId",
                table: "TICKETS_REPORT",
                type: "int",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_TICKETS_REPORT_TerminalId",
                table: "TICKETS_REPORT",
                column: "TerminalId");

            migrationBuilder.AddForeignKey(
                name: "FK_TICKETS_REPORT_TERMINALS_TerminalId",
                table: "TICKETS_REPORT",
                column: "TerminalId",
                principalTable: "TERMINALS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
    name: "FK_TICKETS_REPORT_TERMINALS_TerminalId",
    table: "TICKETS_REPORT");

            migrationBuilder.DropIndex(
                name: "IX_TICKETS_REPORT_TerminalId",
                table: "TICKETS_REPORT");

            migrationBuilder.DropColumn(
                name: "TerminalId",
                table: "TICKETS_REPORT");

            migrationBuilder.AddColumn<int>(
                name: "TerminalId",
                table: "TICKETS_REPORT",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TICKETS_REPORT_TerminalId",
                table: "TICKETS_REPORT",
                column: "TerminalId");

            migrationBuilder.AddForeignKey(
                name: "FK_TICKETS_REPORT_TERMINALS_TerminalId",
                table: "TICKETS_REPORT",
                column: "TerminalId",
                principalTable: "TERMINALS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
