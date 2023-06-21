using GbAviationTicketApi.Models;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GbAviationTicketApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReportTable_CreateUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "UNIQUE_AGENT_AND_OP_AND_DATES_AND_TERMINALS",
                table: "TICKETS_REPORT",
                columns: new[] {nameof(ReportSummary.AgentUserName), 
                    nameof(ReportSummary.OperatorUserName), 
                    nameof(ReportSummary.TerminalId),
                    nameof(ReportSummary.StartDate),
                    nameof(ReportSummary.EndDate)},
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UNIQUE_AGENT_AND_OP_AND_DATES_AND_TERMINALS",
                table: "TICKETS_REPORT");

        }
    }
}
