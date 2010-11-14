#if PERFORMANCE
using System;
using Nall;

namespace Snes
{
    partial class PPU
    {
        private partial class Background
        {
            public enum ID { BG1, BG2, BG3, BG4 }
            public enum Mode { BPP2, BPP4, BPP8, Mode7, Inactive }
            public enum ScreenSize { Size32x32, Size32x64, Size64x32, Size64x64 }
            public enum TileSize { Size8x8, Size16x16 }

            public bool priority0_enable;
            public bool priority1_enable;

            public Regs regs;

            public ushort[][] mosaic_table;

            public uint id;
            public uint opt_valid_bit;

            public bool hires;
            public int width;

            public uint tile_width;
            public uint tile_height;

            public uint mask_x;
            public uint mask_y;

            public uint scx;
            public uint scy;

            public uint hscroll;
            public uint vscroll;

            public uint mosaic_vcounter;
            public uint mosaic_voffset;

            public LayerWindow window;

            public uint get_tile(uint hoffset, uint voffset) { throw new NotImplementedException(); }
            public void offset_per_tile(uint x, uint y, ref uint hoffset, ref uint voffset) { throw new NotImplementedException(); }
            public void scanline() { throw new NotImplementedException(); }
            public void render() { throw new NotImplementedException(); }
            public void render_mode7() { throw new NotImplementedException(); }

            public void serialize(Serializer s) { throw new NotImplementedException(); }
            public Background(PPU self, uint id) { throw new NotImplementedException(); }

            public PPU self;
        }
    }
}
#endif