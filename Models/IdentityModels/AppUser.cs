using Microsoft.AspNetCore.Identity;

namespace Models.IdentityModels
{
    public class AppUser : IdentityUser
    {
        public const string ROLE_ADMIN   = "Admin";
        public const string ROLE_REGULAR = "Regular";
    }
}
