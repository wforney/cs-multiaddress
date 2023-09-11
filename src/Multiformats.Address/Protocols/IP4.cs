namespace Multiformats.Address.Protocols;
using System;using System.Net;using System.Net.Sockets;/// <summary>
/// Represents an IP4 address. Inherits from the IP class.
/// </summary>
public record IP4 : IP{
    /// <summary>
    /// Constructor for IP4 class. 
    /// </summary>
    /// <returns>
    /// An instance of IP4 class.
    /// </returns>
    public IP4()
        : base("ip4", 4, 32)    {    }

    /// <summary>
    /// Constructs an IP4 object from an IPAddress object.
    /// </summary>
    /// <param name="address">The IPAddress object to construct from.</param>
    /// <returns>An IP4 object.</returns>
    public IP4(IPAddress address)
        : this()    {        if (address.AddressFamily != AddressFamily.InterNetwork)        {            throw new Exception("Address is not IPv4");        }        this.Value = address;    }

    /// <summary>
    /// Decodes the specified value and checks if the address is an IPv4 address. 
    /// Throws an exception if the address is not an IPv4 address. 
    /// </summary>
    /// <param name="value">The value to decode.</param>
    public override void Decode(string value)    {        base.Decode(value);        if (this.Value != null && ((IPAddress)this.Value).AddressFamily != AddressFamily.InterNetwork)        {            throw new Exception("Address is not IPv4");        }    }

    /// <summary>
    /// Decodes the given byte array and checks if the address is an IPv4 address. 
    /// </summary>
    /// <param name="bytes">The byte array to decode.</param>
    public override void Decode(byte[] bytes)    {        base.Decode(bytes);        if (this.Value != null && ((IPAddress)this.Value).AddressFamily != AddressFamily.InterNetwork)        {            throw new Exception("Address is not IPv4");        }    }}
