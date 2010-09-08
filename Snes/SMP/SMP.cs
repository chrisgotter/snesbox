using System;
using Nall;

namespace Snes.SMP
{
    partial class SMP : SMPCore, IProcessor
    {
        public static SMP smp = new SMP();

        public static readonly bool Threaded = true;
        public void step(uint clocks) { throw new NotImplementedException(); }
        public void synchronize_cpu() { throw new NotImplementedException(); }
        public void synchronize_dsp() { throw new NotImplementedException(); }

        public byte port_read(uint2 port) { throw new NotImplementedException(); }
        public void port_write(uint2 port, byte data) { throw new NotImplementedException(); }

        public void enter() { throw new NotImplementedException(); }
        public void power() { throw new NotImplementedException(); }
        public void reset() { throw new NotImplementedException(); }

        public SMP() { throw new NotImplementedException(); }

        private byte ram_read(ushort addr) { throw new NotImplementedException(); }
        private void ram_write(ushort addr, byte data) { throw new NotImplementedException(); }

        private byte op_busread(ushort addr) { throw new NotImplementedException(); }
        private void op_buswrite(ushort addr, byte data) { throw new NotImplementedException(); }

        public override void op_io() { throw new NotImplementedException(); }
        public override byte op_read(ushort addr) { throw new NotImplementedException(); }
        public override void op_write(ushort addr, byte data) { throw new NotImplementedException(); }

        private sSMPTimer t0 = new sSMPTimer(192);
        private sSMPTimer t1 = new sSMPTimer(192);
        private sSMPTimer t2 = new sSMPTimer(24);

        private void add_clocks(uint clocks) { throw new NotImplementedException(); }
        private void cycle_edge() { throw new NotImplementedException(); }

        private static readonly byte[] iplrom = new byte[64];

        private Status status;

        private static void Enter() { throw new NotImplementedException(); }
        private void op_step() { throw new NotImplementedException(); }

        public Processor Processor
        {
            get { throw new NotImplementedException(); }
        }
    }
}
