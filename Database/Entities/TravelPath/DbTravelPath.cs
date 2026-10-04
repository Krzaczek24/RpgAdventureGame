using RpgAdventureGame.Database.SQLite.Base;
using RpgAdventureGame.Database.SQLite.Entities.Path;
using RpgAdventureGame.Database.SQLite.Entities.Travel;

namespace RpgAdventureGame.Database.SQLite.Entities.TravelPath
{
    public class DbTravelPath : DbTable
    {
        public virtual int TravelId { get; set; }
        public virtual DbTravel Travel { get; set; }
        public virtual int Sequence { get; set; }
        public virtual int PathId { get; set; }
        public virtual DbPath Path { get; set; }
    }
}
