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
                .MustAsync(async (name, ct) => !await characterAccess.IsCharacterNameUsedAsync(name, ct))
                    .WithErrorCode(ErrorCode.NameAlreadyInUse);
        }
    }
}
