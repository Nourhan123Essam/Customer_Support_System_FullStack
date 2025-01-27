using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSupport.Domain.Entities
{
    public class Note
    {
        public int NoteId { get; set; }
        public string Content { get; set; }
        public bool IsVisibleToCustomer { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign keys
        public int TicketId { get; set; }
        public string UserId { get; set; }

        // Navigation Property
        public Ticket Ticket { get; set; }
    }
}
