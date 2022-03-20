namespace Multiformats.Address;

using BinaryEncoding;
using Multiformats.Address.Protocols;
using Multiformats.Hash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

/// <summary>
/// The multiaddress class
/// </summary>
/// <seealso cref="IEquatable{Multiaddress}" />
public partial class Multiaddress : IEquatable<Multiaddress>
{
    private static readonly List<Protocol> _protocols = new();

    /// <summary>Initializes the <see cref="Multiaddress"/> class.</summary>
    static Multiaddress()
    {
        Setup<IP4>("ip4", 4, 32, false, ip => ip is not null ? new IP4((IPAddress)ip) : new IP4());
        Setup<IP6>("ip6", 41, 128, false, ip => ip is not null ? new IP6((IPAddress)ip) : new IP6());
        Setup<TCP>("tcp", 6, 16, false, port => port is not null ? new TCP((ushort)port) : new TCP());
        Setup<UDP>("udp", 17, 16, false, port => port is not null ? new UDP((ushort)port) : new UDP());
        Setup<P2P>("p2p", 420, -1, false, address => address is not null ? address is Multihash multihash ? new P2P(multihash) : new P2P((string)address) : new P2P());
#pragma warning disable CS0618 // Type or member is obsolete
        Setup<IPFS>("ipfs", 421, -1, false, address => address is not null ? address is Multihash multihash ? new IPFS(multihash) : new IPFS((string)address) : new IPFS());
#pragma warning restore CS0618 // Type or member is obsolete
        Setup<WebSocket>("ws", 477, 0, false, _ => new WebSocket());
        Setup<WebSocketSecure>("wss", 478, 0, false, _ => new WebSocketSecure());
        Setup<DCCP>("dccp", 33, 16, false, port => port is not null ? new DCCP((short)port) : new DCCP());
        Setup<SCTP>("sctp", 132, 16, false, port => port is not null ? new SCTP((short)port) : new SCTP());
        Setup<Unix>("unix", 400, -1, true, address => address is not null ? new Unix((string)address) : new Unix());
        Setup<Onion>("onion", 444, 96, false, address => address is not null ? new Onion((string)address) : new Onion());
        Setup<QUIC>("quic", 460, 0, false, _ => new QUIC());
        Setup<HTTP>("http", 480, 0, false, _ => new HTTP());
        Setup<HTTPS>("https", 443, 0, false, _ => new HTTPS());
        Setup<UTP>("utp", 301, 0, false, _ => new UTP());
        Setup<UDT>("udt", 302, 0, false, _ => new UDT());
        Setup<DNS>("dns", 53, -1, false, address => address is not null ? new DNS((string)address) : new DNS());
        Setup<DNS4>("dns4", 54, -1, false, address => address is not null ? new DNS4((string)address) : new DNS4());
        Setup<DNS6>("dns6", 55, -1, false, address => address is not null ? new DNS6((string)address) : new DNS6());
        Setup<P2PCircuit>("p2p-circuit", 290, 0, false, _ => new P2PCircuit());
        Setup<P2PWebRTCStar>("p2p-webrtc-star", 275, 0, false, _ => new P2PWebRTCStar());
        Setup<P2PWebRTCDirect>("p2p-webrtc-direct", 276, 0, false, _ => new P2PWebRTCStar());
        Setup<P2PWebSocketStar>("p2p-websocket-star", 479, 0, false, _ => new P2PWebSocketStar());
    }

    /// <summary>Initializes a new instance of the <see cref="Multiaddress"/> class.</summary>
    public Multiaddress()
    {
        Protocols = new List<MultiaddressProtocol>();
    }

    /// <summary>Gets the protocols.</summary>
    /// <value>The protocols.</value>
    public List<MultiaddressProtocol> Protocols { get; }

