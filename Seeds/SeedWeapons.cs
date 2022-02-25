using Permissions.Data;
using Permissions.Models;

namespace Permissions.Seeds
{
    public class SeedWeapons
    {
        public static void Seed(ApplicationDbContext db)
        {
            if (db.Weapons == null)
                throw new Exception("Weapons is null, abort seed");

            // If weapons table is empty, insert initial data
            if (db.Weapons.Any())
                return;

            db.Weapons.AddRange(
                new Weapon
                {
                    Name = "Small Sword",
                    Damage = 10,
                    Level = 1,
                },
                new Weapon
                {
                    Name = "Medium Sword",
                    Damage = 20,
                    Level = 1,
                },
                new Weapon
                {
                    Name = "Big Sword",
                    Damage = 30,
                    Level = 1,
                },
                new Weapon
                {
                    Name = "Fire Sword",
                    Damage = 40,
                    Level = 1,
                },
                new Weapon
                {
                    Name = "Master Sword",
                    Damage = 100,
                    Level = 1,
                }
            );

            db.SaveChanges();
        }
    }
}
