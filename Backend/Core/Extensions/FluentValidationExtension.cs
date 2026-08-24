using FluentValidation;
using KrzaqTools.Extensions;
using RpgAdventureGame.Backend.Core.Errors;

namespace RpgAdventureGame.Backend.Core.Extensions
{
    public static class FluentValidationExtension
    {
        public static IRuleBuilderOptions<T, TProperty> WithErrorCode<T, TProperty>(this IRuleBuilderOptions<T, TProperty> builder, ErrorCode errorCode, params IEnumerable<object> @params)
        {
            return builder
                .WithErrorCode(errorCode.ToString())
                .WithMessage(string.Format(errorCode.GetDescription()!, ["{0}", .. @params]));
        }

        public static IRuleBuilderOptions<T, TProperty> WithErrorCodeAndMessage<T, TProperty>(this IRuleBuilderOptions<T, TProperty> builder, ErrorCode errorCode, string message)
        {
            return builder
                .WithErrorCode(errorCode.ToString())
                .WithMessage(message);
        }
    }
}
