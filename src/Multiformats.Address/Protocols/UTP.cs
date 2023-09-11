namespace Multiformats.Address.Protocols;

/// <summary>
/// UTP
/// </summary>
public record UTP : MultiaddressProtocol
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UTP"/> class.
    /// </summary>
    public UTP()
        : base("utp", 301, 0)
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
    /// Decodes the specified bytes.
    /// </summary>
    /// <param name="bytes">The bytes.</param>
    public override void Decode(byte[] bytes)
    {
    }

    /// <summary>
    /// Converts to bytes.
    /// </summary>
    /// <returns>The bytes.</returns>
    public override byte[] ToBytes() => EmptyBuffer;
}
