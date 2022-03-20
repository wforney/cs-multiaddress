namespace Multiformats.Address.Net;

using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

/// <summary>
/// The multiaddress tools
/// </summary>
public static class MultiaddressTools
{
    /// <summary>Gets the interface multiaddresses.</summary>
    /// <returns></returns>
    public static IEnumerable<Multiaddress> GetInterfaceMultiaddresses()
    {
#if __MonoCS__
        return Array.Empty<Multiaddress>();
#else
        return NetworkInterface
            .GetAllNetworkInterfaces()
            .SelectMany(MultiaddressExtensions.GetMultiaddresses);
#endif
    }
}
