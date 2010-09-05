using System;

namespace Snes.Chip.SPC7110
{
    class SPC7110DCU : Snes.Memory.Memory
    {
        public override byte read(uint addr) { throw new NotImplementedException(); }
        public override void write(uint addr, byte data) { throw new NotImplementedException(); }
    }
}
