using Microsoft.AspNetCore.Identity;

namespace Permissions.Seeds
{
    public static class SeedManager
    {
        public static async Task Seed(IServiceProvider services, string password)
        {
            // Get seed identity managers
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            // Seed roles
            await SeedRoles.SeedAsync(roleManager);
            // Seed users
            await SeedUsers.SeedAsync(userManager, password);
        }
    }
}
