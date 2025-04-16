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
            /////////// User Relationships/////////////////////////////////////////////

            // Relationship between Ticket and User (Customer)
            modelBuilder.Entity<Ticket>()
                .HasOne<IdentityUser>() // Specify the related entity type directly
                .WithMany()        // No navigation property on User for this
                .HasForeignKey(t => t.CustomerUserId) // Foreign key in Ticket
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship between Note and User (User who created the Note)
            modelBuilder.Entity<Note>()
                .HasOne<IdentityUser>()
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship between UserAssignedTickets and User
            modelBuilder.Entity<UserAssignedTicket>()
                .HasOne<IdentityUser>()
                .WithMany()
                .HasForeignKey(uat => uat.SupportUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ticket and Customer Support User
            modelBuilder.Entity<Ticket>()
                .HasOne<IdentityUser>()
                .WithMany()
                .HasForeignKey(t => t.CustomerSupportUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Rating and User
            modelBuilder.Entity<Rating>()
                .HasOne<IdentityUser>()
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            /////////// Ticket Relationships/////////////////////////////////////////////

            // Relationship between Ticket and Note
            modelBuilder.Entity<Note>()
                .HasOne(n => n.Ticket)
                .WithMany(t => t.Notes)
                .HasForeignKey(n => n.TicketId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship between Ticket and Attachment
            modelBuilder.Entity<Attachment>()
                .HasOne(a => a.Ticket)
                .WithMany(t => t.Attachments)
                .HasForeignKey(a => a.TicketId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ticket and Rating (1-to-1)
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Rating)
                .WithOne(r => r.Ticket)
                .HasForeignKey<Rating>(r => r.TicketId) // Explicitly define the foreign key
                .OnDelete(DeleteBehavior.Restrict); // Optional: Define delete behavior

            // Ticket and Category (Many-to-1)
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Tickets)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ticket and UserAssignedTickets (1-to-Many)
            modelBuilder.Entity<Ticket>()
                .HasMany(t => t.UserAssignedTickets)
                .WithOne(ua => ua.Ticket)
                .HasForeignKey(ua => ua.TicketId)
                .OnDelete(DeleteBehavior.Restrict);

            /////////////// Note and Attachment Relationships ////////////////////

            // Relationship between Note and Attachments
            modelBuilder.Entity<Attachment>()
                .HasOne(a => a.Note)
                .WithMany(n => n.Attachments)
                .HasForeignKey(a => a.NoteId)
                .OnDelete(DeleteBehavior.Restrict);
            /* 
               Reason: Attachments belong to a note. If the note is deleted, its attachments should be removed.
            */
        }


    }
}

