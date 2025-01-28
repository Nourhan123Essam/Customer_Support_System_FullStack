using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSupport.Application.DTOs.Note
{
    public class AddNoteDTO
    {
        public int TicketId { get; set; }
        public string Content { get; set; }
    }

}
