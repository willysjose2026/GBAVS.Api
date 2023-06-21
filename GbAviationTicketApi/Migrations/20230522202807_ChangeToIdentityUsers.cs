using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GbAviationTicketApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangeToIdentityUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CUSTOMERS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CUSTOMER_NAME = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    EMAIL = table.Column<string>(type: "varchar(75)", unicode: false, maxLength: 75, nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CUSTOMER__3214EC27192B8D23", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PAYMENTMTHD",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PMTD_NAME = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PAYMENTM__3214EC27790F6D2C", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCTS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    PRODUCT_NAME = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PRODUCTS__3214EC27149EBD54", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TERMINALS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    TERMINAL_NAME = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    TERMINAL_LOC = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TERMINAL__3214EC2781EFF9AF", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GBAVS_USERS",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    TerminalId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GBAVS_USERS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GBAVS_USERS_TERMINALS_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "TERMINALS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TICKETS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TICKET_NO = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CUSTOMER_ID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    PRODUCT_ID = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    TERMINAL_ID = table.Column<int>(type: "int", nullable: false),
                    PAYMENT_MTHD_ID = table.Column<int>(type: "int", nullable: false),
                    OPERATOR_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ORDER_DATE = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    PRODUCT_QTY = table.Column<decimal>(type: "decimal(38,4)", nullable: false),
                    AIRCRAFT_TYPE = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    TAIL_NO = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    FLIGHT_NO = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    F_FROM = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    F_TO = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    UNIT_NO = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    INIT_TIME = table.Column<TimeSpan>(type: "time", nullable: false),
                    END_TIME = table.Column<TimeSpan>(type: "time", nullable: false),
                    TEMPERATURE = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    IS_CLEAR_AND_BRIGHT = table.Column<bool>(type: "bit", nullable: false),
                    IS_WATER_FREE = table.Column<bool>(type: "bit", nullable: false),
                    IS_PARTICLE_FREE = table.Column<bool>(type: "bit", nullable: false),
                    API_DENSITY = table.Column<int>(type: "int", nullable: true),
                    PIT_NO = table.Column<int>(type: "int", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TICKET_ID", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TICKET_CUST",
                        column: x => x.CUSTOMER_ID,
                        principalTable: "CUSTOMERS",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TICKET_PAYMTHD",
                        column: x => x.PAYMENT_MTHD_ID,
                        principalTable: "PAYMENTMTHD",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TICKET_PROD",
                        column: x => x.PRODUCT_ID,
                        principalTable: "PRODUCTS",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TICKET_TERMINAL",
                        column: x => x.TERMINAL_ID,
                        principalTable: "TERMINALS",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TICKET_USER",
                        column: x => x.OPERATOR_ID,
                        principalTable: "GBAVS_USERS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GBAVS_USERS_TerminalId",
                table: "GBAVS_USERS",
                column: "TerminalId");

            migrationBuilder.CreateIndex(
                name: "UQ__PRODUCTS__6FDD6CA311B83EC2",
                table: "PRODUCTS",
                column: "PRODUCT_NAME",
                unique: true,
                filter: "[PRODUCT_NAME] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__TERMINAL__71A915CC35D06BB8",
                table: "TERMINALS",
                column: "TERMINAL_NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TICKETS_CUSTOMER_ID",
                table: "TICKETS",
                column: "CUSTOMER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TICKETS_OPERATOR_ID",
                table: "TICKETS",
                column: "OPERATOR_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TICKETS_PAYMENT_MTHD_ID",
                table: "TICKETS",
                column: "PAYMENT_MTHD_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TICKETS_PRODUCT_ID",
                table: "TICKETS",
                column: "PRODUCT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TICKETS_TERMINAL_ID",
                table: "TICKETS",
                column: "TERMINAL_ID");

            migrationBuilder.CreateIndex(
                name: "UQ__TICKETS__7F5E594C4B78BCDC",
                table: "TICKETS",
                column: "TICKET_NO",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TICKETS");

            migrationBuilder.DropTable(
                name: "CUSTOMERS");

            migrationBuilder.DropTable(
                name: "PAYMENTMTHD");

            migrationBuilder.DropTable(
                name: "PRODUCTS");

            migrationBuilder.DropTable(
                name: "GBAVS_USERS");

            migrationBuilder.DropTable(
                name: "TERMINALS");
        }
    }
}
