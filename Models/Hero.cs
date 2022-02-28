namespace Permissions.Models
{
    public class Hero
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Health { get; set; }
        public int Attack { get; set; }
        public Weapon? Weapon { get; set; }
    }
}