    /// <summary>Decodes the specified value.</summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static Multiaddress Decode(string value)
    {
        return new Multiaddress().Add(DecodeProtocols(value.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)).ToArray());
    }

    /// <summary>Decodes the specified bytes.</summary>
    /// <param name="bytes">The bytes.</param>
    /// <returns></returns>
    public static Multiaddress Decode(byte[] bytes)
    {
        return new Multiaddress().Add(DecodeProtocols(bytes).ToArray());
    }

    /// <summary>Joins the specified addresses.</summary>
    /// <param name="addresses">The addresses.</param>
    /// <returns></returns>
    public static Multiaddress Join(IEnumerable<Multiaddress> addresses)
    {
        Multiaddress result = new();
        foreach (Multiaddress address in addresses)
        {
            _ = result.Add(address.Protocols.ToArray());
        }

        return result;
    }

    /// <summary>Adds the specified value.</summary>
    /// <typeparam name="TProtocol">The type of the protocol.</typeparam>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public Multiaddress Add<TProtocol>(object value)
        where TProtocol : MultiaddressProtocol
    {
        Protocol proto = _protocols.SingleOrDefault(p => p.Type == typeof(TProtocol));
        Protocols.Add(proto.Factory(value));
        return this;
    }

    /// <summary>Adds this instance.</summary>
    /// <typeparam name="TProtocol">The type of the protocol.</typeparam>
    /// <returns></returns>
    public Multiaddress Add<TProtocol>() where TProtocol : MultiaddressProtocol
    {
        return Add<TProtocol>(null);
    }

    /// <summary>Adds the specified protocols.</summary>
    /// <param name="protocols">The protocols.</param>
    /// <returns></returns>
    public Multiaddress Add(params MultiaddressProtocol[] protocols)
    {
        Protocols.AddRange(protocols);
        return this;
    }

    /// <summary>Decapsulates the specified address.</summary>
    /// <param name="address">The address.</param>
    /// <returns></returns>
    public Multiaddress Decapsulate(Multiaddress address)
    {
        return new Multiaddress()
            .Add(Protocols.TakeWhile(p => !address.Protocols.Any(p.Equals)).ToArray());
    }

    /// <summary>Encapsulates the specified address.</summary>
    /// <param name="address">The address.</param>
    /// <returns></returns>
    public Multiaddress Encapsulate(Multiaddress address)
    {
        return new Multiaddress()
            .Add(Protocols.Concat(address.Protocols).ToArray());
    }

    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
    /// </returns>
    public override bool Equals(object obj)
    {
        return Equals((Multiaddress)obj);
    }

    /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    public bool Equals(Multiaddress other)
    {
        return other is not null && ToBytes().SequenceEqual(other.ToBytes());
    }

    /// <summary>Gets this instance.</summary>
    /// <typeparam name="TProtocol">The type of the protocol.</typeparam>
    /// <returns></returns>
    public TProtocol Get<TProtocol>() where TProtocol : MultiaddressProtocol
    {
        return Protocols.OfType<TProtocol>().SingleOrDefault();
    }

    /// <summary>Removes this instance.</summary>
    /// <typeparam name="TProtocol">The type of the protocol.</typeparam>
    public void Remove<TProtocol>() where TProtocol : MultiaddressProtocol
    {
        TProtocol protocol = Get<TProtocol>();
        if (protocol is not null)
        {
            _ = Protocols.Remove(protocol);
        }
    }

    /// <summary>Splits this instance.</summary>
    /// <returns></returns>
    public IEnumerable<Multiaddress> Split()
    {
        return Protocols.Select(p => new Multiaddress().Add(p));
    }

    /// <summary>Converts to bytes.</summary>
    /// <returns></returns>
    public byte[] ToBytes()
    {
        return Protocols.SelectMany(EncodeProtocol).ToArray();
    }

    /// <summary>Converts to string.</summary>
    /// <returns>A <see cref="string" /> that represents this instance.</returns>
    public override string ToString()
    {
        return Protocols.Count > 0 ? "/" + string.Join("/", Protocols.SelectMany(ProtocolToStrings)) : string.Empty;
    }

    private static MultiaddressProtocol CreateProtocol(string name)
    {
        return _protocols.SingleOrDefault(p => p.Name == name)?.Factory(null);
    }

    private static MultiaddressProtocol CreateProtocol(int code)
    {
        return _protocols.SingleOrDefault(p => p.Code == code)?.Factory(null);
    }

    private static int DecodeProtocol(MultiaddressProtocol protocol, byte[] bytes, int offset)
    {
        int start = offset;
        int count = 0;
        if (protocol.Size > 0)
        {
            count = protocol.Size / 8;
        }
        else if (protocol.Size == -1)
        {
            offset += Binary.Varint.Read(bytes, offset, out uint proxy);
            count = (int)proxy;
        }

        if (count > 0)
        {
            protocol.Decode(bytes.Slice(offset, count));
            offset += count;
        }

        return offset - start;
    }

    private static IEnumerable<MultiaddressProtocol> DecodeProtocols(params string[] parts)
    {
        for (int i = 0; i < parts.Length; i++)
        {
            if (!SupportsProtocol(parts[i]))
            {
                throw new NotSupportedException(parts[i]);
            }

            MultiaddressProtocol protocol = CreateProtocol(parts[i]);
            if (protocol.Size != 0)
            {
                if (i + 1 >= parts.Length)
                {
                    throw new Exception("Required parameter not found");
                }

                if (_protocols.SingleOrDefault(p => p.Code == protocol.Code).Path)
                {
                    protocol.Decode(string.Join("/", parts.Slice(i + 1)));
                    i = parts.Length - 1;
                }
                else
                {
                    protocol.Decode(parts[++i]);
                }
            }

            yield return protocol;
        }
    }

    private static IEnumerable<MultiaddressProtocol> DecodeProtocols(byte[] bytes)
    {
        int offset = 0;
        while (offset < bytes.Length)
        {
            offset += ParseProtocolCode(bytes, offset, out short code);
            if (SupportsProtocol(code))
            {
                offset += ParseProtocol(bytes, offset, code, out MultiaddressProtocol protocol);

                yield return protocol;
            }
        }
    }

    private static IEnumerable<byte> EncodeProtocol(MultiaddressProtocol p)
    {
        byte[] code = Binary.Varint.GetBytes((ulong)p.Code);

        if (p.Size == 0)
        {
            return code;
        }

        byte[] bytes = p.ToBytes();

        if (p.Size > 0)
        {
            return code.Concat(bytes);
        }

        byte[] prefix = Binary.Varint.GetBytes((ulong)bytes.Length);

        return code.Concat(prefix).Concat(bytes);
    }

    private static int ParseProtocol(byte[] bytes, int offset, short code, out MultiaddressProtocol protocol)
    {
        int start = offset;
        protocol = CreateProtocol(code);
        offset += DecodeProtocol(protocol, bytes, offset);
        return offset - start;
    }

    private static int ParseProtocolCode(byte[] bytes, int offset, out short code)
    {
        code = Binary.LittleEndian.GetInt16(bytes, offset);
        return 2;
    }

    private static IEnumerable<string> ProtocolToStrings(MultiaddressProtocol p)
    {
        yield return p.Name;
        if (p.Value is not null)
        {
            yield return p.Value.ToString();
        }
    }

    private static void Setup<TProtocol>(string name, int code, int size, bool path, Func<object, MultiaddressProtocol> factory)
        where TProtocol : MultiaddressProtocol
    {
        _protocols.Add(new Protocol(name, code, size, typeof(TProtocol), path, factory));
    }

    private static bool SupportsProtocol(string name)
    {
        return _protocols.Any(p => p.Name.Equals(name));
    }

    private static bool SupportsProtocol(int code)
    {
        return _protocols.Any(p => p.Code.Equals(code));
    }
}
