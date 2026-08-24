using RpgAdventureGame.Database.SQLite.Models.Character;

namespace RpgAdventureGame.Backend.Controllers.Queries.Character.GetDetails
{
    public class GetCharacterDetailsQueryResult
    {
        public required SelectCharacterDetailsDto CharacterDetails { get; set; }
    }
}
