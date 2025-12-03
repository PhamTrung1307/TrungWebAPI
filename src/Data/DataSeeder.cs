using Core.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace Data
{
    public class DataSeeder
    {
        public async Task SeedAsync(IDbContext context)
        {
            var passwordHasher = new PasswordHasher<AppUser>();

            var rootAdminRoleId = Guid.NewGuid();
            if(!context.Roles.Any())
            {
                await context.Roles.AddAsync(new AppRole
                {
                    Id = rootAdminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    DisplayName = "administrator"
                });
                await context.SaveChangesAsync();
            }

            if(!context.Users.Any())
            {
                var userId= Guid.NewGuid();
                var user = new AppUser()
                {
                    Id = userId,
                    FirstName = "Trung",
                    LastName = "Pham",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "trung@gmail.com",
                    NormalizedEmail = "TRUNG@GMAIL.COM",
                    IsActive = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    LockoutEnabled =false,
                    DateCreated = DateTime.UtcNow
                };
                user.PasswordHash = passwordHasher.HashPassword(user, "Admin@123");
                await context.Users.AddAsync(user);
                await context.UserRoles.AddAsync(new IdentityUserRole<Guid>
                {
                    RoleId = rootAdminRoleId,
                    UserId = userId
                });
                await context.SaveChangesAsync();
            }
        }
    }
}
