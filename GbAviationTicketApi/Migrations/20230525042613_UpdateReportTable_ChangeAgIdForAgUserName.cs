using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GbAviationTicketApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReportTable_ChangeAgIdForAgUserName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TICKETS_REPORT_GBAVS_USERS_UserId",
                table: "TICKETS_REPORT");

            migrationBuilder.DropIndex(
                name: "IX_TICKETS_REPORT_UserId",
                table: "TICKETS_REPORT");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TICKETS_REPORT");

            migrationBuilder.AddColumn<string>(
                name: "AgentUserName",
                table: "TICKETS_REPORT",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TICKETS_REPORT_AgentUserName",
                table: "TICKETS_REPORT",
                column: "AgentUserName");

            migrationBuilder.AddForeignKey(
                name: "FK_TICKETS_REPORT_AspNetUsers_AgentUserName",
                table: "TICKETS_REPORT",
                column: "AgentUserName",
                principalTable: "AspNetUsers",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TICKETS_REPORT_AspNetUsers_AgentUserName",
                table: "TICKETS_REPORT");

            migrationBuilder.DropIndex(
                name: "IX_TICKETS_REPORT_AgentUserName",
                table: "TICKETS_REPORT");

            migrationBuilder.DropColumn(
                name: "AgentUserName",
                table: "TICKETS_REPORT");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TICKETS_REPORT",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TICKETS_REPORT_UserId",
                table: "TICKETS_REPORT",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TICKETS_REPORT_GBAVS_USERS_UserId",
                table: "TICKETS_REPORT",
                column: "UserId",
                principalTable: "GBAVS_USERS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
