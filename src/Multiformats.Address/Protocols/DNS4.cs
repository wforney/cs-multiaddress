namespace Multiformats.Address.Protocols;
using System.Text;

public class DNS4 : MultiaddressProtocol
{
    public DNS4()
        : base("dns4", 54, -1)
    {
    }

    public DNS4(string address)
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
