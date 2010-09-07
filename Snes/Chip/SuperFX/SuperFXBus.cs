using System;
using Snes.Memory;

namespace Snes.Chip.SuperFX
{
    class SuperFXBus : Bus
    {
        public static SuperFXBus superfxbus = new SuperFXBus();

        public void init() { throw new NotImplementedException(); }
    }
}
