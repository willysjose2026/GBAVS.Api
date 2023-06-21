using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GbAviationTicketApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReportTable_changeOpIdForOpUserName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TICKETS_REPORT_GBAVS_USERS_OperatorId",
                table: "TICKETS_REPORT");

            migrationBuilder.DropIndex(
                name: "IX_TICKETS_REPORT_OperatorId",
                table: "TICKETS_REPORT");

            migrationBuilder.DropColumn(
                name: "OperatorId",
                table: "TICKETS_REPORT");

            migrationBuilder.AddColumn<string>(
                name: "OperatorUserName",
                table: "TICKETS_REPORT",
                type: "nvarchar(256)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TICKETS_REPORT_OperatorUserName",
                table: "TICKETS_REPORT",
                column: "OperatorUserName");

            migrationBuilder.AddForeignKey(
                name: "FK_TICKETS_REPORT_GBAVS_USERS_OperatorUserName",
                table: "TICKETS_REPORT",
                column: "OperatorUserName",
                principalTable: "AspNetUsers",
                principalColumn: "UserName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TICKETS_REPORT_GBAVS_USERS_OperatorUserName",
                table: "TICKETS_REPORT");

            migrationBuilder.DropIndex(
                name: "IX_TICKETS_REPORT_OperatorUserName",
                table: "TICKETS_REPORT");

            migrationBuilder.DropColumn(
                name: "OperatorUserName",
                table: "TICKETS_REPORT");

            migrationBuilder.AddColumn<string>(
                name: "OperatorId",
                table: "TICKETS_REPORT",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TICKETS_REPORT_OperatorId",
                table: "TICKETS_REPORT",
                column: "OperatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TICKETS_REPORT_GBAVS_USERS_OperatorId",
                table: "TICKETS_REPORT",
                column: "OperatorId",
                principalTable: "GBAVS_USERS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
