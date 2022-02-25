using Microsoft.AspNetCore.Authorization;
using Permissions.Constants;

namespace Permissions.Authorization
{
    public class NeedPermission: AuthorizeAttribute
    {
        private const string POLICY_PREFIX = "Permissions";
        public NeedPermission(Modules module, Operations operation)
        {
            Policy = $"{POLICY_PREFIX}.{module.ToString()}.{operation.ToString()}";
        }
    }
}
