using RpgAdventureGame.Database.SQLite.Base;
using RpgAdventureGame.Database.SQLite.Entities.Character;
using RpgAdventureGame.Database.SQLite.Entities.Path;

namespace RpgAdventureGame.Database.SQLite.Entities.Area
{
    internal class DbArea : DbTable
    {
        public virtual string Name { get; set; }
        public virtual ICollection<DbCharacter> Characters { get; set; }
        public virtual ICollection<DbPath> OutgoingPaths { get; set; }
    }
}
