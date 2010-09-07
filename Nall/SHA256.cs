using System;

namespace Nall
{
    public class SHA256
    {
        public byte[] _in = new byte[64];
        public uint inlen;

        public uint[] w = new uint[64];
        public uint[] h = new uint[8];
        public ulong len;

        public static void init(SHA256 p) { throw new NotImplementedException(); }

        public static void block(SHA256 p) { throw new NotImplementedException(); }

        public static void chunk(SHA256 p, byte[] s, uint len) { throw new NotImplementedException(); }

        public static void final(SHA256 p) { throw new NotImplementedException(); }

        public static void hash(SHA256 p, byte[] s) { throw new NotImplementedException(); }
    }
}
