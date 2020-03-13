## 1.0 Package installation instructions.

### 1.1 For dotnet migration and related tools
Using dotnet-cli
```
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
```

### 1.2 ASP.NET Core Identity for Entity Framework Core
Using dotnet-cli
```
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
```

### 1.3 Install `System.IdentityModel.Tokens.Jwt`
```
dotnet add package System.IdentityModel.Tokens.Jwt
```

## 2.0 Create models and inherit Identity class depending on their account definition.

### 2.1 User.cs
```
    public class User : IdentityUser<int>
    {
        // you can add custom properties here that aren't available in the IdentityUser class.
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
```

### 2.2 UserRole.cs
```
    public class UserRole : IdentityUserRole<int>
    {
        public User User { get; set; }

        public Role Role { get; set; }
    }
```

### 2.3 Role.cs
```
    public class Role : IdentityRole<int> 
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
```

## 3.0 Create DbContext and modify it.

### 3.1 Inherit the `IdentityDbContext` class. `IdentityDbContext` is derived from package `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
```
    public class AppDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole,
        IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
    }
```

### 3.2 Override the `OnModelCreating` method inside your `AppDbContext` class.
```
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
        }
```

## 4.0 Establish the connection string in the `Startup.cs`

### 4.1 Add `services.AddDbContext` with connection string inside the `ConfigureServices` method

```
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddDbContext(option=>option.UserSqlServer(@"Server=..."));
    }

```

## 4. Do the migration bro!.

Using dotnet-cli
```
dotnet ef migrations add MigrateIdentity

dotnet ef database update
```