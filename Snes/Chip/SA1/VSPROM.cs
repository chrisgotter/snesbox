﻿using System;

namespace Snes.Chip.SA1
{
    class VSPROM : Snes.Memory.Memory
    {
        public static VSPROM vsprom = new VSPROM();

        public override uint size() { throw new NotImplementedException(); }
        public override byte read(uint addr) { throw new NotImplementedException(); }
        public override void write(uint addr, byte data) { throw new NotImplementedException(); }
    }
}
