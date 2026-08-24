using FluentValidation.Results;
using Krzaq.Extensions.String.Notation;
using Krzaq.MediatR.Interfaces;
using RpgAdventureGame.Backend.Core.Exceptions;

namespace RpgAdventureGame.Backend.Core.Errors
{
    public class ErrorHandler : IRequestErrorsHandler
    {
        public Task<Exception> Handle(IReadOnlyCollection<ValidationFailure> errors)
        {
            var exception = new BadRequestException(errors.Select(Convert));
            return Task.FromResult<Exception>(exception);
        }

        private static ErrorModel Convert(ValidationFailure e)
        {
            var errorCode = Enum.Parse<ErrorCode>(e.ErrorCode);
            return new ErrorModel(errorCode, string.Format(e.ErrorMessage, e.PropertyName.ToCamelCase()));
        }
    }
}
