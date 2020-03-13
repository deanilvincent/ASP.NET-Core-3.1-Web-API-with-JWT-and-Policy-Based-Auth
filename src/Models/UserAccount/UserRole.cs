using Microsoft.AspNetCore.Identity;

namespace src.Models.UserAccount
{
    public class UserRole : IdentityUserRole<int>
    {
        public User User { get; set; }

        public Role Role { get; set; }
    }
}