using RpgAdventureGame.WebApi.Core.Errors;

namespace RpgAdventureGame.WebApi.Core.Exceptions
{
    public class ForbiddenException(ErrorCode errorCode = ErrorCode.Forbidden, Exception? innerException = null)
        : Krzaq.Exceptions.Http.Error.ForbiddenException<ErrorModel>(new(errorCode), innerException)
    {
    }
}
