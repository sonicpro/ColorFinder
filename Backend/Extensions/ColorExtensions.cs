namespace Backend.Extensions;

using System.Drawing;

internal static class ColorExtensions
{
    internal static string ToHtml(this Color color)
    {
        try
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }
        catch
        {
            return $"#{color.Name:X6}";
        }
    }
}