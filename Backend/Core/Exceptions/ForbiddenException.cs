using RpgAdventureGame.Backend.Core.Errors;

namespace RpgAdventureGame.Backend.Core.Exceptions
{
    public class ForbiddenException(ErrorCode errorCode = ErrorCode.Forbidden, Exception? innerException = null)
        : Krzaq.Exceptions.Http.Error.ForbiddenException<ErrorModel>(new(errorCode), innerException)
    {

    }
}
