using RpgAdventureGame.Backend.WebApi.Core.Errors;

namespace RpgAdventureGame.Backend.WebApi.Core.Exceptions
{
    public class ForbiddenException(ErrorCode errorCode = ErrorCode.Forbidden, Exception? innerException = null)
        : Krzaq.Exceptions.Http.Error.ForbiddenException<ErrorModel>(new(errorCode), innerException)
    {
    }
}
