namespace Permissions.Utils
{
    public static class PermissionsManager
    {
        public static string[] CreateModulePermissions(string module)
        {
            return new string[]
            {
                $"Permissions.{module}.Read",
                $"Permissions.{module}.Create",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
            };
        }
    }
}
