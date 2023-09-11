namespace Multiformats.Address.Protocols;
/// <summary>
/// HTTPS
/// </summary>
public record HTTPS : MultiaddressProtocol{
    /// <summary>
    /// Constructor for HTTPS class.
    /// </summary>
    /// <returns>
    /// An instance of the HTTPS class.
    /// </returns>
    public HTTPS()
            : base("https", 480, 0)    {    }

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
