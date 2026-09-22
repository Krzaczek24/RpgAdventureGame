using RpgAdventureGame.Database.SQLite.Models.Area;
using RpgAdventureGame.Database.SQLite.Models.Path;
using System.Collections.ObjectModel;

namespace RpgAdventureGame.Backend.Controllers.Queries.Area.GetAvailablePaths
{
    public class GetAreaAvailablePathsQueryResult
    {
        public required ReadOnlySet<SelectAreaPathListItemDto> Paths { get; set; }
    }
}
