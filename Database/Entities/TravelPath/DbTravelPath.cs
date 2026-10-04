using RpgAdventureGame.Backend.Database.SQLite.Base;
using RpgAdventureGame.Backend.Database.SQLite.Entities.Path;
using RpgAdventureGame.Backend.Database.SQLite.Entities.Travel;

namespace RpgAdventureGame.Backend.Database.SQLite.Entities.TravelPath
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
