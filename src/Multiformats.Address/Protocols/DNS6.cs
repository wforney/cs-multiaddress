namespace Multiformats.Address.Protocols;

using System.Text;/// <summary>
/// DNS6
/// </summary>
public record DNS6 : MultiaddressProtocol{
    /// <summary>
    /// Constructor for DNS6 class.
    /// </summary>
    /// <returns>
    /// An instance of the DNS6 class.
    /// </returns>
    public DNS6()
            : base("dns6", 55, -1)    {    }

    /// <summary>
    /// Constructor for DNS6 class that sets the Value property to the given address.
    /// </summary>
    public DNS6(string address)
            : this() => this.Value = address;

    /// <summary>
    /// Decodes the given string and sets it as the value of the object.
    /// </summary>
    public override void Decode(string value) => this.Value = value;

    /// <summary>
    /// Decodes a byte array into a string using UTF8 encoding.
    /// </summary>
    public override void Decode(byte[] bytes) => this.Value = Encoding.UTF8.GetString(bytes);

    /// <summary>
    /// Converts the value of this instance to a byte array using UTF8 encoding.
    /// </summary>
    public override byte[] ToBytes() => this.Value is not null ? Encoding.UTF8.GetBytes((string)this.Value) : Array.Empty<byte>();

    /// <summary>
    /// Returns the string representation of the Value property, or an empty string if the Value property is null.
    /// </summary>
    public override string ToString() => (string?)this.Value ?? string.Empty;}
