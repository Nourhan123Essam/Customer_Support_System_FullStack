using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSupport.Application.DTOs.Rating
{
    public class AddRatingDTO
    {
        public int TicketId { get; set; }
        public int Score { get; set; } // 1-5
        public string Feedback { get; set; }
    }
}
