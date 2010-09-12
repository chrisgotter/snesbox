using System;

namespace Snes
{
    class Video
    {
        public static Video video = new Video();

        private bool frame_hires;
        private bool frame_interlace;
        private uint[] line_width = new uint[240];

        public void update()
        {
            switch (Input.input.Ports[1].device)
            {
                case Input.Device.SuperScope:
                    draw_cursor(0x001f, Input.input.Ports[1].superscope.x, Input.input.Ports[1].superscope.y);
                    break;
                case Input.Device.Justifiers:
                    draw_cursor(0x02e0, Input.input.Ports[1].justifier.x2, Input.input.Ports[1].justifier.y2);
                    goto case Input.Device.Justifier;  //fallthrough
                case Input.Device.Justifier:
                    draw_cursor(0x001f, Input.input.Ports[1].justifier.x1, Input.input.Ports[1].justifier.y1);
                    break;
            }

            ushort[] data = PPU.ppu.Output;
            // TODO: verify array segments
            var dataSeg = new ArraySegment<ushort>(data);
            if (PPU.ppu.interlace() && PPU.ppu.field())
            {
                // TODO: verify array segments
                dataSeg = new ArraySegment<ushort>(data, 512, data.Length - 512);
            }
            uint width = 256;
            uint height = !PPU.ppu.overscan() ? 224U : 239U;

            if (frame_hires)
            {
                width <<= 1;
                //normalize line widths
                for (uint y = 0; y < 240; y++)
                {
                    if (line_width[y] == 512)
                    {
                        continue;
                    }
                    // TODO: verify array segments
                    ushort[] buffer = new ArraySegment<ushort>(dataSeg.Array, (int)(y * 1024), (int)(dataSeg.Array.Length - (y * 1024))).Array;
                    for (int x = 255; x >= 0; x--)
                    {
                        buffer[(x * 2) + 0] = buffer[(x * 2) + 1] = buffer[x];
                    }
                }
            }

            if (frame_interlace)
            {
                height <<= 1;
            }

            // TODO: verify array segments
            System.system.Interface.video_refresh(new ArraySegment<ushort>(PPU.ppu.Output, 1024, PPU.ppu.Output.Length - 1024).Array, width, height);

            frame_hires = false;
            frame_interlace = false;
        }

        public void scanline()
        {
            uint y = CPU.cpu.PPUCounter.vcounter();
            if (y >= 240)
            {
                return;
            }

            frame_hires |= PPU.ppu.hires();
            frame_interlace |= PPU.ppu.interlace();
            uint width = (PPU.ppu.hires() == false ? 256U : 512U);
            line_width[y] = width;
        }

        public void init()
        {
            frame_hires = false;
            frame_interlace = false;
            for (uint i = 0; i < 240; i++)
            {
                line_width[i] = 256;
            }
        }

        private static readonly byte[] cursor = new byte[15 * 15]
        {
            0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,
            0,0,0,0,1,1,2,2,2,1,1,0,0,0,0,
            0,0,0,1,2,2,1,2,1,2,2,1,0,0,0,
            0,0,1,2,1,1,0,1,0,1,1,2,1,0,0,
            0,1,2,1,0,0,0,1,0,0,0,1,2,1,0,
            0,1,2,1,0,0,1,2,1,0,0,1,2,1,0,
            1,2,1,0,0,1,1,2,1,1,0,0,1,2,1,
            1,2,2,1,1,2,2,2,2,2,1,1,2,2,1,
            1,2,1,0,0,1,1,2,1,1,0,0,1,2,1,
            0,1,2,1,0,0,1,2,1,0,0,1,2,1,0,
            0,1,2,1,0,0,0,1,0,0,0,1,2,1,0,
            0,0,1,2,1,1,0,1,0,1,1,2,1,0,0,
            0,0,0,1,2,2,1,2,1,2,2,1,0,0,0,
            0,0,0,0,1,1,2,2,2,1,1,0,0,0,0,
            0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,
        };

        private void draw_cursor(ushort color, int x, int y)
        {
            ushort[] data = PPU.ppu.Output;
            // TODO: verify array segments
            var dataSeg = new ArraySegment<ushort>(data);
            if (PPU.ppu.interlace() && PPU.ppu.field())
            {
                // TODO: verify array segments
                dataSeg = new ArraySegment<ushort>(data, 512, data.Length - 512);
            }

            for (int cy = 0; cy < 15; cy++)
            {
                int vy = y + cy - 7;
                if (vy <= 0 || vy >= 240)
                {
                    continue;  //do not draw offscreen
                }

                bool hires = (line_width[vy] == 512);
                for (int cx = 0; cx < 15; cx++)
                {
                    int vx = x + cx - 7;
                    if (vx < 0 || vx >= 256)
                    {
                        continue;  //do not draw offscreen
                    }
                    byte pixel = cursor[cy * 15 + cx];
                    if (pixel == 0)
                    {
                        continue;
                    }
                    ushort pixelcolor = (pixel == 1) ? (ushort)0 : color;

                    if (hires == false)
                    {
                        dataSeg.Array[vy * 1024 + vx] = pixelcolor;
                    }
                    else
                    {
                        dataSeg.Array[vy * 1024 + vx * 2 + 0] = pixelcolor;
                        dataSeg.Array[vy * 1024 + vx * 2 + 1] = pixelcolor;
                    }
                }
            }
        }
    }
}
