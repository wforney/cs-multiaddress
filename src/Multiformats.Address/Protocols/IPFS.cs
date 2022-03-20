namespace Multiformats.Address.Protocols;
using Multiformats.Hash;
using System;

[Obsolete("Use P2P instead")]
public class IPFS : MultiaddressProtocol
{
    public IPFS()
        : base("ipfs", 421, -1)
    {
    }

    public IPFS(string address)
        : this(Multihash.FromB58String(address))
    {
    }

    public IPFS(Multihash address)
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
