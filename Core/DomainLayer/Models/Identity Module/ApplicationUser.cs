using Microsoft.AspNetCore.Identity;

namespace DomainLayer.Models.Identity_Module
{
    public class ApplicationUser:IdentityUser
    {
        public string DisplayName { get; set; } = null!;
        public Address? Address { get; set; }
    }
}
