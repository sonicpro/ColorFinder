using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using FluentValidation;
using FluentValidation.AspNetCore;
using Backend.ColorHelpers;
using Backend.Models;
using Backend.ONVIF_Models;
using static Backend.ColorHelpers.ColorPalette;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaletteController : ControllerBase
{
    private readonly IValidator<ClosestPaletteColorRequest> _requestValidator;

    public PaletteController(IValidator<ClosestPaletteColorRequest> validator)
    {
        this._requestValidator = validator;
    }

    //[Filters.Authorization]
    [Filters.ExceptionFilter]
    [HttpGet]
    [Route("matching-color")]
    public IActionResult GetPaletteColorIndex([FromQuery] System.Guid sessionId, [FromQuery] ClosestPaletteColorRequest request)
    {
        if (request != null)
        {
            if (_requestValidator.Validate(request) is var validationResult && !validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState, nameof(request));
                return this.BadRequest(this.ModelState);
            }

            var strategy = System.Enum.Parse<ColorMatchingStrategy>(request.ColorMatchingStrategy, true);
            var colorCluster = GetColorCluster(request.Color);
            // Change brightness levels from 50, 100, 200, 300 etc. to 0, 1, 2, 3 etc.
            var adjustedBrightnessIndex = request.BrightnessLevelValue / 100;
            var colorApproximator = new ColorApproximator(adjustedBrightnessIndex, strategy);
            return Ok(colorApproximator.GetClosestPaletteColorKey(colorCluster));
        }

        return this.BadRequest();
    }

    //[Filters.Authorization]
    [Filters.ExceptionFilter]
    [HttpGet]
    public IActionResult GetPalette([FromQuery] System.Guid sessionId)
    {
        Dictionary<string, IList<string>> palette = Palette;
        return this.Ok(palette.ToDictionary(
                (colorTint) => colorTint.Key,
                (colorTint) => ColorWithBrightnessLevelsGenerator(colorTint.Value))
            );
    }

    private static ColorCluster GetColorCluster(string cssColor)
    {
        var normalizedCssColor = NormalizeColor(cssColor);
        Dictionary<int, double> colorComponents = new Dictionary<int, double>
        {
            { 0, default(double) },
            { 2, default(double) },
            { 4, default(double) }
        };

        foreach (int key in colorComponents.Keys)
        {
            byte component;
            if (!byte.TryParse(normalizedCssColor.Substring(key, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out component))
            {
                throw new System.ArgumentException(nameof(normalizedCssColor));
            }
            colorComponents[key] = component;
        }

        return new ColorCluster { Color = new Color { X = colorComponents[0], Y = colorComponents[2], Z = colorComponents[4] } };
    }

    private static string NormalizeColor(string cssColor)
    {
        if (cssColor.Length == 6)
        {
            return cssColor;
        }
        else // 3-char-long CSS color.
        {
            char r = cssColor[0];
            char g = cssColor[1];
            char b = cssColor[2];
            return new string(Enumerable.Repeat(r, 2).ToArray()) + new string(Enumerable.Repeat(g, 2).ToArray()) + new string(Enumerable.Repeat(b, 2).ToArray());
        }
    }

    /// <summary>
    /// Converts 0-based indexes of colors of the same tint in the palette to "Tailwind CSS" palette format.
    /// I.e. the most watered color have the brightness level 50, the next color has the briteness level 100,
    /// the next color has the brightness level 200, and so on.
    /// </summary>
    /// <param name="colors">Array of colors of the same tint. The watered color at the beginning of the array, the dimmed colors - at the end.</param>
    /// <returns>The colors received in the parameter keyed by the brightness level.</returns>
    private static Dictionary<int, string> ColorWithBrightnessLevelsGenerator(IList<string> colors)
    {
        return colors.Select((color, index) =>
        {
            return new { color, index };
        }).ToDictionary(
                (anonymousColorObject) => anonymousColorObject.index == 0 ? 50 : anonymousColorObject.index * 100,
                (anonymousColorObject) => anonymousColorObject.color
            );
    }
}
