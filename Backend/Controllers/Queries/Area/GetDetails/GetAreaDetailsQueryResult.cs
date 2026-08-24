using RpgAdventureGame.Database.SQLite.Models.Area;

namespace RpgAdventureGame.Backend.Controllers.Queries.Area.GetDetails
{
    public class GetAreaDetailsQueryResult
    {
        public required SelectAreaDetailsDto AreaDetails { get; set; }
    }
}
