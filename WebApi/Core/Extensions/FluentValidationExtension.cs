using FluentValidation;
using KrzaqTools.Extensions;
using RpgAdventureGame.WebApi.Core.Errors;
using RpgAdventureGame.WebApi.Core.Extensions;

namespace RpgAdventureGame.WebApi.Core.Extensions
{
    public static class FluentValidationExtension
    {
        public static IRuleBuilderOptions<T, TProperty> WithErrorCode<T, TProperty>(this IRuleBuilderOptions<T, TProperty> builder, ErrorCode errorCode, params IEnumerable<object> @params)
        {
            return builder
                .WithErrorCode(errorCode.ToString())
                .WithMessage(string.Format(errorCode.GetDescription()!, [.. @params]));
        }

        public static IRuleBuilderOptions<T, TProperty> WithErrorCodeAndMessage<T, TProperty>(this IRuleBuilderOptions<T, TProperty> builder, ErrorCode errorCode, string message)
        {
            return builder
                .WithErrorCode(errorCode.ToString())
                .WithMessage(message);
        }
    }
}
