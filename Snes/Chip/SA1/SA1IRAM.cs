using System;

namespace Snes.Chip.SA1
{
    class SA1IRAM : Snes.Memory.Memory
    {
        public static SA1IRAM sa1iram = new SA1IRAM();

        public override uint size() { throw new NotImplementedException(); }
        public override byte read(uint addr) { throw new NotImplementedException(); }
        public override void write(uint addr, byte data) { throw new NotImplementedException(); }
    }
}
