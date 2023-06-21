using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GbAviationTicketApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTicketTable_ChangedOpIdForOpUserName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TICKET_USER",
                table: "TICKETS");

            migrationBuilder.DropIndex(
                name: "IX_TICKETS_OPERATOR_ID",
                table: "TICKETS");

            migrationBuilder.DropColumn(
                name: "OPERATOR_ID",
                table: "TICKETS");

            migrationBuilder.AddColumn<string>(
                name: "OPERATOR_USERNAME",
                table: "TICKETS",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TICKETS_OPERATOR_USERNAME",
                table: "TICKETS",
                column: "OPERATOR_USERNAME");

            migrationBuilder.AddForeignKey(
                name: "FK_TICKET_USER",
                table: "TICKETS",
                column: "OPERATOR_USERNAME",
                principalTable: "AspNetUsers",
                principalColumn: "UserName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TICKET_USER",
                table: "TICKETS");

            migrationBuilder.DropIndex(
                name: "IX_TICKETS_OPERATOR_USERNAME",
                table: "TICKETS");

            migrationBuilder.DropColumn(
                name: "OPERATOR_USERNAME",
                table: "TICKETS");

            migrationBuilder.AddColumn<string>(
                name: "OPERATOR_ID",
                table: "TICKETS",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TICKETS_OPERATOR_ID",
                table: "TICKETS",
                column: "OPERATOR_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TICKET_USER",
                table: "TICKETS",
                column: "OPERATOR_ID",
                principalTable: "GBAVS_USERS",
                principalColumn: "Id");
        }
    }
}
