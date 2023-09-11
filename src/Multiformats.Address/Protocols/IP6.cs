namespace Multiformats.Address.Protocols;

using System;using System.Net;using System.Net.Sockets;/// <summary>
/// Represents an IPv6 address.
/// </summary>
public record IP6 : IP{
    /// <summary>
    /// Constructor for IP6 class.
    /// </summary>
    /// <returns>
    /// An instance of the IP6 class.
    /// </returns>
    public IP6()
        : base("ip6", 41, 128)    {    }

    /// <summary>
    /// Constructor for IP6 class that takes an IPAddress as a parameter.
    /// </summary>
    /// <param name="address">IPAddress to be used for the IP6 object.</param>
    /// <returns>IP6 object with the given IPAddress.</returns>
    public IP6(IPAddress address)
        : this()    {        if (address.AddressFamily != AddressFamily.InterNetworkV6)        {            throw new Exception("Address is not IPv6");        }        this.Value = address;    }

    /// <summary>
    /// Decodes the specified value and checks if it is an IPv6 address.
    /// </summary>
    /// <param name="value">The value to decode.</param>
    public override void Decode(string value)    {        base.Decode(value);        if (this.Value != null && ((IPAddress)this.Value).AddressFamily != AddressFamily.InterNetworkV6)        {            throw new Exception("Address is not IPv6");        }    }

    /// <summary>
    /// Decodes the given byte array into an IPv6 address.
    /// </summary>
    /// <param name="bytes">The byte array to decode.</param>
    public override void Decode(byte[] bytes)    {        base.Decode(bytes);        if (this.Value != null && ((IPAddress)this.Value).AddressFamily != AddressFamily.InterNetworkV6)        {            throw new Exception("Address is not IPv6");        }    }}
