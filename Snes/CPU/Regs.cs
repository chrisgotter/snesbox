using System;

namespace Snes.CPU
{
    partial class CPUCore
    {
        public class Regs
        {
            public Reg24 pc;
            public Reg16[] r = new Reg16[6];
            public Reg16 a, x, y, z, s, d;
            public Flag p;
            public byte db;
            public bool e;

            public bool irq; //IRQ pin (0 = low, 1 = trigger)
            public bool wai; //raised during wai, cleared after interrupt triggered
            public byte mdr; //memory data register

            public Regs() { throw new NotImplementedException(); }
        }
    }
}
