using System;

namespace Snes.Chip.SPC7110
{
    class SPC7110RAM : Snes.Memory.Memory
    {
        public static SPC7110RAM spc7110ram = new SPC7110RAM();

        public override uint size() { throw new NotImplementedException(); }
        public override byte read(uint addr) { throw new NotImplementedException(); }
        public override void write(uint addr, byte data) { throw new NotImplementedException(); }
    }
}
