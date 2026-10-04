using FluentValidation;
using Krzaq.MediatR.Implementations;
using RpgAdventureGame.Database.SQLite.Entities.Character;
using RpgAdventureGame.WebApi.Core.Errors;
using RpgAdventureGame.WebApi.Core.Extensions;

namespace RpgAdventureGame.WebApi.CQRS.Commands.Character.Create
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
