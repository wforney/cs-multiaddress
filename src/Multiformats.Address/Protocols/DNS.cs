namespace Multiformats.Address.Protocols;

using System.Text;

/// <summary>
/// DNS
/// </summary>
public record DNS : MultiaddressProtocol{    /// <summary>
     /// Constructor for DNS class. 
     /// </summary>
     /// <returns>
     /// An instance of the DNS class.
     /// </returns>
    public DNS()
            : base("dns", 53, -1)    {    }

    /// <summary>
    /// Constructor for DNS class that takes a string address as a parameter.
    /// </summary>
    /// <param name="address">The address to be used for the DNS.</param>
    /// <returns>A new instance of the DNS class.</returns>
    public DNS(string address)
            : this() => this.Value = address;

    /// <summary>
    /// Decodes the given string value.
    /// </summary>
    /// <param name="value">The string value to decode.</param>
    public override void Decode(string value) => this.Value = value;

    /// <summary>
    /// Decodes the given byte array into a string using UTF8 encoding.
    /// </summary>
    /// <param name="bytes">The byte array to decode.</param>
    public override void Decode(byte[] bytes) => this.Value = Encoding.UTF8.GetString(bytes);

    /// <summary>
    /// Converts the value of this instance to a UTF-8 encoded byte array.
    /// </summary>
    /// <returns>A UTF-8 encoded byte array.</returns>
    public override byte[] ToBytes() => Encoding.UTF8.GetBytes((string?)this.Value ?? string.Empty);

    /// <summary>
    /// Returns the string representation of the Value property.
    /// </summary>
    /// <returns>
    /// The string representation of the Value property, or an empty string if Value is null.
    /// </returns>
    public override string ToString() => (string?)this.Value ?? string.Empty;}
