using CustomerSupport.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSupport.Domain.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }

        // Foreign keys
        public string? CustomerUserId { get; set; } // Foreign key for customer user
        public string? CustomerSupportUserId { get; set; } // Foreign key for assigned support user
        //public int? RatingId { get; set; } // Foreign key for rating
        public int? CategoryId { get; set; } // Foreign key for category

        // Navigation Properties
        public Rating? Rating { get; set; }
        public Category? Category { get; set; }
        public ICollection<UserAssignedTicket>? UserAssignedTickets { get; set; }
        public ICollection<Note>? Notes { get; set; }
        public ICollection<Attachment>? Attachments { get; set; }

    }
}
