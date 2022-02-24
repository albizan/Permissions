namespace Permissions.ViewModels
{
    public class UserRoles
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public List<UserRole> Roles { get; set; }
    }
}
