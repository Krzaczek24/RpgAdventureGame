using Krzaq.MediatR.Interfaces;
using RpgAdventureGame.Backend.Core.Exceptions;
using RpgAdventureGame.Database.SQLite.Entities.Character;

namespace RpgAdventureGame.Backend.Controllers.Queries.Character.GetDetails
{
    public class GetCharacterDetailsQueryHandler(IDbCharacterAccess characterAccess)
        : IRequestHandler<GetCharacterDetailsQuery, GetCharacterDetailsQueryResult>
    {
        public async ValueTask<GetCharacterDetailsQueryResult> Handle(GetCharacterDetailsQuery request)
        {
            var character = await characterAccess.Get(request.CharacterId)
                ?? throw new NotFoundException();

            return new() { CharacterDetails = character };
        }
    }
}
