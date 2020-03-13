using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace src.Models.UserAccount
{
    public class User : IdentityUser<int>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        // relationship
        public ICollection<UserRole> UserRoles { get; set; }
    }
}