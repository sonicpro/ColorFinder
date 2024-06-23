using System.Drawing;
using System.Globalization;

namespace Backend.ColorHelpers;

public static class ColorPalette
{
    /// <summary>
    /// Tailwind CSS palette.
    /// The colors are taken from https://tailwindcss.com/docs/customizing-colors.
    /// </summary>
    //private static readonly Dictionary<string, IList<string>> _palette = new Dictionary<string, IList<string>>
    //{
    //    { "white", new BrightnessAgnosticColor("#ffffff") },
    //    { "black", new BrightnessAgnosticColor("#000000") },
    //    { "color1", new List<string> { "#f8fafc", "#f1f5f9", "#e2e8f0", "#cbd5e1", "#94a3b8", "#64748b", "#475569", "#334155", "#1e293b", "#000000" } },
    //    { "color2", new List<string> { "#f9fafb", "#f3f4f6", "#e5e7eb", "#d1d5db", "#9ca3af", "#6b7280", "#4b5563", "#374151", "#1f2937", "#808080" } },
    //    { "color3", new List<string> { "#fafafa", "#f4f4f5", "#e4e4e7", "#d4d4d8", "#a1a1aa", "#71717a", "#52525b", "#3f3f46", "#27272a", "#000080" } },
    //    { "color4", new List<string> { "#fafafa", "#f5f5f5", "#e5e5e5", "#d4d4d4", "#a3a3a3", "#737373", "#525252", "#404040", "#262626", "#008080" } },
    //    { "color5", new List<string> { "#fafaf9", "#f5f5f4", "#e7e5e4", "#d6d3d1", "#a8a29e", "#78716c", "#57534e", "#44403c", "#292524", "#008000" } },
    //    { "color6", new List<string> { "#fef2f2", "#fee2e2", "#fecaca", "#fca5a5", "#f87171", "#ef4444", "#dc2626", "#b91c1c", "#991b1b", "#808000" } },
    //    { "color7", new List<string> { "#fff7ed", "#ffedd5", "#fed7aa", "#fdba74", "#fb923c", "#f97316", "#ea580c", "#c2410c", "#9a3412", "#800000" } },
    //    { "color8", new List<string> { "#fffbeb", "#fef3c7", "#fde68a", "#fcd34d", "#fbbf24", "#f59e0b", "#d97706", "#b45309", "#92400e", "#800080" } },
    //    { "color9", new List<string> { "#fefce8", "#fef9c3", "#fef08a", "#fde047", "#facc15", "#eab308", "#ca8a04", "#a16207", "#854d0e", "#404000" } },
    //    { "color10", new List<string> { "#f7fee7", "#ecfccb", "#d9f99d", "#bef264", "#a3e635", "#84cc16", "#65a30d", "#4d7c0f", "#3f6212", "#ff8000" } },
    //    { "color11", new List<string> { "#f0fdf4", "#dcfce7", "#bbf7d0", "#86efac", "#4ade80", "#22c55e", "#16a34a", "#15803d", "#166534", "#804000" } },
    //    { "color12", new List<string> { "#ecfdf5", "#d1fae5", "#a7f3d0", "#6ee7b7", "#34d399", "#10b981", "#059669", "#047857", "#065f46", "#ff0040" } },
    //    { "color13", new List<string> { "#f0fdfa", "#ccfbf1", "#99f6e4", "#5eead4", "#2dd4bf", "#14b8a6", "#0d9488", "#0f766e", "#115e59", "#004080" } },
    //    { "color14", new List<string> { "#ecfeff", "#cffafe", "#a5f3fc", "#67e8f9", "#22d3ee", "#06b6d4", "#0891b2", "#0e7490", "#155e75", "#ffffff" } },
    //    { "color15", new List<string> { "#f0f9ff", "#e0f2fe", "#bae6fd", "#7dd3fc", "#38bdf8", "#0ea5e9", "#0284c7", "#0369a1", "#075985", "#c0c0c0" } },
    //    { "color16", new List<string> { "#eff6ff", "#dbeafe", "#bfdbfe", "#93c5fd", "#60a5fa", "#3b82f6", "#2563eb", "#1d4ed8", "#1e40af", "#0000ff" } },
    //    { "color17", new List<string> { "#eef2ff", "#e0e7ff", "#c7d2fe", "#a5b4fc", "#818cf8", "#6366f1", "#4f46e5", "#4338ca", "#3730a3", "#00ffff" } },
    //    { "color18", new List<string> { "#f5f3ff", "#ede9fe", "#ddd6fe", "#c4b5fd", "#a78bfa", "#8b5cf6", "#7c3aed", "#6d28d9", "#5b21b6", "#00ff00" } },
    //    { "color19", new List<string> { "#faf5ff", "#f3e8ff", "#e9d5ff", "#d8b4fe", "#c084fc", "#a855f7", "#9333ea", "#7e22ce", "#6b21a8", "#ffff00" } },
    //    { "color20", new List<string> { "#fdf4ff", "#fae8ff", "#f5d0fe", "#f0abfc", "#e879f9", "#d946ef", "#c026d3", "#a21caf", "#86198f", "#ff0000" } },
    //    { "color21", new List<string> { "#fdf2f8", "#fce7f3", "#fbcfe8", "#f9a8d4", "#f472b6", "#ec4899", "#db2777", "#be185d", "#9d174d", "#ff00ff" } },
    //    { "color22", new List<string> { "#fff1f2", "#ffe4e6", "#fecdd3", "#fda4af", "#fb7185", "#f43f5e", "#e11d48", "#be123c", "#9f1239", "#ff8080" } },
    //    { "color23", new BrightnessAgnosticColor("#8000ff") },
    //    { "color24", new BrightnessAgnosticColor("#f4d3d9") }
    //};

