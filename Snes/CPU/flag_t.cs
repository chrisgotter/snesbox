using System;

namespace Snes.CPU
{
    partial class CPUCore
    {
        public class flag_t
        {
            public byte n, v, m, x, d, i, z, c;
            public static explicit operator uint(flag_t core) { throw new NotImplementedException(); }
            public uint Assign(byte data) { throw new NotImplementedException(); }
            public static uint operator |(flag_t left, uint data) { throw new NotImplementedException(); }
            public static uint operator ^(flag_t left, uint data) { throw new NotImplementedException(); }
            public static uint operator &(flag_t left, uint data) { throw new NotImplementedException(); }
            public flag_t() { throw new NotImplementedException(); }
        }
    }
}
