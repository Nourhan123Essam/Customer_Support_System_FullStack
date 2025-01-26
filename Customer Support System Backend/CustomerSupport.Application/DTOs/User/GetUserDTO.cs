using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSupport.Application.DTOs.User
{
    public record GetUserDTO
    (
        string Id,
        [Required] string Name,
        [Required] string TelephoneNumber,
        [Required, EmailAddress] string Email,
        [Required] string Role
    );
}
