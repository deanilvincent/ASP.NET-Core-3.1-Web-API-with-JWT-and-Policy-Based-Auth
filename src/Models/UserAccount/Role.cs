using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace src.Models.UserAccount
{
    public class Role : IdentityRole<int> 
    {
        // relationship
        public ICollection<UserRole> UserRoles { get; set; }
    }
}