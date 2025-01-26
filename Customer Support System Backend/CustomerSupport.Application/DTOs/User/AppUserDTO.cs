
using System.ComponentModel.DataAnnotations;

namespace CustomerSupport.Application.DTOs.User
{
    public record AppUserDTO
    (
        string Id,
        [Required] string Name,
        [Required] string TelephoneNumber,
        [Required, EmailAddress] string Email,
        [Required] string Password,
        [Required] string Role
    );
}
