using System;

namespace Snes
{
    class Coprocessor : Processor, ICoprocessor
    {
        public void step(uint clocks) { throw new NotImplementedException(); }
        public void synchronize_cpu() { throw new NotImplementedException(); }

        Coprocessor ICoprocessor.Coprocessor
        {
            get
            {
                return this;
            }
        }
    }
}
