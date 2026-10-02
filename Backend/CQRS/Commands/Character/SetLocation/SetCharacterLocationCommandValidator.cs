using FluentValidation;
using Krzaq.MediatR.Implementations;
using RpgAdventureGame.Common.Enums;
using RpgAdventureGame.Backend.Core.Errors;
using RpgAdventureGame.Backend.Core.Extensions;
using RpgAdventureGame.Database.SQLite.Entities.Area;
using RpgAdventureGame.Database.SQLite.Entities.Character;
using RpgAdventureGame.Database.SQLite.Entities.Path;

namespace RpgAdventureGame.Backend.CQRS.Commands.Character.SetLocation
{
    public class SetCharacterLocationCommandValidator
         : RequestValidator<SetCharacterLocationCommand>
    {
        public SetCharacterLocationCommandValidator(
            IDbAreaAccess areaAccess,
            IDbCharacterAccess characterAccess,
            IDbPathAccess pathAccess)
        {
            RuleFor(x => x.CharacterId)
                .NotEmpty()
                    .WithErrorCode(ErrorCode.MissingField)
                .MustAsync(async (characterId, cancellation) => await characterAccess.Exists(characterId, cancellation))
                    .WithErrorCode(ErrorCode.NotFound);

            RuleFor(x => x.LocationId)
                .NotEmpty()
                    .WithErrorCode(ErrorCode.MissingField)
                .MustAsync(async (locationId, cancellation) => await areaAccess.Exists(locationId, cancellation))
                    .WithErrorCode(ErrorCode.NotFound)
                    .When(x => x.LocationType is CharacterLocationType.Area, ApplyConditionTo.CurrentValidator)
                .MustAsync(async (locationId, cancellation) => await pathAccess.Exists(locationId, cancellation))
                    .WithErrorCode(ErrorCode.NotFound)
                    .When(x => x.LocationType is CharacterLocationType.Path, ApplyConditionTo.CurrentValidator);
        }
    }
}
