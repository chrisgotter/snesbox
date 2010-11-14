#if PERFORMANCE
using System;

namespace Snes
{
    partial class PPU
    {
        private partial class Sprite
        {
            public class Output
            {
                public byte[] palette = new byte[256];
                public byte[] priority = new byte[256];
            }
        }
    }

    partial class PPU
    {
        private partial class Screen
        {
            public class Output
            {
                public class Pixel
                {
                    public uint color;
                    public uint priority;
                    public uint source;
                }
                public Pixel[] main = new Pixel[256];
                public Pixel[] sub = new Pixel[256];

                public void plot_main(uint x, uint color, uint priority, uint source) { throw new NotImplementedException(); }
                public void plot_sub(uint x, uint color, uint priority, uint source) { throw new NotImplementedException(); }
            }
        }
    }
}
#endif