using FluentValidation;
using Krzaq.MediatR.Implementations;
using RpgAdventureGame.Backend.Core.Errors;
using RpgAdventureGame.Backend.Core.Extensions;
using RpgAdventureGame.Common.Enums;
using RpgAdventureGame.Database.SQLite.Entities.Area;
using RpgAdventureGame.Database.SQLite.Entities.Character;
using RpgAdventureGame.Database.SQLite.Entities.Travel;

namespace RpgAdventureGame.Backend.CQRS.Commands.Character.SetLocation
{
    public class SetCharacterLocationCommandValidator
         : RequestValidator<SetCharacterLocationCommand>
    {
        public SetCharacterLocationCommandValidator(
            IDbAreaAccess areaAccess,
            IDbCharacterAccess characterAccess,
            IDbTravelAccess travelAccess)
        {
            RuleFor(x => x.CharacterId)
                .NotNull()
                .MustAsync(async (id, ct) => await characterAccess.CharacterExistsAsync(id, ct))
                    .WithErrorCode(ErrorCode.CharacterNotFound);

            RuleFor(x => x.LocationId)
                .NotNull()
                .MustAsync(async (id, ct) => await areaAccess.AreaExistsAsync(id, ct))
                    .WithErrorCode(ErrorCode.AreaNotFound)
                    .When(x => x.LocationType is CharacterLocationType.Area, ApplyConditionTo.CurrentValidator)
                .MustAsync(async (id, ct) => await travelAccess.PathExistsAsync(id, ct))
                    .WithErrorCode(ErrorCode.PathNotFound)
                    .When(x => x.LocationType is CharacterLocationType.Path, ApplyConditionTo.CurrentValidator);
        }
    }
}
