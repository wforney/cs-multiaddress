namespace Multiformats.Address.Protocols;using Multiformats.Hash;/// <summary>
/// P2P
/// </summary>
public record P2P : MultiaddressProtocol{
    /// <summary>
    /// Constructor for the P2P class.
    /// </summary>
    /// <returns>
    /// An instance of the P2P class.
    /// </returns>
    public P2P()
        : base("p2p", 420, -1)    {    }

    /// <summary>
    /// Constructs a P2P object from a given address.
    /// </summary>
    /// <param name="address">The address of the P2P object.</param>
    /// <returns>A P2P object.</returns>
    [Obsolete("This constructor is obsolete.")]
    public P2P(string address)        : this(Multihash.FromB58String(address))    {    }

    /// <summary>
    /// Constructor for P2P class with a given Multihash address.
    /// </summary>
    /// <param name="address">The Multihash address.</param>
    /// <returns>A new instance of the P2P class.</returns>
    public P2P(Multihash address)
            : this() => this.Value = address;

#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
    /// <summary>
    /// Decodes a Base58 encoded string into a Multihash object.
    /// </summary>
    /// <param name="value">The Base58 encoded string.</param>
    [Obsolete("Use byte array decode instead.")]
    public override void Decode(string value) => this.Value = Multihash.FromB58String(value);
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member

    /// <summary>
    /// Decodes the given byte array into a Multihash object.
    /// </summary>
    /// <param name="bytes">The byte array to decode.</param>
    public override void Decode(byte[] bytes) => this.Value = Multihash.Decode(bytes);

    /// <summary>
    /// Converts the multihash value to a byte array.
    /// </summary>
    /// <returns>A byte array representing the multihash value.</returns>
    public override byte[] ToBytes() => (Multihash?)this.Value ?? Array.Empty<byte>();

#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
    /// <summary>
    /// Returns the Base58 string representation of the Multihash value.
    /// </summary>
    /// <returns>The Base58 string representation of the Multihash value.</returns>
    [Obsolete("Do not use ToString. Use ToBytes.")]
    public override string ToString() => ((Multihash?)this.Value)?.B58String() ?? string.Empty;
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
}
