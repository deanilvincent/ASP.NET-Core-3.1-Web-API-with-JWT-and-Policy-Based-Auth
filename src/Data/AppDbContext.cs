using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using src.Models.UserAccount;

namespace src.Data
{
    public class AppDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole,
        IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            // user seedings
            builder.Entity<Role>().HasData(new List<Role>
            {
                new Role {
                    Id = 1,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new Role {
                    Id = 2,
                    Name = "Staff",
                    NormalizedName = "STAFF"
                },
            });

            var hasher = new PasswordHasher<User>();
            builder.Entity<User>().HasData(
                new User
                {
                    Id = 1, // primary key
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    PasswordHash = hasher.HashPassword(null, "temppass01"),

                },
                new User
                {
                    Id = 2, // primary key
                    UserName = "staff",
                    NormalizedUserName = "STAFF",
                    PasswordHash = hasher.HashPassword(null, "temppass01"),
                }
            );

            builder.Entity<UserRole>().HasData(
                new UserRole
                {
                    RoleId = 1, // for admin username
                    UserId = 1  // for admin role
                },
                new UserRole
                {
                    RoleId = 2, // for staff username
                    UserId = 2  // for staff role
                }
            );
        }
    }
}