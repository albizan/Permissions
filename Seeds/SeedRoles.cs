using Microsoft.AspNetCore.Identity;
using Permissions.Constants;
using Permissions.Utils;
using System.Security.Claims;

namespace Permissions.Seeds
{
    public static class SeedRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            // Create default roles
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Helper.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));

            // Add default permissions for "Products" (granted all) to admin role
            var adminRole = await roleManager.FindByNameAsync(Roles.Admin.ToString());
            await AddPermissionsToRole(roleManager, adminRole, "Products");
        }

        public static async Task AddPermissionsToRole(RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            var modulePermissions = PermissionsManager.CreateModulePermissions(module);

            foreach (var permission in modulePermissions)
            {
                await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
            }
        }
    }
}
