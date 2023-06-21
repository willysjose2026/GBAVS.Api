using GbAviationTicketApi.Models;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GbAviationTicketApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReportTable_AddedIsActiveToUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UNIQUE_AGENT_AND_OP_AND_DATES_AND_TERMINALS",
                table: "TICKETS_REPORT");

            migrationBuilder.CreateIndex(
                name: "UNIQUE_AGENT_OP_DATES_TERMINALS_ISACTIVE",
                table: "TICKETS_REPORT",
                columns: new[] { nameof(ReportSummary.AgentUserName),
                    nameof(ReportSummary.OperatorUserName),
                    nameof(ReportSummary.TerminalId),
                    nameof(ReportSummary.StartDate),
                    nameof(ReportSummary.EndDate),
                    "IS_ACTIVE"},
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UNIQUE_AGENT_OP_DATES_TERMINALS_ISACTIVE",
                table: "TICKETS_REPORT");

            migrationBuilder.CreateIndex(
                name: "UNIQUE_AGENT_AND_OP_AND_DATES_AND_TERMINALS",
                table: "TICKETS_REPORT",
                columns: new[] {nameof(ReportSummary.AgentUserName),
                    nameof(ReportSummary.OperatorUserName),
                    nameof(ReportSummary.TerminalId),
                    nameof(ReportSummary.StartDate),
                    "IS_ACTIVE"},
                unique: true);
        }
    }
}
