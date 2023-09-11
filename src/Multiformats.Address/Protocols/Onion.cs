namespace Multiformats.Address.Protocols;

using BinaryEncoding;using Multiformats.Base;using System;using System.Linq;/// <summary>
/// Onion
/// </summary>
public record Onion : MultiaddressProtocol{
    /// <summary>
    /// Gets the address of the object, or an empty string if the value is null.
    /// </summary>
    public string Address => this.Value is null ? string.Empty : (string)this.Value;

    /// <summary>
    /// Constructor for Onion class which inherits from the base class.
    /// </summary>
    /// <returns>
    /// An instance of the Onion class.
    /// </returns>
    public Onion()
        : base("onion", 444, 96)    {    }

    /// <summary>
    /// Constructor for Onion class that takes a string as an argument.
    /// </summary>
    /// <param name="s">String to be used for the Onion object.</param>
    /// <returns>An Onion object with the given string as its value.</returns>
    public Onion(string s)
        : this() => this.Value = s;

    /// <summary>
    /// Decodes a given onion address string into its components.
    /// </summary>
    /// <param name="value">The onion address string to decode.</param>
    public override void Decode(string value)    {        string[] addr = value.Split(':');        if (addr.Length != 2)        {            throw new Exception("Failed to parse addr");        }        if (addr[0].Length != 16)        {            throw new Exception("Failed to parse addr");        }        if (!Multibase.TryDecode(addr[0], out MultibaseEncoding encoding, out _) || encoding != MultibaseEncoding.Base32Lower)        {            throw new InvalidOperationException($"{value} is not a valid onion address.");        }        ushort i = ushort.Parse(addr[1]);        if (i < 1)        {            throw new Exception("Failed to parse addr");        }        this.Value = value;    }

    /// <summary>
    /// Decodes a byte array into a string containing an address and port.
    /// </summary>
    /// <param name="bytes">The byte array to decode.</param>
    public override void Decode(byte[] bytes)    {        string addr = Multibase.Base32.Encode(bytes.AsSpan()[..10].ToArray());        ushort port = Binary.BigEndian.GetUInt16(bytes, 10);        this.Value = $"{addr}:{port}";    }

    /// <summary>
    /// Converts an onion address to a byte array.
    /// </summary>
    /// <returns>
    /// A byte array representing the onion address.
    /// </returns>
    public override byte[] ToBytes()    {        string? s = (string?)this.Value;        string[] addr = s?.Split(':') ?? Array.Empty<string>();        if (addr.Length != 2)        {            throw new Exception("Failed to parse addr");        }        if (addr[0].Length != 16)        {            throw new Exception("Failed to parse addr");        }        if (!Multibase.TryDecode(addr[0], out MultibaseEncoding encoding, out byte[] onionHostBytes) || encoding != MultibaseEncoding.Base32Lower)        {            throw new InvalidOperationException($"{s} is not a valid onion address.");        }        ushort i = ushort.Parse(addr[1]);        return i < 1 ? throw new Exception("Failed to parse addr") : onionHostBytes.Concat(Binary.BigEndian.GetBytes(i)).ToArray();    }}
