using System.ComponentModel.DataAnnotations;

namespace Shared.DTOS.IdentityDtos
{
    public class RegisterDto
    {
        [EmailAddress]
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
