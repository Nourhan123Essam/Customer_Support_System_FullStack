using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSupport.Domain.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } // e.g., "Bug", "Feature Request"

        // Navigation Property
        public ICollection<Ticket>? Tickets { get; set; }
    }
}
