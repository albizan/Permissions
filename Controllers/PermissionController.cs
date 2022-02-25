using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Permissions.ViewModels;
using Permissions.Utils;
using Permissions.Constants;


namespace Permissions.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PermissionController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public PermissionController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> IndexAsync(string roleId)
        {
            if (roleId == null)
                return NotFound(roleId);
            var allPermissions = new List<RoleClaim>();

            // Create a list of permissions for every module (entity) of the app
            // Defa
            // Entities: Weapons, Heroes...
            var allPossibleClaims = new List<RoleClaim>();
            var allModules = Enum.GetNames<Modules>();
            foreach(var module in allModules)
            {
                var modulePermissions = PermissionsManager.CreateModulePermissions(module);
                allPossibleClaims.AddRange(modulePermissions.Select(mp => new RoleClaim() { Type = "Permission", Value = mp, IsSelected = false }));
            }
            
            // Get current permissions for given role
            var role = await _roleManager.FindByIdAsync(roleId);
            var currentClaims = await _roleManager.GetClaimsAsync(role);
            var currentClaimValues = currentClaims.Select(a => a.Value).ToList();
            var allPossibleClaimValues = allPossibleClaims.Select(a => a.Value).ToList();

            foreach(var permission in allPossibleClaims)
            {
                if(currentClaimValues.Contains(permission.Value))
                {
                    permission.IsSelected = true;
                }
            }

            // Create model to send to view
            var model = new RoleClaims()
            {
                Id=roleId,
                Claims=allPossibleClaims
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleClaims model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index", new { roleId = model.Id });

            var role = await _roleManager.FindByIdAsync(model.Id);
            if(role.ToString() == Roles.Admin.ToString())
            {
                return RedirectToAction("Index", new { roleId = model.Id });
            }
            var claims = await _roleManager.GetClaimsAsync(role);
            claims = claims.Where(c => c.Type == "Permission").ToList();
            // Remove all permission claims, they will be update later in this method
            foreach (var claim in claims)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }


            var selectedPermissions = model.Claims.Where(a => a.IsSelected).ToList();
            foreach (var permission in selectedPermissions)
            {
                Claim claim = new Claim("Permission", permission.Value);

                await _roleManager.AddClaimAsync(role, claim);
            }
            return RedirectToAction("Index", "Role");
        }
    }
}
