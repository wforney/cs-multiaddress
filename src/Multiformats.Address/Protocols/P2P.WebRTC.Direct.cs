namespace Multiformats.Address.Protocols;/// <summary>
/// P2PWebRTCDirect
/// </summary>
public record P2PWebRTCDirect : MultiaddressProtocol{
    /// <summary>
    /// Constructor for P2PWebRTCDirect class.
    /// </summary>
    /// <returns>
    /// An instance of the P2PWebRTCDirect class.
    /// </returns>
    public P2PWebRTCDirect()
            : base("p2p-webrtc-direct", 276, 0)    {    }

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
