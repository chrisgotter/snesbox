using System;

namespace Snes.Chip
{
    class Coprocessor : Processor
    {
        public void step(uint clocks) { throw new NotImplementedException(); }
        public void synchronize_cpu() { throw new NotImplementedException(); }
    }
}
