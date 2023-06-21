using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GbAviationTicketApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReportsTable_FixedNullables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TICKETS_REPORT");

            migrationBuilder.CreateTable(
                name: "TICKETS_REPORT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OperatorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TerminalId = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TICKETS_REPORT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TICKETS_REPORT_GBAVS_USERS_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "GBAVS_USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TICKETS_REPORT_GBAVS_USERS_UserId",
                        column: x => x.UserId,
                        principalTable: "GBAVS_USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TICKETS_REPORT_TERMINALS_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "TERMINALS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TICKETS_REPORT_OperatorId",
                table: "TICKETS_REPORT",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_TICKETS_REPORT_TerminalId",
                table: "TICKETS_REPORT",
                column: "TerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_TICKETS_REPORT_UserId",
                table: "TICKETS_REPORT",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TICKETS_REPORT");
        }
    }
}
