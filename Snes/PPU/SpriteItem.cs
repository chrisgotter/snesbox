using System;

namespace Snes.PPU
{
    partial class PPU
    {
        private partial class Sprite
        {
            public class SpriteItem
            {
                ushort x;
                ushort y;
                byte character;
                bool nameselect;
                bool vflip;
                bool hflip;
                byte priority;
                byte palette;
                bool size;
                uint width() { throw new NotImplementedException(); }
                uint height() { throw new NotImplementedException(); }
            }
        }
    }
}
