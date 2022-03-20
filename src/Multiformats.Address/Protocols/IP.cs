namespace Multiformats.Address.Protocols;
using System.Net;

public abstract class IP : MultiaddressProtocol
{
    public IPAddress Address => Value != null ? (IPAddress)Value : IPAddress.None;

    protected IP(string name, int code, int size)
        : base(name, code, size)
    {
    }

    public override void Decode(string value)
    {
        Value = IPAddress.Parse(value);
    }

    public override void Decode(byte[] bytes)
    {
        Value = new IPAddress(bytes);
    }

    public override byte[] ToBytes()
    {
        return Address.GetAddressBytes();
    }

    public override string ToString()
    {
        return Address.ToString();
    }
}

