using System;

namespace Snes.Chip.SuperFX
{
    partial class SuperFX
    {
        public class scmr_t
        {
            public uint ht;
            public bool ron;
            public bool ran;
            public uint md;

            public static explicit operator uint(scmr_t flag) { throw new NotImplementedException(); }

            public scmr_t Assign(byte data) { throw new NotImplementedException(); }
        }
    }
}
