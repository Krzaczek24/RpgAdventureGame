using Krzaq.Errors.Model;

namespace RpgAdventureGame.Backend.Core.Errors
{
    public class ErrorResponse : ErrorResponseModel<ErrorCode>
    {
        public ErrorResponse() : base() { }

        public ErrorResponse(ErrorModel error) : base(error) { }

        public ErrorResponse(IEnumerable<ErrorModel> errors) : base(errors) { }
    }
}
