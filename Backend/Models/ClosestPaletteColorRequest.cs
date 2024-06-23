namespace Backend.Models;

public class ClosestPaletteColorRequest
{
    /// <summary>
    /// For the current palette one of 50, 100, 200, 300, 400, 500, 600, 700, 800, 900 values.
    /// </summary>
    public int BrightnessLevelValue { get; set; }

    public string Color { get; set; }

    public string ColorMatchingStrategy { get; set; }
}