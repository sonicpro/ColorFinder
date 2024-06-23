using Backend.Helpers;
using Microsoft.Extensions.Options;

namespace Backend.Cors;

public class CorsValidator
{
    #region Constructor and data

    private bool _allOriginsAllowed = false;

    private bool _privateOriginsAllowed = false;

    private readonly List<Origin> _allowedOrigins = new List<Origin>();

    public CorsValidator(IOptions<CorsSettings> config)
    {
        SetConfiguration(config);
    }

    #endregion

    public bool IsOriginAllowed(string origin)
    {
        // highest priority - everything is allowed
        if (_allOriginsAllowed)
        {
            return true;
        }

        if (!Origin.TryParse(origin, out Origin originParsed))
        {
            return false;
        }

        var result = false;

        // second - need to check over allowed origins
        // same origin - scheme, host, port
        foreach (var allowedOrigin in _allowedOrigins ?? Enumerable.Empty<Origin>())
        {
            // compare scheme (both are lower case)
            if (allowedOrigin.Scheme != originParsed.Scheme)
            {
                continue;
            }

            // compare port number
            if (allowedOrigin.Port != originParsed.Port)
            {
                continue;
            }

            // support just a simple wildcard at the beginning of the host name
            if (allowedOrigin.WithWildcard)
            {
                result = originParsed.Host == allowedOrigin.Host
                            || originParsed.Host.EndsWith("." + allowedOrigin.Host);
            }
            else
            {
                result = allowedOrigin.Host == originParsed.Host;
            }

            if (result)
            {
                return result;
            }
        }

        // last and the most expensive chance - check if origin is private
        if (_privateOriginsAllowed)
        {
            var host = IpAddressHelper.GetHostName(originParsed.Host);

            var addresses = IpAddressHelper.TranslateToIpAddresses(host);

            // check is address is private
            result = IpAddressHelper.IsIpAddressPrivate(addresses);
        }

        if (result)
        {
            return result;
        }

        if (!result)
        {
        }

        return result;
    }

    #region Private methods

    // sets validator configuration according to data, read from appsettings.json
    private void SetConfiguration(IOptions<CorsSettings> config)
    {
        if (config?.Value == null)
        {

            return;
        }

        _privateOriginsAllowed = config.Value.PrivateAddressesAllowed;

        if (config.Value.AllowedOrigins != null)
        {
            _allOriginsAllowed = config.Value.AllowedOrigins.Any(o => o.Trim() == "*");
        }

        if (!_allOriginsAllowed)
        {
            foreach (var strOrigin in config.Value.AllowedOrigins ?? Enumerable.Empty<string>())
            {
                if (Origin.TryParse(strOrigin, out Origin origin))
                {
                    _allowedOrigins.Add(origin);
                }
                else
                {
                }
            }
        }

    }

    #endregion
}