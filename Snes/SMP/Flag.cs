using System;

namespace Snes
{
    partial class SMPCore
    {
        public class Flag
        {
            public byte n, v, p, b, h, i, z, c;
            public static explicit operator uint(Flag flag) { throw new NotImplementedException(); }
            public uint Assign(byte data) { throw new NotImplementedException(); }
            public static uint operator |(Flag flag, uint data) { throw new NotImplementedException(); }
            public static uint operator ^(Flag flag, uint data) { throw new NotImplementedException(); }
            public static uint operator &(Flag flag, uint data) { throw new NotImplementedException(); }
            public Flag() { throw new NotImplementedException(); }
        }
    }
}
