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
            /////////////// User Relationships ////////////////////

            // Relationship between Ticket and User (Customer)
            modelBuilder.Entity<Ticket>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(t => t.CustomerUserId)
                .OnDelete(DeleteBehavior.Restrict);
            /* 
               Reason: If a customer is deleted, we keep the ticket for reference.
            */

            // Relationship between Ticket and Customer Support User (Agent handling the ticket)
            modelBuilder.Entity<Ticket>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(t => t.CustomerSupportUserId)
                .OnDelete(DeleteBehavior.NoAction);
            /* 
               Reason: Prevents multiple cascade paths when both CustomerUserId and CustomerSupportUserId reference ApplicationUser.
               Instead, handle cleanup manually when deleting an employee.
            */

            // Relationship between Note and User (User who created the Note)
            modelBuilder.Entity<Note>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            /* 
               Reason: Notes act like chat messages; we keep them even if the user is deleted.
            */

            // Relationship between Rating and User (Who provided the rating)
            modelBuilder.Entity<Rating>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            /* 
               Reason: Keeping ratings for analytics even if the user is removed.
            */

            // Relationship between UserAssignedTickets (Logging table) and Support User
            modelBuilder.Entity<UserAssignedTicket>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(uat => uat.SupportUserId)
                .OnDelete(DeleteBehavior.NoAction);
            /* 
               Reason: This is a logging table, so if a user is deleted, their assignments should be deleted as well.
            */

            /////////////// Ticket Relationships ////////////////////

            // Relationship between Ticket and Notes
            modelBuilder.Entity<Note>()
                .HasOne(n => n.Ticket)
                .WithMany(t => t.Notes)
                .HasForeignKey(n => n.TicketId)
                .OnDelete(DeleteBehavior.NoAction);
            /* 
               Reason: Notes belong to a ticket. If a ticket is deleted, notes should be removed.
            */

            // Relationship between Ticket and Attachments
            modelBuilder.Entity<Attachment>()
                .HasOne(a => a.Ticket)
                .WithMany(t => t.Attachments)
                .HasForeignKey(a => a.TicketId)
                .OnDelete(DeleteBehavior.Cascade);
            /* 
               Reason: Attachments are specific to a ticket. If the ticket is deleted, attachments should be removed.
            */

            // Relationship between Ticket and Rating (1-to-1)
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Rating)
                .WithOne(r => r.Ticket)
                .HasForeignKey<Rating>(r => r.TicketId)
                .OnDelete(DeleteBehavior.NoAction);
            /* 
               Reason: Keeping rating records even if the ticket is deleted for historical analysis.
            */

            // Relationship between Ticket and Category (Many-to-1)
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Tickets)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);
            /* 
               Reason: If a category is deleted, tickets should remain uncategorized.
            */

            // Relationship between Ticket and UserAssignedTickets (1-to-Many)
            modelBuilder.Entity<Ticket>()
                .HasMany(t => t.UserAssignedTickets)
                .WithOne(ua => ua.Ticket)
                .HasForeignKey(ua => ua.TicketId)
                .OnDelete(DeleteBehavior.NoAction);
            /* 
               Reason: Logging table; if a ticket is deleted, logs should be removed.
            */

            /////////////// Note and Attachment Relationships ////////////////////

            // Relationship between Note and Attachments
            modelBuilder.Entity<Attachment>()
                .HasOne(a => a.Note)
                .WithMany(n => n.Attachments)
                .HasForeignKey(a => a.NoteId)
                .OnDelete(DeleteBehavior.Cascade);
            /* 
               Reason: Attachments belong to a note. If the note is deleted, its attachments should be removed.
            */
        }


    }
}

