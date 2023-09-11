namespace Multiformats.Address.Protocols;/// <summary>
/// P2PWebSocketStar
/// </summary>
public record P2PWebSocketStar : MultiaddressProtocol{
    /// <summary>
    /// Constructor for P2PWebSocketStar class.
    /// </summary>
    /// <returns>
    /// An instance of the P2PWebSocketStar class.
    /// </returns>
    public P2PWebSocketStar()
        : base("p2p-websocket-star", 479, 0)    {    }

    /// <summary>
    /// Decodes the specified value.
    /// </summary>
    /// <param name="value">The value to decode.</param>
    public override void Decode(string value)    {    }

    /// <summary>
    /// Decodes the specified bytes.
    /// </summary>
    /// <param name="bytes">The bytes.</param>
    public override void Decode(byte[] bytes)    {    }

    /// <summary>
    /// Converts the object to a byte array.
    /// </summary>
    public override byte[] ToBytes() => EmptyBuffer;}
