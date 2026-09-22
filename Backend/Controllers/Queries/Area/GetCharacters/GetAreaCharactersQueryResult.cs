using RpgAdventureGame.Database.SQLite.Models.Area;
using System.Collections.ObjectModel;

namespace RpgAdventureGame.Backend.Controllers.Queries.Area.GetCharacters
{
    public class GetAreaCharactersQueryResult
    {
        public required ReadOnlySet<SelectAreaCharacterListItemDto> Characters { get; set; }
    }
}
