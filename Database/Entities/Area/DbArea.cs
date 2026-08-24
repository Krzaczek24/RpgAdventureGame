using RpgAdventureGame.Database.SQLite.Base;

namespace RpgAdventureGame.Database.SQLite.Entities.Area
{
    internal class DbArea : DbTable
    {
        public virtual string Name { get; set; }
    }
}
