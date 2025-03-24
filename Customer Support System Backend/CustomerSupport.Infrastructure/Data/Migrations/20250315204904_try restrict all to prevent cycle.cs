using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerSupport.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class tryrestrictalltopreventcycle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Notes_NoteId",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Tickets_TicketId",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_AspNetUsers_UserId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Tickets_TicketId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AspNetUsers_UserId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Tickets_TicketId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_CustomerSupportUserId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_CustomerUserId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Categories_CategoryId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAssignedTickets_AspNetUsers_SupportUserId",
                table: "UserAssignedTickets");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAssignedTickets_Tickets_TicketId",
                table: "UserAssignedTickets");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Notes_NoteId",
                table: "Attachments",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Tickets_TicketId",
                table: "Attachments",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_AspNetUsers_UserId",
                table: "Notes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Tickets_TicketId",
                table: "Notes",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_AspNetUsers_UserId",
                table: "Ratings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Tickets_TicketId",
                table: "Ratings",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_CustomerSupportUserId",
                table: "Tickets",
                column: "CustomerSupportUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_CustomerUserId",
                table: "Tickets",
                column: "CustomerUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Categories_CategoryId",
                table: "Tickets",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssignedTickets_AspNetUsers_SupportUserId",
                table: "UserAssignedTickets",
                column: "SupportUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssignedTickets_Tickets_TicketId",
                table: "UserAssignedTickets",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Notes_NoteId",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Tickets_TicketId",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_AspNetUsers_UserId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Tickets_TicketId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AspNetUsers_UserId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Tickets_TicketId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_CustomerSupportUserId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_CustomerUserId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Categories_CategoryId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAssignedTickets_AspNetUsers_SupportUserId",
                table: "UserAssignedTickets");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAssignedTickets_Tickets_TicketId",
                table: "UserAssignedTickets");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Notes_NoteId",
                table: "Attachments",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "NoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Tickets_TicketId",
                table: "Attachments",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_AspNetUsers_UserId",
                table: "Notes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Tickets_TicketId",
                table: "Notes",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_AspNetUsers_UserId",
                table: "Ratings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Tickets_TicketId",
                table: "Ratings",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_CustomerSupportUserId",
                table: "Tickets",
                column: "CustomerSupportUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_CustomerUserId",
                table: "Tickets",
                column: "CustomerUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Categories_CategoryId",
                table: "Tickets",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssignedTickets_AspNetUsers_SupportUserId",
                table: "UserAssignedTickets",
                column: "SupportUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssignedTickets_Tickets_TicketId",
                table: "UserAssignedTickets",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id");
        }
    }
}
