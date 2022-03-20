namespace Multiformats.Address.Protocols;
using System.Text;

public class DNS6 : MultiaddressProtocol
{
    public DNS6()
        : base("dns6", 55, -1)
    {
    }

    public DNS6(string address)
        : this()
    {
        Value = address;
    }

    public override void Decode(string value)
    {
        Value = value;
    }

    public override void Decode(byte[] bytes)
    {
        Value = Encoding.UTF8.GetString(bytes);
    }

    public override byte[] ToBytes()
    {
        return Encoding.UTF8.GetBytes((string)Value);
    }

    public override string ToString()
    {
        return (string)Value ?? string.Empty;
    }
}
