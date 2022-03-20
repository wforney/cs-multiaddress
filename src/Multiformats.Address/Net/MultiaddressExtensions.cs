namespace Multiformats.Address.Net;

using Multiformats.Address.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;

/// <summary>
/// The multiaddress extensions class
/// </summary>
public static class MultiaddressExtensions
{
    /// <summary>Gets the local multiaddress.</summary>
    /// <param name="socket">The socket.</param>
    /// <returns></returns>
    public static Multiaddress GetLocalMultiaddress(this Socket socket)
    {
        return socket.LocalEndPoint.ToMultiaddress(socket.ProtocolType);
    }

    /// <summary>Gets the remote multiaddress.</summary>
    /// <param name="socket">The socket.</param>
    /// <returns></returns>
    public static Multiaddress GetRemoteMultiaddress(this Socket socket)
    {
        return socket.RemoteEndPoint.ToMultiaddress(socket.ProtocolType);
    }

    /// <summary>Converts to multiaddress.</summary>
    /// <param name="ep">The ep.</param>
    /// <param name="protocolType">Type of the protocol.</param>
    /// <returns></returns>
    public static Multiaddress ToMultiaddress(this EndPoint ep, ProtocolType protocolType)
    {
        Multiaddress ma = new();

        IPEndPoint ip = (IPEndPoint)ep;
        if (ip is not null)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                ma.Add<IP4>(ip.Address);
            }

            if (ip.AddressFamily == AddressFamily.InterNetworkV6)
            {
                ma.Add<IP6>(ip.Address);
            }

            if (protocolType == ProtocolType.Tcp)
            {
                ma.Add<TCP>((ushort)ip.Port);
            }

            if (protocolType == ProtocolType.Udp)
            {
                ma.Add<UDP>((ushort)ip.Port);
            }
        }

        return ma;
    }

    public static Multiaddress ToMultiaddress(this IPAddress ip)
    {
        Multiaddress ma = new();
        if (ip.AddressFamily == AddressFamily.InterNetwork)
        {
            ma.Add<IP4>(ip);
        }

        if (ip.AddressFamily == AddressFamily.InterNetworkV6)
        {
            ma.Add<IP6>(ip);
        }

        return ma;
    }

    public static IPEndPoint ToEndPoint(this Multiaddress ma)
    {
        return ToEndPoint(ma, out ProtocolType pt);
    }

    public static IPEndPoint ToEndPoint(this Multiaddress ma, out ProtocolType protocolType)
    {
        return ToEndPoint(ma, out protocolType, out SocketType st);
    }

    public static IPEndPoint ToEndPoint(this Multiaddress ma, out ProtocolType protocolType, out SocketType socketType)
    {
        IPAddress addr = null;
        IP ip = ma.Protocols.OfType<IP4>().SingleOrDefault();
        if (ip is not null)
        {
            addr = (IPAddress)ip.Value;
        }
        else
        {
            ip = ma.Protocols.OfType<IP6>().SingleOrDefault();
            if (ip is not null)
            {
                addr = (IPAddress)ip.Value;
            }
        }

        int? port = null;
        Number n = ma.Protocols.OfType<TCP>().SingleOrDefault();
        if (n is not null)
        {
            port = (ushort)n.Value;
            protocolType = ProtocolType.Tcp;
            socketType = SocketType.Stream;
        }
        else
        {
            n = ma.Protocols.OfType<UDP>().SingleOrDefault();
            if (n is not null)
            {
                port = (ushort)n.Value;
                protocolType = ProtocolType.Udp;
                socketType = SocketType.Dgram;
            }
            else
            {
                protocolType = ProtocolType.Unknown;
                socketType = SocketType.Unknown;
            }
        }

        return new IPEndPoint(addr ?? IPAddress.Any, port ?? 0);
    }

    public static Socket CreateSocket(this Multiaddress ma)
    {
        return CreateSocket(ma, out IPEndPoint ep);
    }

    public static Socket CreateSocket(this Multiaddress ma, out IPEndPoint ep)
    {
        ep = ma.ToEndPoint(out ProtocolType pt, out SocketType st);

        return new Socket(ep.AddressFamily, st, pt);
    }

    public static Socket CreateConnection(this Multiaddress ma)
    {
        Socket socket = CreateSocket(ma, out IPEndPoint ep);
        socket.Connect(ep);
        return socket;
    }

    public static Task<Socket> CreateConnectionAsync(this Multiaddress ma)
    {
        Socket socket = CreateSocket(ma, out IPEndPoint ep);

#if NETSTANDARD1_6
        return socket.ConnectAsync(ep)
            .ContinueWith(_ => socket);
#else
        TaskCompletionSource<Socket> tcs = new();

        try
        {
            socket.BeginConnect(ep, ar =>
            {
                try
                {
                    socket.EndConnect(ar);
                    tcs.TrySetResult(socket);
                }
                catch (Exception e)
                {
                    tcs.TrySetException(e);
                }
            }, null);
        }
        catch (Exception e)
        {
            tcs.TrySetException(e);
        }

        return tcs.Task;
#endif
    }

    public static Socket CreateListener(this Multiaddress ma, int backlog = 10)
    {
        Socket socket = CreateSocket(ma, out IPEndPoint ep);
        socket.Bind(ep);
        socket.Listen(backlog);
        return socket;
    }

    public static bool IsThinWaist(this Multiaddress ma)
    {
        if (!ma.Protocols.Any())
        {
            return false;
        }

        if (!(ma.Protocols[0] is IP4) && !(ma.Protocols[0] is IP6))
        {
            return false;
        }

        if (ma.Protocols.Count == 1)
        {
            return true;
        }

        return ma.Protocols[1] is TCP || ma.Protocols[1] is UDP ||
               ma.Protocols[1] is IP4 || ma.Protocols[1] is IP6;
    }

    public static IEnumerable<Multiaddress> GetMultiaddresses(this NetworkInterface nic)
    {
        return nic
            .GetIPProperties()
            .UnicastAddresses
            .Select(addr => addr.Address.ToMultiaddress());
    }

    public static IEnumerable<Multiaddress> Match(this Multiaddress match, params Multiaddress[] addrs)
    {
        foreach (Multiaddress a in addrs.Where(x => match.Protocols.Count == x.Protocols.Count))
        {
            int i = 0;

            if (a.Protocols.All(p2 => match.Protocols[i++].Code == p2.Code))
            {
                yield return a;
            }
        }
    }
}
