using FluentValidation;
using Krzaq.MediatR.Implementations;
using RpgAdventureGame.Backend.Core.Errors;
using RpgAdventureGame.Backend.Core.Extensions;
using RpgAdventureGame.Database.SQLite.Entities.Character;

namespace RpgAdventureGame.Backend.CQRS.Commands.Character.Create
{
    public class CreateCharacterCommandValidator : RequestValidator<CreateCharacterCommand>
    {
        public CreateCharacterCommandValidator(IDbCharacterAccess characterAccess)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                    .WithErrorCode(ErrorCode.MissingField)
                .MustAsync(async (name, cancellation) => !await characterAccess.IsNameUsed(name, cancellation))
                    .WithErrorCode(ErrorCode.NonUnique);
        }
    }
}
