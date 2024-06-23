using FluentValidation;
using Backend.Models;
using Backend.ColorHelpers;
using Backend.Extensions;
using static Backend.ColorHelpers.BrightnessLevelValues;

namespace Backend.Validators;

public class ClosestPaletteColorRequestValidator : AbstractValidator<ClosestPaletteColorRequest>
{
    public ClosestPaletteColorRequestValidator()
    {
        this.RuleFor(r => r.BrightnessLevelValue).Must(s => BrightnessLevels.Contains(s))
            .WithMessage("Briteness level must be one of the 50, 100, 200, 300, 400, 500, 600, 700, 800, 900 values.");
        this.RuleFor(r => r.Color).Matches("^(?:[0-9A-Fa-f]{3})?[0-9A-Fa-f]{3}$");
        this.RuleFor(r => r.ColorMatchingStrategy).Must(s => s.ParseEnumString<ColorMatchingStrategy>())
            .WithMessage(r => $"The supplied strategy name '{r.ColorMatchingStrategy}' is invalid");
    }
}