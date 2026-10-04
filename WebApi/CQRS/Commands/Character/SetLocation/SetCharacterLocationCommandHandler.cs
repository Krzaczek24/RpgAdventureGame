using Krzaq.MediatR.Interfaces;
using RpgAdventureGame.Common.Enums;
using RpgAdventureGame.Database.SQLite.Entities.Character;


namespace RpgAdventureGame.WebApi.CQRS.Commands.Character.SetLocation
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
