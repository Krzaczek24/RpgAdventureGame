using Krzaq.Extensions.Expression;
using Krzaq.Extensions.String.Notation;
using KrzaqTools.Extensions;
using RpgAdventureGame.Backend.Core.Errors;
using System.Linq.Expressions;

namespace RpgAdventureGame.Backend.Core.Extensions
{
    public static class ErrorCodeExtension
    {
        public static ErrorModel AsError(this ErrorCode code, params IEnumerable<object> @params)
        {
            string description = code.GetDescription()!;
            string message = string.Format(description, [.. @params]);
            return new ErrorModel(code, message);
        }

        public static ErrorModel AsFieldError(this ErrorCode code, string fieldName, params IEnumerable<object> @params)
            => AsError(code, [fieldName.ToCamelCase(), .. @params]);

        public static ErrorModel AsFieldError(this ErrorCode code, LambdaExpression field, params IEnumerable<object> @params)
            => AsError(code, [field.GetMemberName().ToCamelCase(), .. @params]);
    }
}
