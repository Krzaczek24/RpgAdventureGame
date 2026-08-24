using RpgAdventureGame.Backend.Core.Errors;

namespace RpgAdventureGame.Backend.Core.Exceptions
{
    public class NotFoundException(IEnumerable<ErrorModel> errors, Exception? innerException = null)
        : Krzaq.Exceptions.Http.Error.BadRequestException<ErrorModel>(errors, innerException)
    {
        public NotFoundException(ErrorCode errorCode = ErrorCode.NotFound, Exception? innerException = null)
            : this([new(errorCode)], innerException)
        {

        }
    }
}
