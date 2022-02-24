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
            await AddAllPermissionsToRole(roleManager, adminRole, Modules.Products.ToString());

            var userRole = await roleManager.FindByNameAsync(Roles.User.ToString());
            await AddPermissionsToRole(roleManager, userRole, "Permissions.Products.Read");
        }

        public static async Task AddAllPermissionsToRole(RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            var modulePermissions = PermissionsManager.CreateModulePermissions(module);
            var currentClaims = await roleManager.GetClaimsAsync(role);
            var currentClaimValues = currentClaims.Where(c => c.Type == "Permission").Select(c => c.Value).ToArray();

            foreach (var permission in modulePermissions)
            {
                if(!currentClaimValues.Contains(permission))
                await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
            }
        }

        public static async Task AddPermissionsToRole(RoleManager<IdentityRole> roleManager, IdentityRole role, params string[] permissions)
        {
            var currentClaims = await roleManager.GetClaimsAsync(role);
            var currentClaimValues = currentClaims.Where(c => c.Type == "Permission").Select(c => c.Value).ToArray();

            foreach (var permission in permissions)
            {
                if (!currentClaimValues.Contains(permission))
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
            }

        }
    }
}
