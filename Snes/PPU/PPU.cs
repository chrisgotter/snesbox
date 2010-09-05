using System;
using Snes.Memory;

namespace Snes.PPU
{
    abstract partial class PPU : PPUCounter, IProcessor, IMMIO
    {
        public static readonly bool Threaded = true;
        public void step(uint clocks) { throw new NotImplementedException(); }
        public void synchronize_cpu() { throw new NotImplementedException(); }

        public void latch_counters() { throw new NotImplementedException(); }
        public bool interlace() { throw new NotImplementedException(); }
        public bool overscan() { throw new NotImplementedException(); }
        public bool hires() { throw new NotImplementedException(); }

        public void enter() { throw new NotImplementedException(); }
        public void power() { throw new NotImplementedException(); }
        public void reset() { throw new NotImplementedException(); }

        public PPU() { throw new NotImplementedException(); }

        private Background bg1;
        private Background bg2;
        private Background bg3;
        private Background bg4;
        private Sprite oam;
        private Window window;
        private Screen screen;

        private ushort[] surface;
        private ushort[] output;

        private byte ppu1_version;
        private byte ppu2_version;

        private Display display;

        private static void Enter() { throw new NotImplementedException(); }
        private void add_clocks(uint clocks) { throw new NotImplementedException(); }

        private void scanline() { throw new NotImplementedException(); }
        private void frame() { throw new NotImplementedException(); }

        public byte mmio_read(uint addr) { throw new NotImplementedException(); }
        public void mmio_write(uint addr, byte data) { throw new NotImplementedException(); }

        public Processor Processor
        {
            get { throw new NotImplementedException(); }
        }
    }
}
