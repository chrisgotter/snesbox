using System;

namespace Snes.Chip.DSP3
{
    class DSP3 : Memory.Memory
    {
        public override uint size() { throw new NotImplementedException(); }
        public void init() { throw new NotImplementedException(); }
        public void enable() { throw new NotImplementedException(); }
        public void power() { throw new NotImplementedException(); }
        public void reset() { throw new NotImplementedException(); }

        public override byte read(uint addr) { throw new NotImplementedException(); }
        public override void write(uint addr, byte data) { throw new NotImplementedException(); }
    }
}
