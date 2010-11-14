#if PERFORMANCE
using System;
using Nall;

namespace Snes
{
    partial class PPU
    {
        private partial class Sprite
        {
            public bool priority0_enable;
            public bool priority1_enable;
            public bool priority2_enable;
            public bool priority3_enable;

            public Regs regs;

            public List[] list = new List[128];
            public bool list_valid;

            public byte[] itemlist = new byte[32];
            public TileList[] tilelist = new TileList[34];

            public Output output;

            public LayerWindow window;

            public void frame() { throw new NotImplementedException(); }
            public void update_list(uint addr, byte data) { throw new NotImplementedException(); }
            public void address_reset() { throw new NotImplementedException(); }
            public void set_first() { throw new NotImplementedException(); }
            public bool on_scanline(uint sprite) { throw new NotImplementedException(); }
            public void render() { throw new NotImplementedException(); }

            public void serialize(Serializer s) { throw new NotImplementedException(); }
            public Sprite(PPU self) { throw new NotImplementedException(); }

            public PPU self;
        }
    }
}
#endif