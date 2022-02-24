using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Permissions.ViewModels;

namespace Permissions.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserRolesController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRolesController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index(string userId)
        {
            var userRoles = new List<UserRole>();
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();
            var currentUserRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in _roleManager.Roles.ToList())
            {
                var userRoleVM = new UserRole
                {
                    RoleName = role.Name
                };
                if (currentUserRoles.Contains(role.Name))
                {
                    userRoleVM.IsSelected = true;
                }
                else
                {
                    userRoleVM.IsSelected = false;
                }
                userRoles.Add(userRoleVM);
            }

            var userRolesVM = new UserRoles()
            {
                Id = userId,
                Email = user.Email,
                Roles = userRoles
            };
            return View(userRolesVM);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(string userId, UserRoles userRolesVM)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            var receivedRoles = userRolesVM.Roles;

            // Remove all roles from user, they will be update later
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            if(receivedRoles==null)
                return RedirectToAction("Index", "User");
            Console.Write(receivedRoles);
            var newRoles = receivedRoles.Where(r => r.IsSelected).Select(r => r.RoleName);

            await _userManager.AddToRolesAsync(user, newRoles);

            return RedirectToAction("Index", "User");
        }
    }
}
