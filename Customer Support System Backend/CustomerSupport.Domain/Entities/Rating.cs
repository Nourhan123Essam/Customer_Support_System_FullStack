using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSupport.Domain.Entities
{
    public class Rating
    {
        public int RatingId { get; set; }
        public int Score { get; set; } // Rating score, e.g., 1-5
        public string Feedback { get; set; }

        // Foreign keys
        public int TicketId { get; set; } // Foreign key for ticket
        public string UserId { get; set; } // Foreign key for the user who provided the rating
                                           
        // Navigation Property
        public Ticket Ticket { get; set; }
    }
}
