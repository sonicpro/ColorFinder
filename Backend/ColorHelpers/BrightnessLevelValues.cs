namespace Backend.ColorHelpers;

internal static class BrightnessLevelValues
{
    private static readonly HashSet<int> _brightnessLevels = new HashSet<int> { 50, 100, 200, 300, 400, 500, 600, 700, 800, 900 };

    public static HashSet<int> BrightnessLevels => _brightnessLevels;
}