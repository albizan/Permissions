using Permissions.Data;
using Permissions.Models;

namespace Permissions.Seeds
{
    public class SeedPerks
    {
        public static void Seed(ApplicationDbContext db)
        {
            if (db.Perks == null)
                throw new Exception("Perks prop is null, abort seed");

            // If weapons table is empty, insert initial data
            if (db.Perks.Any())
                return;

            db.Perks.AddRange(
                new Perk
                {
                    Name = "Fire",
                    Bonus = 16
                },
                new Perk
                {
                    Name = "Wind",
                    Bonus = 4
                },
                new Perk
                {
                    Name = "Water",
                    Bonus = 9
                },
                new Perk
                {
                    Name = "Earth",
                    Bonus = 12
                }
            );

            db.SaveChanges();
        }
    }
}
