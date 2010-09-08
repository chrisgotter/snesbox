using System;

namespace Snes
{
    partial class PPU
    {
        private partial class Background
        {
            public PPU self;
            public enum ID { BG1, BG2, BG3, BG4 }
            public uint id;

            public enum Mode { BPP2, BPP4, BPP8, Mode7, Inactive }
            public enum ScreenSize { Size32x32, Size32x64, Size64x32, Size64x64 }
            public enum TileSize { Size8x8, Size16x16 }

            public T t;
            public Regs regs;
            public Output output;

            public void scanline() { throw new NotImplementedException(); }
            public void run() { throw new NotImplementedException(); }
            public uint get_tile(uint x, uint y) { throw new NotImplementedException(); }
            public uint get_color(uint x, uint y, ushort offset) { throw new NotImplementedException(); }
            public void reset() { throw new NotImplementedException(); }

            public Background(PPU self, uint id) { throw new NotImplementedException(); }

            private static ushort[,] mosaic_table = new ushort[16, 4096];

            //mode7.cpp
            private int clip(int n) { throw new NotImplementedException(); }
            private void run_mode7(uint x, uint y) { throw new NotImplementedException(); }
        }
    }
}
