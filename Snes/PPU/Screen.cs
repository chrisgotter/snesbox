using System;

namespace Snes
{
    partial class PPU
    {
        private partial class Screen
        {
            public PPU self;
            public ushort[] output;

            public Regs regs;

            public void scanline() { throw new NotImplementedException(); }
            public void run() { throw new NotImplementedException(); }
            public void reset() { throw new NotImplementedException(); }

            public Screen(PPU self) { throw new NotImplementedException(); }

            private ushort[,] light_table = new ushort[16, 32768];
            private ushort get_pixel(bool swap) { throw new NotImplementedException(); }
            private ushort addsub(uint x, uint y, bool halve) { throw new NotImplementedException(); }
            private ushort get_color(uint palette) { throw new NotImplementedException(); }
            private ushort get_direct_color(uint palette, uint tile) { throw new NotImplementedException(); }
        }
    }
}
