using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSupport.Domain.Entities
{
    public class Attachment
    {
        public int AttachmentId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.Now;

        // Foreign keys
        public int? TicketId { get; set; }
        public int? NoteId { get; set; }

        // Navigation Property
        public Ticket? Ticket { get; set; }
        public Note? Note { get; set; }
    }
}
