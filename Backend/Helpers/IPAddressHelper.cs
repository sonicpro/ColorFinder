using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace Backend.Helpers;

public static class IpAddressHelper
{
    // used to cache resolved IP addresses for TranslateToIpAddresses()
    private static MemoryCache _hostsCache = new MemoryCache(new MemoryCacheOptions());

    /// <summary>
    /// Extracts host name or IP address from input string
    /// </summary>
    public static string GetHostName(string host)
    {
        var result = host;

        if (string.IsNullOrEmpty(result))
        {
            return result;
        }

        // if IP address is passed - will just return it
        if (!IPAddress.TryParse(result, out IPAddress ipAddress))
        {
            var parts = result.Split('.');

            var length = parts.Length;

            // if there are more than 2 segments, return just last 2 of them
            if (length > 2)
            {
                result = $"{parts[length - 2]}.{parts[length - 1]}";
            }
        }

        return result;
    }

    /// <summary>
    /// Resolves a string (host name or IP address) into an IPAddress object
    /// </summary>
    /// <param name="host">Host name or IP address</param>
    public static IPAddress[] TranslateToIpAddresses(string host, int cacheSlidingWindowMinutes = 15)
    {
        IPAddress ipAddress = null;

        // host is represented by IP address
        if (IPAddress.TryParse(host, out ipAddress))
        {
            return new IPAddress[] { ipAddress };
        }

        // host is represented by a domain name
        try
        {
            if (_hostsCache.TryGetValue<IPAddress[]>(host, out IPAddress[] result))
            {
                return result;
            }

            result = Dns.GetHostAddresses(host);

            _hostsCache.Set<IPAddress[]>(host, result, new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(cacheSlidingWindowMinutes)
            });

            return result;
        }
        catch
        {
            return new IPAddress[0];
        }
    }

    /// <summary>
    /// Checks whether IP address is private (RFC 1918)
    /// </summary>
    public static bool IsIpAddressPrivate(IPAddress ipAddress)
    {
        var result = false;

        if (!IPAddress.IsLoopback(ipAddress))
        {
            switch (ipAddress.AddressFamily)
            {
                case System.Net.Sockets.AddressFamily.InterNetwork:
                    {
                        var bytes = ipAddress.GetAddressBytes();
                        var byte0 = bytes[0];

                        switch (byte0)
                        {
                            // 10.0.0.0 - 10.255.255.255  (10/8 prefix)
                            case 10:
                                result = true;
                                break;
                            // 169.254.0.0 - 169.254.255.255 (169.254/16 prefix) - link-local
                            case 169:
                                {
                                    result = (bytes[1] == 254);
                                    break;
                                }
                            // 172.16.0.0 - 172.31.255.255  (172.16/12 prefix)
                            case 172:
                                {
                                    var byte1 = bytes[1];
                                    result = (byte1 >= 16) && (byte1 <= 31);
                                    break;
                                }
                            // 192.168.0.0 - 192.168.255.255 (192.168/16 prefix)
                            case 192:
                                {
                                    result = (bytes[1] == 168);
                                    break;
                                }
                            default:
                                break;
                        }

                        break;
                    }
                default:
                    break;
            }
        }
        else
        {
            result = true;
        }

        return result;
    }

    /// <summary>
    /// Checks whether any item of IP addresses array is private (RFC 1918)
    /// </summary>
    public static bool IsIpAddressPrivate(IPAddress[] ipAddresses)
    {
        var result = false;

        if ((ipAddresses != null) && (ipAddresses.Length > 0))
        {
            for (var i = 0; i < ipAddresses.Length; i++)
            {
                result = IsIpAddressPrivate(ipAddresses[i]);

                if (result)
                {
                    break;
                }
            }
        }

        return result;
    }
}