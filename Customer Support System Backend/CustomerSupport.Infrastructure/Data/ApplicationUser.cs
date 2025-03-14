using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CustomerSupport.Infrastructure.Data
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsDeleted { get; set; } = false;  // Soft delete flag
    }
}
