using RpgAdventureGame.Database.SQLite.Base;
using RpgAdventureGame.Database.SQLite.Entities.Character;
using RpgAdventureGame.Database.SQLite.Entities.TravelPath;

namespace RpgAdventureGame.Database.SQLite.Entities.Travel
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
