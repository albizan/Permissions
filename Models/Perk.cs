namespace Permissions.Models
{
    public class Perk
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Bonus { get; set; }
        public IEnumerable<Weapon>? Weapons { get; set; }
    }
}
