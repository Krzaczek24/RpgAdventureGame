using RpgAdventureGame.WebApi.Core.Errors;

namespace RpgAdventureGame.WebApi.Core.Exceptions
{
    public class BadRequestException(IEnumerable<ErrorModel> errors, Exception? innerException = null)
        : Krzaq.Exceptions.Http.Error.BadRequestException<ErrorModel>(errors, innerException)
    {
        public BadRequestException(ErrorCode errorCode, Exception? innerException = null)
            : this([new(errorCode)], innerException)
        {

        }
    }
}
