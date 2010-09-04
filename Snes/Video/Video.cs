using System;

namespace Snes
{
    class Video
    {
        private bool frame_hires;
        private bool frame_interlace;
        private uint[] line_width = new uint[240];

        private void update() { throw new NotImplementedException(); }
        private void scanline() { throw new NotImplementedException(); }
        private void init() { throw new NotImplementedException(); }

        private static byte[] cursor = new byte[15 * 15];
        private void draw_cursor(ushort color, int x, int y) { throw new NotImplementedException(); }
    }
}
