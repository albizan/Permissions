using Microsoft.AspNetCore.Authorization;
using Permissions.Constants;
using Permissions.Utils;

namespace Permissions.Authorization
{
    public class NeedPermission: AuthorizeAttribute
    {
        public NeedPermission(Modules module, Operations operation)
        {
            Policy = PolicyNameGenerator.Generate(module, operation);
        }
    }
}
