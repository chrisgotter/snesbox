using System;

namespace Snes.Chip.SuperFX
{
    class SuperFXCPUROM : Snes.Memory.Memory
    {
        public static SuperFXCPUROM fxrom = new SuperFXCPUROM();

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
