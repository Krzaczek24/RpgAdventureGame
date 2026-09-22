using RpgAdventureGame.Database.SQLite.Models.Area;
using System.Collections.ObjectModel;

namespace RpgAdventureGame.Backend.Controllers.Queries.Area.List
{
    public class ListAreasQueryResult
    {
        public required ReadOnlySet<SelectAreaListItemDto> Areas { get; set; }
    }
}
