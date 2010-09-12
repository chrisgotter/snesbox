using System;

namespace Snes
{
    partial class CPUCore
    {
        public class Flag
        {
            public bool n, v, m, x, d, i, z, c;

            public static explicit operator uint(Flag flag)
            {
                throw new NotImplementedException();
            }

            public uint Assign(byte data)
            {
                throw new NotImplementedException();
            }

            public static uint operator |(Flag flag, uint data)
            {
                throw new NotImplementedException();
            }

            public static uint operator ^(Flag flag, uint data)
            {
                throw new NotImplementedException();
            }

            public static uint operator &(Flag flag, uint data)
            {
                throw new NotImplementedException();
            }

            public Flag()
            {
                throw new NotImplementedException();
            }
        }
    }
}
