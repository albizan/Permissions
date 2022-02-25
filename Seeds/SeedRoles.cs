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
            // Create default roles if they do not exist
            var role = await roleManager.FindByNameAsync(Roles.Admin.ToString());
            if(role == null)
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));

            role = await roleManager.FindByNameAsync(Roles.Helper.ToString());
            if(role == null)
                await roleManager.CreateAsync(new IdentityRole(Roles.Helper.ToString()));

            role = await roleManager.FindByNameAsync(Roles.User.ToString());
            if(role == null)
                await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));

            // Add default permissions for "all modules" (granted all) to admin role
            var adminRole = await roleManager.FindByNameAsync(Roles.Admin.ToString());
            foreach(var module in Enum.GetNames<Modules>())
                await AddPermissionsToRole(roleManager, adminRole, PermissionsManager.CreateModulePermissions(module));

            var userRole = await roleManager.FindByNameAsync(Roles.User.ToString());
            await AddPermissionsToRole(roleManager, userRole, $"Permissions.{Modules.Weapons}.Read");
        }

        public static async Task AddPermissionsToRole(RoleManager<IdentityRole> roleManager, IdentityRole role, params string[] permissions)
        {
            var newPermissions = await GetOnlyNewPermissions(roleManager, role, permissions);

            foreach (var permission in newPermissions)
            {
                await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
            }
        }

        public static async Task<List<string>> GetOnlyNewPermissions(RoleManager<IdentityRole> roleManager, IdentityRole role, params string[] permissions)
        {
            var currentClaims = await roleManager.GetClaimsAsync(role);
            var currentPermissions = currentClaims.Where(c => c.Type == "Permission").Select(c => c.Value).ToList();
            var newPermissions = new List<string>();

            foreach(var permission in permissions)
            {
                if (!currentPermissions.Contains(permission))
                    newPermissions.Add(permission);
            }

            return newPermissions;
        }
    }
}
