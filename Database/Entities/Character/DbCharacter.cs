using RpgAdventureGame.Database.SQLite.Base;
using RpgAdventureGame.Database.SQLite.Entities.Area;

namespace RpgAdventureGame.Database.SQLite.Entities.Character
{
    internal class DbCharacter : DbTable
    {
        public virtual string Name { get; set; }
        public virtual int CurrentAreaId { get; set; }
        public virtual DbArea CurrentArea { get; set; }
    }
}
