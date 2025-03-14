using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerSupport.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixMultipleCascadePaths : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_CustomerUserId",
                table: "Tickets");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_CustomerUserId",
                table: "Tickets",
                column: "CustomerUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_CustomerUserId",
                table: "Tickets");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_CustomerUserId",
                table: "Tickets",
                column: "CustomerUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
