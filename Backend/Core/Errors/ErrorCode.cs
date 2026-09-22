using Krzaq.Attributes.EnumToString;
using System.ComponentModel;

namespace RpgAdventureGame.Backend.Core.Errors
{
    [EnumToString(NameAlterMode.ToUpperSnake)]
    public enum ErrorCode
    {
        [Description("An unknown error occurred")]
        Unknown,
        [Description("Bad request")]
        BadRequest,

        // --- authorization ---
        [Description("Failed to authorize")]
        Unauthorized,
        [Description("Forbidden")]
        Forbidden,
        [Description("Token expired")]
        TokenExpired,
        [Description("Invalid token")]
        InvalidToken,
        [Description("Token already exists")]
        TokenExists,
        // ---------------------

        // --- request fields ---
        [Description("Field '{0}' is required.")]
        MissingField,
        [Description("Field '{0}' value is not unique.")]
        NonUnique,
        [Description("Field '{0}' value is the identifier of a non-existent resource.")]
        NotFound,
        [Description("Field '{0}' value is not valid SHA512 string.")]
        InvalidSha512,
        [Description("Field '{0}' value cannot be longer than '{1}' characters.")]
        ValueTooLong,
        [Description("Field '{0}' value cannot be non positive.")]
        NonPositiveValue,
        [Description("Field '{0}' value cannot be lesser than '{1}' field value.")]
        LesserThanOtherField,
        [Description("Field '{0}' value cannot be lesser than '{1}'.")]
        LesserThan,
        [Description("Field '{0}' value cannot be greater than '{1}' field value.")]
        GreaterThanOtherField,
        [Description("Field '{0}' value cannot be greater than '{1}'.")]
        GreaterThan,
        [Description("Field '{0}' value cannot be equal to '{1}' field value.")]
        EqualToOtherField,
        [Description("Field '{0}' date cannot be from past.")]
        DateFromPast,
        // -----------------------

        
        // -------------
    }
}
