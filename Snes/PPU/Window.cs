using System;

namespace Snes
{
    partial class PPU
    {
        private partial class Window
        {
            public PPU self;
            public T t;
            public Regs regs;
            public Output output;

            public void scanline() { throw new NotImplementedException(); }
            public void run() { throw new NotImplementedException(); }
            public void reset() { throw new NotImplementedException(); }

            public Window(PPU self) { throw new NotImplementedException(); }

            private void test(ref bool main, ref bool sub, bool one_enable, bool one_invert, bool two_enable, bool two_invert, byte mask, bool main_enable, bool sub_enable) { throw new NotImplementedException(); }
        }
    }
}