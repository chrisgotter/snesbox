using System;

namespace Snes.PPU
{
    partial class PPU
    {
        private partial class Sprite
        {
            public PPU self;
            public SpriteItem[] list = new SpriteItem[128];
            public State t;
            public Regs regs;
            public Output output;

            //list.cpp
            public void update(uint addr, byte data) { throw new NotImplementedException(); }

            //sprite.cpp
            public void address_reset() { throw new NotImplementedException(); }
            public void frame() { throw new NotImplementedException(); }
            public void scanline() { throw new NotImplementedException(); }
            public void run() { throw new NotImplementedException(); }
            public void tilefetch() { throw new NotImplementedException(); }
            public void reset() { throw new NotImplementedException(); }

            public Sprite(PPU self) { throw new NotImplementedException(); }

            public bool on_scanline(SpriteItem sprite) { throw new NotImplementedException(); }
        }
    }
}
