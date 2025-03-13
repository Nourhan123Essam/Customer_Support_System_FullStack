using CustomerSupport.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSupport.Infrastructure.Data
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyEntityRelationships(this ModelBuilder modelBuilder)
        {
            /////////// User Relationships /////////////////////////////////////////////

            // Relationship between Ticket and User (Customer)
            modelBuilder.Entity<Ticket>()
                .HasOne<IdentityUser>()
                .WithMany()
                .HasForeignKey(t => t.CustomerUserId)
                .OnDelete(DeleteBehavior.SetNull); // Keep ticket for records if user is deleted

            // Relationship between Note and User (User who created the Note)
            modelBuilder.Entity<Note>()
                .HasOne<IdentityUser>()
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.SetNull); // Keep notes for historical/legal purposes

            // Relationship between UserAssignedTickets and User (Logging Table)
            modelBuilder.Entity<UserAssignedTicket>()
                .HasOne<IdentityUser>()
                .WithMany()
                .HasForeignKey(uat => uat.SupportUserId)
                .OnDelete(DeleteBehavior.Cascade); // If the user is deleted, remove logging records

            // Ticket and Customer Support User
            modelBuilder.Entity<Ticket>()
                .HasOne<IdentityUser>()
                .WithMany()
                .HasForeignKey(t => t.CustomerSupportUserId)
                .OnDelete(DeleteBehavior.SetNull); // Keep ticket, remove support user reference

            // Rating and User
            modelBuilder.Entity<Rating>()
                .HasOne<IdentityUser>()
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.SetNull); // Keep rating records for analytics

            /////////// Ticket Relationships ///////////////////////////////////////////

            // Relationship between Ticket and Note
            modelBuilder.Entity<Note>()
                .HasOne(n => n.Ticket)
                .WithMany(t => t.Notes)
                .HasForeignKey(n => n.TicketId)
                .OnDelete(DeleteBehavior.SetNull); // Keep notes for historical/legal purposes

            // Relationship between Ticket and Attachment
            modelBuilder.Entity<Attachment>()
                .HasOne(a => a.Ticket)
                .WithMany(t => t.Attachments)
                .HasForeignKey(a => a.TicketId)
                .OnDelete(DeleteBehavior.Cascade); // Attachments should be deleted if the ticket is deleted

            // Ticket and Rating (1-to-1)
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Rating)
                .WithOne(r => r.Ticket)
                .HasForeignKey<Rating>(r => r.TicketId)
                .OnDelete(DeleteBehavior.SetNull); // Keep rating records even if the ticket is deleted

            // Ticket and Category (Many-to-1)
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Tickets)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.SetNull); // Uncategorize ticket instead of deleting it

            // Ticket and UserAssignedTickets (1-to-Many)
            modelBuilder.Entity<Ticket>()
                .HasMany(t => t.UserAssignedTickets)
                .WithOne(ua => ua.Ticket)
                .HasForeignKey(ua => ua.TicketId)
                .OnDelete(DeleteBehavior.Cascade); // Remove all logs if ticket is deleted

            ///////////////////////////////////////////////////////////////////////////
            // Relationship between Note and Attachment
            modelBuilder.Entity<Attachment>()
                .HasOne(a => a.Note)
                .WithMany(n => n.Attachments)
                .HasForeignKey(a => a.NoteId)
                .OnDelete(DeleteBehavior.Cascade); // Attachments should be deleted if the note is deleted

        }
    }
}
