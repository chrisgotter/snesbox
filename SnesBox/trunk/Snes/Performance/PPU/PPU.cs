#if PERFORMANCE
using System;
using Nall;

namespace Snes
{
    partial class PPU : IProcessor, IPPUCounter, IMMIO
    {
        public static PPU ppu = new PPU();

        public void step(uint clocks) { throw new NotImplementedException(); }
        public void synchronize_cpu() { throw new NotImplementedException(); }

        public void latch_counters() { throw new NotImplementedException(); }
        public bool interlace() { throw new NotImplementedException(); }
        public bool overscan() { throw new NotImplementedException(); }
        public bool hires() { throw new NotImplementedException(); }

        public void enter() { throw new NotImplementedException(); }
        public void power() { throw new NotImplementedException(); }
        public void reset() { throw new NotImplementedException(); }
        public void scanline() { throw new NotImplementedException(); }
        public void frame() { throw new NotImplementedException(); }

        public void layer_enable(uint layer, uint priority, bool enable) { throw new NotImplementedException(); }
        public void set_frameskip(uint frameskip) { throw new NotImplementedException(); }

        public void serialize(Serializer s) { throw new NotImplementedException(); }
        public PPU() { throw new NotImplementedException(); }

        private ushort[] surface;
        public ArraySegment<ushort> output;

        private Regs regs;

        private ushort get_vram_addr() { throw new NotImplementedException(); }
        private byte vram_read(uint addr) { throw new NotImplementedException(); }
        private void vram_write(uint addr, byte data) { throw new NotImplementedException(); }

        private byte oam_read(uint addr) { throw new NotImplementedException(); }
        private void oam_write(uint addr, byte data) { throw new NotImplementedException(); }

        private byte cgram_read(uint addr) { throw new NotImplementedException(); }
        private void cgram_write(uint addr, byte data) { throw new NotImplementedException(); }

        private void mmio_update_video_mode() { throw new NotImplementedException(); }
        public byte mmio_read(uint addr) { throw new NotImplementedException(); }
        public void mmio_write(uint addr, byte data) { throw new NotImplementedException(); }
        private void mmio_reset() { throw new NotImplementedException(); }

        private Cache cache;
        private Background bg1;
        private Background bg2;
        private Background bg3;
        private Background bg4;
        private Sprite oam;
        private Screen screen;

        private Display display;

        private static void Enter() { throw new NotImplementedException(); }
        private void add_clocks(uint clocks) { throw new NotImplementedException(); }
        private void render_scanline() { throw new NotImplementedException(); }

        public Processor Processor
        {
            get { throw new NotImplementedException(); }
        }

        public PPUCounter PPUCounter
        {
            get { throw new NotImplementedException(); }
        }
    }
}
#endif