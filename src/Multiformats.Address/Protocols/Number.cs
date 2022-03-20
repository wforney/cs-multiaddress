namespace Multiformats.Address.Protocols;
using BinaryEncoding;
using System.Globalization;

public abstract class Number : MultiaddressProtocol
{
    public ushort Port => (ushort?)Value ?? 0;

    protected Number(string name, int code)
        : base(name, code, 16)
    {
    }

    public override void Decode(string value)
    {
        Value = ushort.Parse(value, NumberStyles.Number);
    }

    public override void Decode(byte[] bytes)
    {
        Value = Binary.BigEndian.GetUInt16(bytes, 0);
    }

    public override byte[] ToBytes()
    {
        return Binary.BigEndian.GetBytes((ushort)Value);
    }
}
