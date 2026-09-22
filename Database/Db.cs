using Microsoft.EntityFrameworkCore;
using RpgAdventureGame.Database.SQLite.Entities.Area;
using RpgAdventureGame.Database.SQLite.Entities.Character;
using RpgAdventureGame.Database.SQLite.Entities.Path;

namespace RpgAdventureGame.Database.SQLite
{
    public class Db(DbContextOptions<Db> options) : DbContext(options)
    {
        internal virtual DbSet<DbArea> Areas { get; set; }
        internal virtual DbSet<DbCharacter> Characters { get; set; }
        internal virtual DbSet<DbPath> Paths { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=E:\RpgAdventureGame\RpgAdventureGame.SQLite.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
