using Krzaq.MediatR.Interfaces;
using RpgAdventureGame.Database.SQLite.Entities.Character;

namespace RpgAdventureGame.Backend.CQRS.Queries.Character.Search
{
    public class SearchCharactersQueryHandler(IDbCharacterAccess characterAccess)
        : IRequestHandler<SearchCharactersQuery, SearchCharactersQueryResult>
    {
        public async ValueTask<SearchCharactersQueryResult> Handle(SearchCharactersQuery request)
        {
            var areaCharacters = await characterAccess.Search(request);
            return new()
            {
                Characters = areaCharacters.Select(character => new SearchCharacterDto
                {
                    Id = character.Id,
                    Name = character.Name,
                }).ToHashSet().AsReadOnly(),
            };
        }
    }
}
