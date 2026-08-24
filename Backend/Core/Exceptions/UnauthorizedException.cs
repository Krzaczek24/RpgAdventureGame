using RpgAdventureGame.Backend.Core.Errors;

namespace RpgAdventureGame.Backend.Core.Exceptions
{
    public class UnauthorizedException(ErrorCode errorCode, Exception? innerException = null)
        : Krzaq.Exceptions.Http.Error.UnauthorizedException<ErrorModel>(new(errorCode), innerException)
    {

    }
}
