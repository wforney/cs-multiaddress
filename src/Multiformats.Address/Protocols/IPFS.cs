namespace Multiformats.Address.Protocols;

using Multiformats.Hash;using System;/// <summary>
/// IPFS
/// </summary>
[Obsolete("Use P2P instead")]public record IPFS : MultiaddressProtocol{
    /// <summary>
    /// Constructor for IPFS class.
    /// </summary>
    /// <returns>
    /// An instance of the IPFS class.
    /// </returns>
    public IPFS()
        : base("ipfs", 421, -1)    {    }

    /// <summary>
    /// Initializes a new instance of the IPFS class with the specified address.
    /// </summary>
    /// <param name="address">The address of the IPFS node.</param>
    /// <returns>
    /// A new instance of the IPFS class.
    /// </returns>
    public IPFS(string address)
        : this(Multihash.FromB58String(address))    {    }

    /// <summary>
    /// Constructor for IPFS class with a given Multihash address.
    /// </summary>
    /// <param name="address">The Multihash address.</param>
    /// <returns>An instance of the IPFS class.</returns>
    public IPFS(Multihash address)
        : this() => this.Value = address;

    /// <summary>
    /// Decodes a Base58 encoded string into a Multihash object.
    /// </summary>
    /// <param name="value">The Base58 encoded string.</param>
    public override void Decode(string value) => this.Value = Multihash.FromB58String(value);

    /// <summary>
    /// Decodes the given byte array into a Multihash object.
    /// </summary>
    /// <param name="bytes">The byte array to decode.</param>
    public override void Decode(byte[] bytes) => this.Value = Multihash.Decode(bytes);

    /// <summary>
    /// Converts the multihash value to a byte array.
    /// </summary>
    /// <returns>The multihash value as a byte array.</returns>
    public override byte[] ToBytes() => (Multihash?)this.Value ?? Array.Empty<byte>();

    /// <summary>
    /// Returns the Base58 string representation of the Multihash value.
    /// </summary>
    /// <returns>The Base58 string representation of the Multihash value.</returns>
    public override string ToString() => ((Multihash?)this.Value)?.B58String() ?? string.Empty;}
