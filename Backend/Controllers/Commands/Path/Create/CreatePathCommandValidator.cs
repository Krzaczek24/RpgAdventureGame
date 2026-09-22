using FluentValidation;
using Krzaq.MediatR.Implementations;
using RpgAdventureGame.Backend.Core.Errors;
using RpgAdventureGame.Backend.Core.Extensions;

namespace RpgAdventureGame.Backend.Controllers.Commands.Path.Create
{
    public class CreatePathCommandValidator : RequestValidator<CreatePathCommand>
    {
        public CreatePathCommandValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithErrorCode(ErrorCode.MissingField);

            RuleFor(x => x.StartAreaId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithErrorCode(ErrorCode.MissingField)
                .GreaterThan(0)
                .WithErrorCode(ErrorCode.LesserThan, 0);

            RuleFor(x => x.EndAreaId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithErrorCode(ErrorCode.MissingField)
                .GreaterThan(0)
                .WithErrorCode(ErrorCode.LesserThan, 0)
                .NotEqual(x => x.StartAreaId)
                .WithErrorCode(ErrorCode.EqualToOtherField, nameof(CreatePathCommand.StartAreaId));

            RuleFor(x => x.Distance)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithErrorCode(ErrorCode.MissingField)
                .GreaterThan(0)
                .WithErrorCode(ErrorCode.LesserThan, 0);

            RuleFor(x => x.DangerLevel)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0)
                .WithErrorCode(ErrorCode.LesserThan, 0);

            RuleFor(x => x.DangerProbability)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0)
                .WithErrorCode(ErrorCode.LesserThan, 0)
                .LessThan(1)
                .WithErrorCode(ErrorCode.GreaterThan, 1);
        }
    }
}
