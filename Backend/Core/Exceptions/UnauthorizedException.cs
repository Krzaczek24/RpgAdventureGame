using RpgAdventureGame.Backend.Core.Errors;

namespace RpgAdventureGame.Backend.Core.Exceptions
{
    public class UnauthorizedException(ErrorCode errorCode = ErrorCode.Unauthorized, Exception? innerException = null)
        : Krzaq.Exceptions.Http.Error.UnauthorizedException<ErrorModel>(new(errorCode), innerException)
    {
    }
}
