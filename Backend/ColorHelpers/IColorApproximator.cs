namespace Backend.ColorHelpers;

using ONVIF_Models;

public interface IColorApproximator
{
    /// <summary>
    /// Takes the list of colors from the database.
    /// </summary>
    /// <param name="color">Object's color.</param>
    /// <returns>Returns a similar structure that it takes except the color is an HTML RGB value from <see cref="ColorPalette"/>
    /// that is closest to the original color.</returns>
    string GetClosestColor(ColorCluster color);
}