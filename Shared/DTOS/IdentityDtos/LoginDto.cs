using System.ComponentModel.DataAnnotations;

namespace Shared.DTOS.IdentityDtos
{
    public class LoginDto
    {
        [EmailAddress]
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
