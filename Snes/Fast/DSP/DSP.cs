using System;
using Nall;

namespace Snes.Fast
{
    class DSP : IProcessor
    {
        public void step(uint clocks) { throw new NotImplementedException(); }
        public void synchronize_smp() { throw new NotImplementedException(); }

        public byte read(byte addr) { throw new NotImplementedException(); }
        public void write(byte addr, byte data) { throw new NotImplementedException(); }

        public void enter() { throw new NotImplementedException(); }
        public void power() { throw new NotImplementedException(); }
        public void reset() { throw new NotImplementedException(); }

        public void serialize(Serializer s) { throw new NotImplementedException(); }
        public bool property(uint id, string name, string value) { return false; }

        private SPCDSP spc_dsp;
        private short[] samplebuffer = new short[8192];

        private Processor _processor = new Processor();
        public Processor Processor
        {
            get
            {
                return _processor;
            }
        }
    }
}
