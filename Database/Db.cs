using Microsoft.EntityFrameworkCore;
using RpgAdventureGame.Backend.Database.SQLite.Entities.Area;
using RpgAdventureGame.Backend.Database.SQLite.Entities.Character;
using RpgAdventureGame.Backend.Database.SQLite.Entities.Path;
using RpgAdventureGame.Backend.Database.SQLite.Entities.Travel;
using RpgAdventureGame.Backend.Database.SQLite.Entities.Worker;

namespace RpgAdventureGame.Backend.Database.SQLite
{
    public class Db(DbContextOptions<Db> options) : DbContext(options)
    {
        internal virtual DbSet<DbArea> Areas { get; set; }
        internal virtual DbSet<DbCharacter> Characters { get; set; }
        internal virtual DbSet<DbPath> Paths { get; set; }
        internal virtual DbSet<DbTravel> Travels { get; set; }
        internal virtual DbSet<DbWorker> Workers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Only configure default SQLite file when no other options were configured by the caller.
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(@"Data Source=E:\RpgAdventureGame\RpgAdventureGame.SQLite.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
