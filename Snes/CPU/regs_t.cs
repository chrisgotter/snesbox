using System;

namespace Snes.CPU
{
    partial class CPUCore
    {
        public class regs_t
        {
            public reg24_t pc;
            public reg16_t[] r = new reg16_t[6];
            public reg16_t a, x, y, z, s, d;
            public flag_t p;
            public byte db;
            public bool e;

            public bool irq; //IRQ pin (0 = low, 1 = trigger)
            public bool wai; //raised during wai, cleared after interrupt triggered
            public byte mdr; //memory data register

            public regs_t() { throw new NotImplementedException(); }
        }
    }
}
