using Microsoft.AspNetCore.Authorization;

namespace Permissions.Authorization
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }
            if(context.User.Identity?.IsAuthenticated == null)
            {
                return Task.CompletedTask;
            }
            if(context.User.Claims == null || context.User.Claims.Count() <= 0)
            {
                return Task.CompletedTask;
            }
            var permissionClaim = context.User.Claims.Where(c => c.Type == "Permission" && c.Value == requirement.Permission).FirstOrDefault();
            if (permissionClaim != null)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
