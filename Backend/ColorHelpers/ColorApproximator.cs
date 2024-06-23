using Backend.ONVIF_Models;
using Backend.Extensions;
using Color = System.Drawing.Color;
using static Backend.ColorHelpers.ColorPalette;
namespace Backend.ColorHelpers;

delegate double ColorDistanceFormula(Color paletteColor, ColorCluster cameraColor);

internal class ColorApproximator : IColorApproximator
{
    const float MaxHueValue = 360F;
    private readonly static double _denominator = byte.MaxValue + 1;

    private readonly Dictionary<string, Color> _palette;
    private readonly ColorDistanceFormula _colorDistanceFormula;

    public ColorApproximator(int paletteBrightnessIndex = 0, ColorMatchingStrategy strategy = ColorMatchingStrategy.RedMean)
    {
        this._palette = PaletteAtGivenBrightnessIndex(paletteBrightnessIndex);
        this._colorDistanceFormula = GetDistanceFormulaByStratedy(strategy);
    }

    /// <summary>
    /// Return the palette color that is closest to a given color.
    /// Uses "redmean" forumula to tell the colors difference. See https://en.wikipedia.org/wiki/Color_difference#sRGB
    /// </summary>
    /// <param name="color">Color detected by a camera.</param>
    /// <returns>The closest color as HTML RGB string.</returns>
    public string GetClosestColor(ColorCluster color)
    {
        Color closestPaletteColor = this._palette[GetClosestPaletteColorKey(color)];
        return closestPaletteColor.ToHtml();
    }

    public string GetClosestPaletteColorKey(ColorCluster cameraColor)
    {
        var paletteColorNames = this._palette?.Keys;
        string closestPaletteColorKey = paletteColorNames?.FirstOrDefault();
        if (closestPaletteColorKey == null)
        {
            throw new InvalidOperationException("The palette must not be empty.");
        }

        var minimalColorDistance = this._colorDistanceFormula(this._palette[closestPaletteColorKey], cameraColor);
        foreach (string key in paletteColorNames.Skip(1))
        {
            var distanceFromCurrentPaletteColor = this._colorDistanceFormula(this._palette[key], cameraColor);
            if (distanceFromCurrentPaletteColor < minimalColorDistance)
            {
                closestPaletteColorKey = key;
                minimalColorDistance = distanceFromCurrentPaletteColor;
            }
        }

        return closestPaletteColorKey;
    }

    private static double RedMeanColorDistance(Color paletteColor, ColorCluster cameraColor)
    {
        var redmean = (paletteColor.R + cameraColor.Color.X) / 2;
        double deltaR = paletteColor.R - cameraColor.Color.X;
        double deltaG = paletteColor.G - cameraColor.Color.Y;
        double deltaB = paletteColor.B - cameraColor.Color.Z;
        return (2 + redmean / _denominator) * Math.Pow(deltaR, 2) +
            4 * Math.Pow(deltaG, 2) +
            (2 + (byte.MaxValue - redmean) / _denominator) * Math.Pow(deltaB, 2);
    }

    private static double RgbSpaceColorDistance(Color paletteColor, ColorCluster cameraColor)
    {
        double rDistanceSquare = Math.Pow(paletteColor.R - cameraColor.Color.X, 2);
        double gDistanceSquare = Math.Pow(paletteColor.G - cameraColor.Color.Y, 2);
        double bDistanceSquare = Math.Pow(paletteColor.B - cameraColor.Color.Z, 2);
        return rDistanceSquare + gDistanceSquare + bDistanceSquare;
    }

    private static double HueDistance(Color paletteColor, ColorCluster cameraColor)
    {
        var color = Color.FromArgb((int)cameraColor.Color.X, (int)cameraColor.Color.Y, (int)cameraColor.Color.Z);
        double distance = Math.Abs(paletteColor.GetHue() - color.GetHue());
        return distance > (MaxHueValue / 2) ? MaxHueValue - distance : distance;
    }

    private static ColorDistanceFormula GetDistanceFormulaByStratedy(ColorMatchingStrategy strategy)
    {
        switch (strategy)
        {
            case ColorMatchingStrategy.Hue:
                return HueDistance;
            case ColorMatchingStrategy.RedMean:
                return RedMeanColorDistance;
            case ColorMatchingStrategy.RgbSpace:
                return RgbSpaceColorDistance;
            default:
                return RedMeanColorDistance;
        };
    }
}