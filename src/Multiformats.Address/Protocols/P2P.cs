namespace Multiformats.Address.Protocols;
using Multiformats.Hash;

public class P2P : MultiaddressProtocol
{
    public P2P()
        : base("p2p", 420, -1)
    {
    }

    public P2P(string address)
        : this(Multihash.FromB58String(address))
    {
    }

    public P2P(Multihash address)
        : this()
    {
        Value = address;
    }

    public override void Decode(string value)
    {
        Value = Multihash.FromB58String(value);
    }

    public override void Decode(byte[] bytes)
    {
        Value = Multihash.Decode(bytes);
    }

    public override byte[] ToBytes()
    {
        return (Multihash)Value;
    }

    public override string ToString()
    {
        return ((Multihash)Value)?.B58String() ?? string.Empty;
    }
}
