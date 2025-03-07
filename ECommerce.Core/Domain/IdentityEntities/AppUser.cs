using Microsoft.AspNetCore.Identity;

namespace ECommerce.Core.Domain.IdentityEntities
{
    // Guid is used as the primary key for the user
    public class AppUser : IdentityUser<Guid>
    {
        public string Address { get; set; }

        public string FullName { get; set; }

    }
}
