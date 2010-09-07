using System;

namespace Snes.Chip.SuperFX
{
    class SuperFXGSUROM : Snes.Memory.Memory
    {
        public static SuperFXGSUROM gsurom = new SuperFXGSUROM();

        public override uint size()
        {
            throw new NotImplementedException();
        }

        public override byte read(uint addr)
        {
            throw new NotImplementedException();
        }

        public override void write(uint addr, byte data)
        {
            throw new NotImplementedException();
        }
    }
}
