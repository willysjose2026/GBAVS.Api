using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GbAviationTicketApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUsersTable_ChangedNameAndAddedRolesField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_GBAVS_USERS_GbavsUserId",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_TICKET_USER",
                table: "TICKETS");

            migrationBuilder.DropTable(
                name: "GBAVS_USERS");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_GbavsUserId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "GbavsUserId",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TerminalId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RoleId",
                table: "AspNetUsers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TerminalId",
                table: "AspNetUsers",
                column: "TerminalId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_RoleId",
                table: "AspNetUsers",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TERMINALS_TerminalId",
                table: "AspNetUsers",
                column: "TerminalId",
                principalTable: "TERMINALS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TICKET_USER",
                table: "TICKETS",
                column: "OPERATOR_USERNAME",
                principalTable: "AspNetUsers",
                principalColumn: "UserName");

            migrationBuilder.AddForeignKey(
                name: "FK_TICKETS_REPORT_AspNetUsers_AgentUserName",
                table: "TICKETS_REPORT",
                column: "AgentUserName",
                principalTable: "AspNetUsers",
                principalColumn: "UserName",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_TICKETS_REPORT_AspNetUsers_OperatorUserName",
                table: "TICKETS_REPORT",
                column: "OperatorUserName",
                principalTable: "AspNetUsers",
                principalColumn: "UserName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_RoleId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TERMINALS_TerminalId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_TICKET_USER",
                table: "TICKETS");

            migrationBuilder.DropForeignKey(
                name: "FK_TICKETS_REPORT_AspNetUsers_AgentUserName",
                table: "TICKETS_REPORT");

            migrationBuilder.DropForeignKey(
                name: "FK_TICKETS_REPORT_AspNetUsers_OperatorUserName",
                table: "TICKETS_REPORT");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RoleId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TerminalId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TerminalId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "GbavsUserId",
                table: "AspNetRoles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GBAVS_USERS",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TerminalId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FullName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GBAVS_USERS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GBAVS_USERS_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GBAVS_USERS_TERMINALS_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "TERMINALS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_GbavsUserId",
                table: "AspNetRoles",
                column: "GbavsUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GBAVS_USERS_TerminalId",
                table: "GBAVS_USERS",
                column: "TerminalId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_GBAVS_USERS_GbavsUserId",
                table: "AspNetRoles",
                column: "GbavsUserId",
                principalTable: "GBAVS_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TICKET_USER",
                table: "TICKETS",
                column: "OPERATOR_USERNAME",
                principalTable: "GBAVS_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TICKETS_REPORT_GBAVS_USERS_AgentUserName",
                table: "TICKETS_REPORT",
                column: "AgentUserName",
                principalTable: "GBAVS_USERS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TICKETS_REPORT_GBAVS_USERS_OperatorUserName",
                table: "TICKETS_REPORT",
                column: "OperatorUserName",
                principalTable: "GBAVS_USERS",
                principalColumn: "Id");
        }
    }
}