    /// <summary>
    /// MS Paint-like palette.
    /// </summary>
    // const string ColorNamePrefix = "color";
    //
    //private static readonly Dictionary<string, IList<string>> _palette =
    //    new[] {
    //      "#000000", "#808080", "#000080", "#008080", "#008000", "#808000", "#800000",
    //      "#800080", "#408080", "#404000", "#ff8000", "#804000", "#ff0040", "#004080",
    //      "#ffffff", "#c0c0c0", "#0000ff", "#00ffff", "#00ff00", "#ffff00", "#ff0000",
    //      "#ff00ff", "#80ffff", "#80ff00", "#ffff80", "#ff8080", "#8000ff", "#4080ff"
    //    }.Select((color, index) => (ColorName: $"{ColorNamePrefix}{index + 1}", Color: color))
    //    .ToDictionary(tuple => tuple.ColorName, tuple => (IList<string>)new BrightnessAgnosticColor(tuple.Color));

    private static readonly Dictionary<string, IList<string>> _palette = new Dictionary<string, IList<string>>
    {
        { "black", new BrightnessAgnosticColor("#000000") },
        { "dark-gray-3", new BrightnessAgnosticColor("#666666") },
        { "gray", new BrightnessAgnosticColor("#cccccc") },
        { "light-gray-2", new BrightnessAgnosticColor("#efefef") },
        { "white", new BrightnessAgnosticColor("#ffffff") },
        { "red-berry", new BrightnessAgnosticColor("#980000") },
        { "red", new BrightnessAgnosticColor("#ff0000") },
        { "orange", new BrightnessAgnosticColor("#ff9900") },
        { "yellow", new BrightnessAgnosticColor("#ffff00") },
        { "green", new BrightnessAgnosticColor("#00ff00") },
        { "cyan", new BrightnessAgnosticColor("#00ffff") },
        { "cornflower-blue", new BrightnessAgnosticColor("#4a86e8") },
        { "blue", new BrightnessAgnosticColor("#0000ff") },
        { "purple", new BrightnessAgnosticColor("#9900ff") },
        { "magenta", new BrightnessAgnosticColor("#ff00ff") },
        { "light-red-berry-1", new BrightnessAgnosticColor("#cc4125") },
        { "light-red-1", new BrightnessAgnosticColor("#e06666") },
        { "light-orange-1", new BrightnessAgnosticColor("#f6b26b") },
        { "light-yellow-1", new BrightnessAgnosticColor("#ffd966") },
        { "light-green-1", new BrightnessAgnosticColor("#93c47d") },
        { "light-cyan-1", new BrightnessAgnosticColor("#76a5af") },
        { "light-cornflower-blue-1", new BrightnessAgnosticColor("#6d9eeb") },
        { "light-blue-1", new BrightnessAgnosticColor("#6fa8dc") },
        { "light-purple-1", new BrightnessAgnosticColor("#8e7cc3") },
        { "light-magenta-1", new BrightnessAgnosticColor("#c27ba0") },
        { "dark-red-berry-1", new BrightnessAgnosticColor("#a61c00") },
        { "dark-red-1", new BrightnessAgnosticColor("#cc0000") },
        { "dark-orange-2", new BrightnessAgnosticColor("#b45f06") },
        { "dark-yellow-2", new BrightnessAgnosticColor("#bf9000") },
        { "dark-green-2", new BrightnessAgnosticColor("#38761d") },
        { "dark-cyan-2", new BrightnessAgnosticColor("#134f5c") },
        { "dark-cornflower-blue-2", new BrightnessAgnosticColor("#1155cc") },
        { "dark-blue-2", new BrightnessAgnosticColor("#0b5394") },
        { "dark-purple-2", new BrightnessAgnosticColor("#351c75") },
        { "dark-magenta-2", new BrightnessAgnosticColor("#741b47") },
        { "pastel-pink", new BrightnessAgnosticColor("#f4d3d9") }
    };

