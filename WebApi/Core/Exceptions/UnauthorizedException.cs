using RpgAdventureGame.WebApi.Core.Errors;

namespace RpgAdventureGame.WebApi.Core.Exceptions
{
    public class UnauthorizedException(ErrorCode errorCode = ErrorCode.Unauthorized, Exception? innerException = null)
        : Krzaq.Exceptions.Http.Error.UnauthorizedException<ErrorModel>(new(errorCode), innerException)
    {
    }
}
