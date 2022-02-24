namespace Permissions.Utils
{
    public static class PermissionsManager
    {
        public static List<string> CreateModulePermissions(string module)
        {
            return new List<string>
            {
                $"Permissions.{module}.Read",
                $"Permissions.{module}.Create",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
            };
        }
    }
}
