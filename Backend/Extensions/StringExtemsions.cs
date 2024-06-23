namespace Backend.Extensions;

public static class StringExtensions
{
    public static bool ParseEnumString<T>(this string enumString) where T: struct, Enum
    {
        // Any integer string will be parsed OK by Enum.TryParse(), use IsDefined() to handle that case.
        return Enum.TryParse(enumString, true, out T value) && Enum.IsDefined(value);
    }
}