    public static Dictionary<string, IList<string>> Palette => _palette;

    /// <summary>
    /// Returns a slice of the palette.
    /// </summary>
    /// <param name="index">The index that defines the brightness. Useful only for "Tailwind CSS" palette.</param>
    /// <returns>A dictionary of color names and <see cref="Color"></see> values.</returns>
    internal static Dictionary<string, Color> PaletteAtGivenBrightnessIndex(int index)
    {
        if (index > 0 && !typeof(IList<string>).IsAssignableFrom(_palette.First().Value.GetType()))
        {
            throw new InvalidOperationException($"The palette does not support brightness levels. \"{nameof(index)}\" must be 0.");
        }

        var palette = _palette.ToDictionary(color => color.Key,
            color => ColorFunc(color.Value[index]),
            StringComparer.InvariantCultureIgnoreCase);
        return palette.Any() ? palette : new Dictionary<string, Color>();
    }

    private static Color ColorFunc(string cssColor)
    {
        if (cssColor[0] != '#')
        {
            throw new System.ArgumentException(nameof(cssColor));
        }

        return Color.FromArgb(GetR(cssColor), GetG(cssColor), GetB(cssColor));
    }

    private static byte GetR(string cssColor)
    {
        byte r;
        if (!byte.TryParse(cssColor.Substring(1, 2), System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture, out r))
        {
            throw new ArgumentException(nameof(cssColor));
        }
        return r;
    }

    private static byte GetG(string cssColor)
    {
        byte g;
        if (!byte.TryParse(cssColor.Substring(3, 2), System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture, out g))
        {
            throw new ArgumentException(nameof(cssColor));
        }
        return g;
    }

    private static byte GetB(string cssColor)
    {
        byte b;
        if (!byte.TryParse(cssColor.Substring(5, 2), System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture, out b))
        {
            throw new ArgumentException(nameof(cssColor));
        }
        return b;
    }
}