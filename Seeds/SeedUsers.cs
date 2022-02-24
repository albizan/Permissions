using Microsoft.AspNetCore.Identity;
using Permissions.Constants;
using Permissions.Utils;
using System.Security.Claims;

namespace Permissions.Seeds
{
    public static class SeedUsers
    {
        public static async Task SeedAsync(UserManager<IdentityUser> userManager, string password)
        {
            await SeedUser(userManager, Roles.User.ToString().ToLower(), password);
            await SeedUser(userManager, Roles.Admin.ToString().ToLower(), password);
        }
        private static async Task SeedUser(UserManager<IdentityUser> userManager, string role, string password)
        {
            var newUser = new IdentityUser
            {
                UserName = $"{role}@gmail.com",
                Email = $"{role}@gmail.com",
                EmailConfirmed = true
            };
            // Check if user already exists in the database
            if (userManager.Users.Any(user => user.Email == newUser.Email))
                return;

            await userManager.CreateAsync(newUser, password);
            await userManager.AddToRoleAsync(newUser, role);
        }
    }
}
