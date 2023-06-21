using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GbAviationTicketApi.Migrations
{
    /// <inheritdoc />
    public partial class addedRolesToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GbavsUserId",
                table: "AspNetRoles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_GbavsUserId",
                table: "AspNetRoles",
                column: "GbavsUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_GBAVS_USERS_GbavsUserId",
                table: "AspNetRoles",
                column: "GbavsUserId",
                principalTable: "GBAVS_USERS",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_GBAVS_USERS_GbavsUserId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_GbavsUserId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "GbavsUserId",
                table: "AspNetRoles");
        }
    }
}
