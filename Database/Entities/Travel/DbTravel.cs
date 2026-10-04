using RpgAdventureGame.Backend.Database.SQLite.Base;
using RpgAdventureGame.Backend.Database.SQLite.Entities.Character;
using RpgAdventureGame.Backend.Database.SQLite.Entities.TravelPath;

namespace RpgAdventureGame.Backend.Database.SQLite.Entities.Travel
{
    public class DbTravel : DbTable
    {
        public virtual int CharacterId { get; set; }
        public virtual DbCharacter Character { get; set; }
        public virtual DateTime StartTimestamp { get; set; }
        public virtual bool Completed { get; set; }
        public virtual ICollection<DbTravelPath> TravelPaths { get; set; } = [];
    }
}
