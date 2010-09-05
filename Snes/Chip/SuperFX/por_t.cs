using System;

namespace Snes.Chip.SuperFX
{
    partial class SuperFX
    {
        public class por_t
        {
            public bool obj;
            public bool freezehigh;
            public bool highnibble;
            public bool dither;
            public bool transparent;

            public static explicit operator uint(por_t flag) { throw new NotImplementedException(); }

            public por_t Assign(byte data) { throw new NotImplementedException(); }
        }
    }
}
