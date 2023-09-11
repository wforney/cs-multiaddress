namespace Multiformats.Address.Protocols;

/// <summary>
/// WebSocket
/// </summary>
public record WebSocket : MultiaddressProtocol
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WebSocket"/> class.
    /// </summary>
    public WebSocket()
        : base("ws", 477, 0)
    {
    }

    /// <summary>
    /// Decodes the specified bytes.
    /// </summary>
    /// <param name="bytes">The bytes.</param>
    public override void Decode(byte[] bytes)
    {
    }

    /// <summary>
    /// Decodes the specified string.
    /// </summary>
    /// <param name="value">The string.</param>
    public override void Decode(string value)
    {
    }

    /// <summary>
    /// Converts to bytes.
    /// </summary>
    /// <returns>The bytes.</returns>
    public override byte[] ToBytes() => EmptyBuffer;
}
