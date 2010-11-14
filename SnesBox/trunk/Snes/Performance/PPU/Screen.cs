#if PERFORMANCE
using System;
using Nall;

namespace Snes
{
    partial class PPU
    {
        private partial class Screen
        {
            public Regs regs;
            public Output output;

            public ColorWindow window;
            public ushort[][] light_table;

            public uint get_palette(uint color) { throw new NotImplementedException(); }
            public uint get_direct_color(uint palette, uint tile) { throw new NotImplementedException(); }
            public ushort addsub(uint x, uint y, bool halve) { throw new NotImplementedException(); }
            public void scanline() { throw new NotImplementedException(); }
            public void render_black() { throw new NotImplementedException(); }
            public ushort get_pixel_main(uint x) { throw new NotImplementedException(); }
            public ushort get_pixel_sub(uint x) { throw new NotImplementedException(); }
            public void render() { throw new NotImplementedException(); }

            public void serialize(Serializer s) { throw new NotImplementedException(); }
            public Screen(PPU self) { throw new NotImplementedException(); }

            public PPU self;
        }
    }
}
#endif