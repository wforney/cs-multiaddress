namespace Multiformats.Address.Protocols;/// <summary>
/// P2PCircuit
/// </summary>
public record P2PCircuit : MultiaddressProtocol{
    /// <summary>
    /// Constructor for the P2PCircuit class.
    /// </summary>
    /// <returns>
    /// An instance of the P2PCircuit class.
    /// </returns>
    public P2PCircuit()
        : base("p2p-circuit", 290, 0)    {    }

    /// <summary>
    /// Decodes the specified value.
    /// </summary>
    /// <param name="value">The value to decode.</param>
    public override void Decode(string value)    {    }

    /// <summary>
    /// Decodes the given byte array.
    /// </summary>
    /// <param name="bytes">The byte array to decode.</param>
    public override void Decode(byte[] bytes)    {    }

    /// <summary>
    /// Converts the object to a byte array.
    /// </summary>
    /// <returns>An empty byte array.</returns>
    public override byte[] ToBytes() => EmptyBuffer;}
