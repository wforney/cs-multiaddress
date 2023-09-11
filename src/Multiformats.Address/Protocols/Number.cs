namespace Multiformats.Address.Protocols;

using BinaryEncoding;
using System.Globalization;

/// <summary>
/// The number record
/// </summary>
public abstract record Number : MultiaddressProtocol
{
    /// <summary>
    /// Constructor for the Number class.
    /// </summary>
    /// <param name="name">The name of the number.</param>
    /// <param name="code">The code of the number.</param>
    /// <returns>
    /// A new instance of the Number class.
    /// </returns>
    protected Number(string name, int code)
        : base(name, code, 16)
    {
    }

    /// <summary>
    /// Gets the port value of the Value property, or 0 if Value is null.
    /// </summary>
    public ushort Port => (ushort?)this.Value ?? 0;

    /// <summary>
    /// Decodes the specified value into a ushort.
    /// </summary>
    /// <param name="value">The value to decode.</param>
    public override void Decode(string value) => this.Value = ushort.Parse(value, NumberStyles.Number);

    /// <summary>
    /// Decodes a byte array into a UInt16 value.
    /// </summary>
    /// <param name="bytes">The byte array to decode.</param>
    public override void Decode(byte[] bytes) => this.Value = Binary.BigEndian.GetUInt16(bytes, 0);

    /// <summary>
    /// Converts the value of this instance to a byte array in big-endian format.
    /// </summary>
    /// <returns>A byte array in big-endian format.</returns>
    public override byte[] ToBytes() => this.Value is not null ? Binary.BigEndian.GetBytes((ushort)this.Value) : Array.Empty<byte>();
}
