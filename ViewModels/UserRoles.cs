namespace Permissions.ViewModels
{
    public class UserRoles
    {
        public string UserId { get; set; }
        public ICollection<UserRole> Roles { get; set; }
    }
}
