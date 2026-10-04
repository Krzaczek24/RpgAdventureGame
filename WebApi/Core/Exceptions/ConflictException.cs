using RpgAdventureGame.Backend.WebApi.Core.Errors;

namespace RpgAdventureGame.Backend.WebApi.Core.Exceptions
{
    public class ConflictException(IEnumerable<ErrorModel> errors, Exception? innerException = null)
        : Krzaq.Exceptions.Http.Error.ConflictException<ErrorModel>(errors, innerException)
    {
        public ConflictException(ErrorCode errorCode, Exception? innerException = null)
            : this([new(errorCode)], innerException)
        {

        }
    }
}
