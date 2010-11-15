#if PERFORMANCE
using System;
using Nall;

namespace Snes
{
    partial class PPU
    {
        private class Cache
        {
            public byte[] tiledata = new byte[3];
            public byte[][] tilevalid = new byte[3][];

            public byte[] tile_2bpp(uint tile) { throw new NotImplementedException(); }
            public byte[] tile_4bpp(uint tile) { throw new NotImplementedException(); }
            public byte[] tile_8bpp(uint tile) { throw new NotImplementedException(); }
            public byte[] tile(uint bpp, uint tile) { throw new NotImplementedException(); }

            public void serialize(Serializer s) { throw new NotImplementedException(); }
            public Cache(PPU self) { throw new NotImplementedException(); }

            public PPU self;
        }
    }
}
#endif