using FluentValidation;
using Krzaq.MediatR.Implementations;
using RpgAdventureGame.Backend.Core.Errors;
using RpgAdventureGame.Backend.Core.Extensions;

namespace RpgAdventureGame.Backend.Controllers.Commands.Character.Create
{
    public class CreateCharacterCommandValidator : RequestValidator<CreateCharacterCommand>
    {
        public CreateCharacterCommandValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithErrorCode(ErrorCode.MissingField);

            RuleFor(x => x.StartingAreaID)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0)
                .WithErrorCode(ErrorCode.BadRequest);
        }
    }
}
