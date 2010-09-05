using System;

namespace Snes.Chip.SuperFX
{
    partial class SuperFX
    {
        public class cfgr_t
        {
            public bool irq;
            public bool ms0;

            public static explicit operator uint(cfgr_t flag) { throw new NotImplementedException(); }

            public cfgr_t Assign(byte data) { throw new NotImplementedException(); }
        }
    }
}
