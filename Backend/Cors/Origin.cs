namespace Backend.Cors;

public class Origin
{
    public string Scheme { get; set; }

    public string Host { get; set; }

    public bool WithWildcard { get; set; }

    public int Port { get; set; }

    /// <summary>
    /// Parses an input string into an Origin instance using Uri class functionality
    /// </summary>
    public static bool TryParse(string uriString, out Origin origin)
    {
        var result = false;

        origin = null;

        Uri uri = null;

        var wildCardFound = false;

        // Uri.TryCreate() will fail in case of wildcard presence
        if (uriString.Contains("//*."))
        {
            uriString = uriString.Replace("//*.", "//");
            wildCardFound = true;
        }

        if (Uri.TryCreate(uriString, UriKind.RelativeOrAbsolute, out uri))
        {
            try
            {
                origin = new Origin
                {
                    Scheme = !string.IsNullOrEmpty(uri.Scheme) ? uri.Scheme.ToLower() : "http",
                    Host = uri.Host.ToLower(),
                    // the Port, even if it was not specified explicitly,
                    // will be derived from the Scheme
                    Port = uri.Port,
                    WithWildcard = wildCardFound
                };

                result = true;
            }
            catch
            {
                // just swallow and return null
            }
        }

        return result;
    }
}