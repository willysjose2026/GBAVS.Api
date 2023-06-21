using GbAviationTicketApi.Models;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GbAviationTicketApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReportTable_AddedCreatedAtToUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UNIQUE_AGENT_OP_DATES_TERMINALS_ISACTIVE",
                table: "TICKETS_REPORT");

            migrationBuilder.CreateIndex(
                name: "UNIQUE_AGENT_OP_DATES_TERMINALS_ISACTIVE_CREATED_AT",
                table: "TICKETS_REPORT",
                columns: new[] { nameof(ReportSummary.AgentUserName),
                    nameof(ReportSummary.OperatorUserName),
                    nameof(ReportSummary.TerminalId),
                    nameof(ReportSummary.StartDate),
                    nameof(ReportSummary.EndDate),
                    "IS_ACTIVE", "CREATED_AT"},
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UNIQUE_AGENT_OP_DATES_TERMINALS_ISACTIVE_CREATED_AT",
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
    }
}
