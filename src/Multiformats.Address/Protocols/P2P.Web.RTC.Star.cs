namespace Multiformats.Address.Protocols;/// <summary>
/// P2PWebRTCStar
/// </summary>
public record P2PWebRTCStar : MultiaddressProtocol{
    /// <summary>
    /// Constructor for P2PWebRTCStar class. 
    /// </summary>
    /// <returns>
    /// An instance of the P2PWebRTCStar class. 
    /// </returns>
    public P2PWebRTCStar()
        : base("p2p-webrtc-star", 275, 0)    {    }

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
