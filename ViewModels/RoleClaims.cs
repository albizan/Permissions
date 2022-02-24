namespace Permissions.ViewModels
{
    public class RoleClaims
    {
        public string Id { get; set; }
        public IList<RoleClaim> Claims { get; set; }
    }
    public class RoleClaim
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public bool IsSelected { get; set; }
    }
}
