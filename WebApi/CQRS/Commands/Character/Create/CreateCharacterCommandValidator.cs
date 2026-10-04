using FluentValidation;
using Krzaq.MediatR.Implementations;
using RpgAdventureGame.Backend.Database.SQLite.Entities.Character;
using RpgAdventureGame.Backend.WebApi.Core.Errors;
using RpgAdventureGame.Backend.WebApi.Core.Extensions;

namespace RpgAdventureGame.Backend.WebApi.CQRS.Commands.Character.Create
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
