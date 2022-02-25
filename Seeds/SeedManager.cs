using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Permissions.Data;

namespace Permissions.Seeds
{
    public static class SeedManager
    {
        public static async Task Seed(IServiceProvider services, string password)
        {
            // Get seed identity managers
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var db = new ApplicationDbContext(services.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            // Seed roles
            await SeedRoles.SeedAsync(roleManager);
            // Seed users
            await SeedUsers.SeedAsync(userManager, password);

            // Seed modules
            SeedWeapons.Seed(db);
        }
    }
}
