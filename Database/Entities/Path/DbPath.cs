using RpgAdventureGame.Database.SQLite.Base;
using RpgAdventureGame.Database.SQLite.Entities.Area;

namespace RpgAdventureGame.Database.SQLite.Entities.Path
{
    internal class DbPath : DbTable
    {
        public virtual string Name { get; set; }
        public virtual int StartAreaId { get; set; }
        public virtual DbArea StartArea { get; set; }
        public virtual int EndAreaId { get; set; }
        public virtual DbArea EndArea { get; set; }
        public virtual decimal Distance { get; set; }
        public virtual int DangerLevel { get; set; }
        public virtual decimal DangerProbability { get; set; }
    }
}
