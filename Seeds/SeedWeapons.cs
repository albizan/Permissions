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

            if (db.Perks == null || !db.Perks.Any())
                return;

            var earth = db.Perks.Where(p => p.Name == "Earth").FirstOrDefault();
            var fire = db.Perks.Where(p => p.Name == "Fire").FirstOrDefault();
            var water = db.Perks.Where(p => p.Name == "Water").FirstOrDefault();
            var wind = db.Perks.Where(p => p.Name == "Wind").FirstOrDefault();

            db.Weapons.AddRange(
                new Weapon
                {
                    Name = "Small Sword",
                    Damage = 10,
                    Level = 1,
                    Perks = new List<Perk>() { earth, fire, water, wind }   
                },
                new Weapon
                {
                    Name = "Medium Sword",
                    Damage = 20,
                    Level = 1,
                    Perks = new List<Perk>() { earth, water, wind }
                },
                new Weapon
                {
                    Name = "Big Sword",
                    Damage = 30,
                    Level = 1,
                    Perks = new List<Perk>() { earth, wind }
                },
                new Weapon
                {
                    Name = "Fire Sword",
                    Damage = 40,
                    Level = 1,
                    Perks = new List<Perk>() { water, wind }
                },
                new Weapon
                {
                    Name = "Master Sword",
                    Damage = 100,
                    Level = 1,
                    Perks = new List<Perk>() { earth }
                }
            );

            db.SaveChanges();
        }
    }
}
