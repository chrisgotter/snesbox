using System;

namespace Snes.Chip.DSP2
{
    class DSP2SR : Memory.Memory
    {
        public override uint size() { throw new NotImplementedException(); }
        public override byte read(uint addr) { throw new NotImplementedException(); }
        public override void write(uint addr, byte data) { throw new NotImplementedException(); }
    }
}
