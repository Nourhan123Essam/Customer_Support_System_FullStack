using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerSupport.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixMultipleCascadePathsForApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Tickets_TicketId",
                table: "Notes");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Tickets_TicketId",
                table: "Notes",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Tickets_TicketId",
                table: "Notes");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Tickets_TicketId",
                table: "Notes",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
