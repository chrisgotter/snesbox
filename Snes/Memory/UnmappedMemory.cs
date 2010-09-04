using System;

namespace Snes.Memory
{
    class UnmappedMemory : Memory
    {
        public override uint size() { throw new NotImplementedException(); }
        public override byte read(uint addr) { throw new NotImplementedException(); }
        public override void write(uint addr, byte data) { throw new NotImplementedException(); }
    }
}
