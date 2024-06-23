using FluentValidation;
using Backend.Models;
using Backend.Validators;

namespace Backend.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<ClosestPaletteColorRequest>, ClosestPaletteColorRequestValidator>();
    }
}