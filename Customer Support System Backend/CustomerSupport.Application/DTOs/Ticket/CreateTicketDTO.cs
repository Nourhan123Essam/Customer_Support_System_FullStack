using CustomerSupport.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSupport.Application.DTOs.Ticket
{
    public class CreateTicketDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string CustomerSupportUserId { get; set; }
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
}
