using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSupport.Domain.Entities
{
    public class UserAssignedTicket
    {
        public int Id { get; set; }

        // Foreign keys
        public int? TicketId { get; set; } // Foreign key for ticket
        public string? SupportUserId { get; set; } // Foreign key for support user

        // Navigation Properties
        public Ticket? Ticket { get; set; }
    }
}
