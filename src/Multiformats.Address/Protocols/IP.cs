namespace Multiformats.Address.Protocols;using System.Net;/// <summary>
/// IP
/// </summary>
public abstract record IP : MultiaddressProtocol{
    /// <summary>
    /// Gets the IP address associated with the current instance of the IPAddressValue class.
    /// </summary>
    /// <returns>An IPAddress object that contains the IP address associated with the current instance of the IPAddressValue class.</returns>
    public IPAddress Address => this.Value != null ? (IPAddress)this.Value : IPAddress.None;

    /// <summary>
    /// Constructor for the IP class which inherits from the base class. 
    /// </summary>
    /// <param name="name">Name of the IP.</param>
    /// <param name="code">Code of the IP.</param>
    /// <param name="size">Size of the IP.</param>
    /// <returns>
    /// No return value.
    /// </returns>
    protected IP(string name, int code, int size)
            : base(name, code, size)    {    }

    /// <summary>
    /// Decodes the given string into an IPAddress object.
    /// </summary>
    /// <param name="value">The string to decode.</param>
    public override void Decode(string value) => this.Value = IPAddress.Parse(value);

    /// <summary>
    /// Decodes the given byte array into an IPAddress object.
    /// </summary>
    /// <param name="bytes">The byte array to decode.</param>
    public override void Decode(byte[] bytes) => this.Value = new IPAddress(bytes);

    /// <summary>
    /// Converts the address to a byte array.
    /// </summary>
    /// <returns>A byte array representing the address.</returns>
    public override byte[] ToBytes() => this.Address.GetAddressBytes();

    /// <summary>
    /// Returns a string representation of the Address object.
    /// </summary>
    /// <returns>
    /// A string representation of the Address object.
    /// </returns>
    public override string ToString() => this.Address.ToString();}
