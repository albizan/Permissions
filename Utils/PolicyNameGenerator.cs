using Permissions.Constants;

namespace Permissions.Utils
{
    public static class PolicyNameGenerator
    {
        private const string POLICY_PREFIX = "Permissions";
        public static string Generate(Modules module, Operations operation)
        {
            return $"{POLICY_PREFIX}.{module}.{operation}";
        }
    }
}
