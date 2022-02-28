using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Permissions.Models;

namespace Permissions.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Weapon>? Weapons { get; set; }
        public DbSet<Perk>? Perks { get; set; }
        public DbSet<Hero> Heroes { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.LogTo(Console.WriteLine, LogLevel.Warning);
    }
}