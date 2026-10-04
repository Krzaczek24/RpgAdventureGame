using Krzaq.MediatR.Interfaces;
using RpgAdventureGame.Backend.Database.SQLite.Entities.Character;
using RpgAdventureGame.Backend.Common.Enums;


namespace RpgAdventureGame.Backend.WebApi.CQRS.Commands.Character.SetLocation
{
    public class SetCharacterLocationCommandHandler(IDbCharacterAccess characterAccess)
        : IRequestHandler<SetCharacterLocationCommand>
    {
        public async ValueTask Handle(SetCharacterLocationCommand request, CancellationToken cancellationToken = default)
        {
            switch (request.LocationType)
            {
                case CharacterLocationType.Area:
                    await characterAccess.SetCharacterCurrentAreaAsync(request.CharacterId, request.LocationId, cancellationToken);
                    break;
                case CharacterLocationType.Path:
                    await characterAccess.SetCharacterCurrentPathAsync(request.CharacterId, request.LocationId, cancellationToken);
                    break;
            }
        }
    }
}
