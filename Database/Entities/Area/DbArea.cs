using RpgAdventureGame.Backend.Database.SQLite.Base;
using RpgAdventureGame.Backend.Database.SQLite.Entities.Character;
using RpgAdventureGame.Backend.Database.SQLite.Entities.Path;

namespace RpgAdventureGame.Backend.Database.SQLite.Entities.Area
{
    public class DbArea : DbTable
    {
        public virtual string Name { get; set; }
        public virtual ICollection<DbCharacter> Characters { get; set; } = [];
        public virtual ICollection<DbPath> OutgoingPaths { get; set; } = [];
    }
}
