using System;

namespace Snes.Chip.SuperFX
{
    class SuperFXCPURAM : Snes.Memory.Memory
    {
        public static SuperFXCPURAM fxram = new SuperFXCPURAM();

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
