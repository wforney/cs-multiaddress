namespace Multiformats.Address.Protocols;

using BinaryEncoding;
using System;
using System.Linq;
using System.Text;

/// <summary>
/// Unix
/// </summary>
public record Unix : MultiaddressProtocol
{
    /// <summary>
    /// Gets the path.
    /// </summary>
    public string Path => this.Value is null ? string.Empty : (string)this.Value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Unix"/> class.
    /// </summary>
    public Unix()
        : base("unix", 400, -1)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Unix"/> class.
    /// </summary>
    /// <param name="address">The address.</param>
    public Unix(string address)
        : this() => this.Value = address;

    /// <inheritdoc/>
    public override void Decode(string value) => this.Value = value;

    /// <inheritdoc/>
    public override void Decode(byte[] bytes)
    {
        int n = Binary.Varint.Read(bytes, 0, out uint size);

        if (bytes.Length - n != size)
        {
            throw new Exception("Inconsitent lengths");
        }

        if (size == 0)
        {
            throw new Exception("Invalid length");
        }

        string s = Encoding.UTF8.GetString(bytes, n, bytes.Length - n);

        this.Value = s[1..];
    }

    /// <inheritdoc/>
    public override byte[] ToBytes()
    {
        return Binary.Varint.GetBytes(
            (uint)Encoding.UTF8.GetByteCount((string?)this.Value ?? string.Empty))
            .Concat(Encoding.UTF8.GetBytes((string?)this.Value ?? string.Empty))
            .ToArray();
    }
}
