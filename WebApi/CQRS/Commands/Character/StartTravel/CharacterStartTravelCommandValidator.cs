using FluentValidation;
using Krzaq.MediatR.Implementations;
using RpgAdventureGame.Database.SQLite.Entities.Character;
using RpgAdventureGame.WebApi.Core.Errors;
using RpgAdventureGame.WebApi.Core.Extensions;
using RpgAdventureGame.WebApi.Services;

namespace RpgAdventureGame.WebApi.CQRS.Commands.Character.StartTravel
{
    public class CharacterStartTravelCommandValidator
        : RequestValidator<CharacterStartTravelCommand>
    {


        public CharacterStartTravelCommandValidator(
            IDbCharacterAccess characterAccess,
            ITravelService travelService)
        {
            IReadOnlySet<(int, int)> invalidPaths = new HashSet<(int, int)>();

            RuleFor(x => x.CharacterId)
                .NotEmpty()
                .MustAsync(async (id, ct) => await characterAccess.CharacterExistsAsync(id, ct))
                    .WithErrorCode(ErrorCode.CharacterNotFound)
                .MustAsync(async (id, ct) => !await characterAccess.IsCharacterTravelingAsync(id, ct))
                    .WithErrorCode(ErrorCode.CharacterInTravel)
                .DependentRules(() => {
                    RuleFor(x => x.PathIds)
                        .NotEmpty()
                        .MustAsync(async (ids, ct) => (invalidPaths = await travelService.GetInvalidTravelPathsAsync(ids, ct)).Count == 0)
                            .WithErrorCode(ErrorCode.InvalidTravelPaths, string.Join(", ", invalidPaths));
                });
        }
    }
}
