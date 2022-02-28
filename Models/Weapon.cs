namespace Permissions.Models
{
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public int Damage { get; set; }
        public int Level { get; set; }
        public IEnumerable<Perk>? Perks { get; set; }
    }
}
