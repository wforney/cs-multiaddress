namespace Multiformats.Address.Protocols;

using System;

/// <summary>
/// Represents a Multiaddress protocol.
/// </summary>
public abstract record MultiaddressProtocol(
    string Name,
    int Code,
    int Size)
{
    /// <summary>
    /// Property to get and set the value of an object.
    /// </summary>
    public object? Value { get; protected set; }

    /// <summary>
    /// An empty buffer
    /// </summary>
    protected static readonly byte[] EmptyBuffer = Array.Empty<byte>();

    /// <summary>
    /// Decodes the specified value.
    /// </summary>
    /// <param name="value">The value to decode.</param>
    public abstract void Decode(string value);

    /// <summary>
    /// Decodes a byte array into a meaningful representation.
    /// </summary>
    public abstract void Decode(byte[] bytes);

    /// <summary>
    /// Converts the object to a byte array.
    /// </summary>
    public abstract byte[] ToBytes();

    /// <summary>
    /// Returns the string representation of the Value property, or an empty string if Value is null.
    /// </summary>
    /// <returns>
    /// The string representation of the Value property, or an empty string if Value is null.
    /// </returns>
    public override string ToString() => this.Value?.ToString() ?? string.Empty;
}
