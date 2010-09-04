using System;

namespace Snes.Memory
{
    class MMIOAccess : Memory
    {
        IMMIO handle(uint addr) { throw new NotImplementedException(); }
        public override uint size() { throw new NotImplementedException(); }
        public void map(uint addr, IMMIO access) { throw new NotImplementedException(); }
        public override byte read(uint addr) { throw new NotImplementedException(); }
        public override void write(uint addr, byte data) { throw new NotImplementedException(); }
        MMIOAccess() { throw new NotImplementedException(); }

        public IMMIO[] mmio = new IMMIO[0x8000];
    }
}
