namespace Multiformats.Address.Protocols;
/// <summary>
/// HTTP
/// </summary>
public record HTTP : MultiaddressProtocol{
    /// <summary>
    /// Constructor for the HTTP class.
    /// </summary>
    /// <returns>
    /// An instance of the HTTP class.
    /// </returns>
    public HTTP()
        : base("http", 480, 0)    {    }

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
    /// <returns>An empty byte array.</returns>
    public override byte[] ToBytes() => EmptyBuffer;}
