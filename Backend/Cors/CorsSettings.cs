namespace Backend.Cors;

public class CorsSettings
{
    /// <summary>
    /// List of origins in format "scheme://host:port/action"
    /// </summary>
    public List<string> AllowedOrigins { get; set; }

    /// <summary>
    /// True if origin is private (RFC 1918)
    /// </summary>
    public bool PrivateAddressesAllowed { get; set; }
}