using Krzaq.MediatR.Interfaces;
using RpgAdventureGame.Database.SQLite.Entities.Character;

namespace RpgAdventureGame.Backend.CQRS.Commands.Character.SetLocation
{
    public class SetCharacterLocationCommandHandler(IDbCharacterAccess characterAccess)
        : IRequestHandler<SetCharacterLocationCommand>
    {
        public async ValueTask Handle(SetCharacterLocationCommand request)
        {
            await characterAccess.SetLocation(request.CharacterId, request.LocationId, request.LocationType);
        }
    }
}
