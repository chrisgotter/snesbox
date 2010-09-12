using System;

namespace Snes
{
    partial class PPU : PPUCounter, IProcessor, IMMIO
    {
        public static PPU ppu = new PPU();

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

        private Regs regs;
        private ushort get_vram_address() { throw new NotImplementedException(); }

        private byte vram_read(uint addr) { throw new NotImplementedException(); }
        private void vram_write(uint addr, byte data) { throw new NotImplementedException(); }

        private byte oam_read(uint addr) { throw new NotImplementedException(); }
        private void oam_write(uint addr, byte data) { throw new NotImplementedException(); }

        private byte cgram_read(uint addr) { throw new NotImplementedException(); }
        private void cgram_write(uint addr, byte data) { throw new NotImplementedException(); }

        private void mmio_update_video_mode() { throw new NotImplementedException(); }

        private void mmio_w2100(byte data) { throw new NotImplementedException(); }  //INIDISP
        private void mmio_w2101(byte data) { throw new NotImplementedException(); }  //OBSEL
        private void mmio_w2102(byte data) { throw new NotImplementedException(); }  //OAMADDL
        private void mmio_w2103(byte data) { throw new NotImplementedException(); }  //OAMADDH
        private void mmio_w2104(byte data) { throw new NotImplementedException(); }  //OAMDATA
        private void mmio_w2105(byte data) { throw new NotImplementedException(); }  //BGMODE
        private void mmio_w2106(byte data) { throw new NotImplementedException(); }  //MOSAIC
        private void mmio_w2107(byte data) { throw new NotImplementedException(); }  //BG1SC
        private void mmio_w2108(byte data) { throw new NotImplementedException(); }  //BG2SC
        private void mmio_w2109(byte data) { throw new NotImplementedException(); }  //BG3SC
        private void mmio_w210a(byte data) { throw new NotImplementedException(); }  //BG4SC
        private void mmio_w210b(byte data) { throw new NotImplementedException(); }  //BG12NBA
        private void mmio_w210c(byte data) { throw new NotImplementedException(); }  //BG34NBA
        private void mmio_w210d(byte data) { throw new NotImplementedException(); }  //BG1HOFS
        private void mmio_w210e(byte data) { throw new NotImplementedException(); }  //BG1VOFS
        private void mmio_w210f(byte data) { throw new NotImplementedException(); }  //BG2HOFS
        private void mmio_w2110(byte data) { throw new NotImplementedException(); }  //BG2VOFS
        private void mmio_w2111(byte data) { throw new NotImplementedException(); }  //BG3HOFS
        private void mmio_w2112(byte data) { throw new NotImplementedException(); }  //BG3VOFS
        private void mmio_w2113(byte data) { throw new NotImplementedException(); }  //BG4HOFS
        private void mmio_w2114(byte data) { throw new NotImplementedException(); }  //BG4VOFS
        private void mmio_w2115(byte data) { throw new NotImplementedException(); }  //VMAIN
        private void mmio_w2116(byte data) { throw new NotImplementedException(); }  //VMADDL
        private void mmio_w2117(byte data) { throw new NotImplementedException(); }  //VMADDH
        private void mmio_w2118(byte data) { throw new NotImplementedException(); }  //VMDATAL
        private void mmio_w2119(byte data) { throw new NotImplementedException(); }  //VMDATAH
        private void mmio_w211a(byte data) { throw new NotImplementedException(); }  //M7SEL
        private void mmio_w211b(byte data) { throw new NotImplementedException(); }  //M7A
        private void mmio_w211c(byte data) { throw new NotImplementedException(); }  //M7B
        private void mmio_w211d(byte data) { throw new NotImplementedException(); }  //M7C
        private void mmio_w211e(byte data) { throw new NotImplementedException(); }  //M7D
        private void mmio_w211f(byte data) { throw new NotImplementedException(); }  //M7X
        private void mmio_w2120(byte data) { throw new NotImplementedException(); }  //M7Y
        private void mmio_w2121(byte data) { throw new NotImplementedException(); }  //CGADD
        private void mmio_w2122(byte data) { throw new NotImplementedException(); }  //CGDATA
        private void mmio_w2123(byte data) { throw new NotImplementedException(); }  //W12SEL
        private void mmio_w2124(byte data) { throw new NotImplementedException(); }  //W34SEL
        private void mmio_w2125(byte data) { throw new NotImplementedException(); }  //WOBJSEL
        private void mmio_w2126(byte data) { throw new NotImplementedException(); }  //WH0
        private void mmio_w2127(byte data) { throw new NotImplementedException(); }  //WH1
        private void mmio_w2128(byte data) { throw new NotImplementedException(); }  //WH2
        private void mmio_w2129(byte data) { throw new NotImplementedException(); }  //WH3
        private void mmio_w212a(byte data) { throw new NotImplementedException(); }  //WBGLOG
        private void mmio_w212b(byte data) { throw new NotImplementedException(); }  //WOBJLOG
        private void mmio_w212c(byte data) { throw new NotImplementedException(); }  //TM
        private void mmio_w212d(byte data) { throw new NotImplementedException(); }  //TS
        private void mmio_w212e(byte data) { throw new NotImplementedException(); }  //TMW
        private void mmio_w212f(byte data) { throw new NotImplementedException(); }  //TSW
        private void mmio_w2130(byte data) { throw new NotImplementedException(); }  //CGWSEL
        private void mmio_w2131(byte data) { throw new NotImplementedException(); }  //CGADDSUB
        private void mmio_w2132(byte data) { throw new NotImplementedException(); }  //COLDATA
        private void mmio_w2133(byte data) { throw new NotImplementedException(); }  //SETINI
        private byte mmio_r2134() { throw new NotImplementedException(); }  //MPYL
        private byte mmio_r2135() { throw new NotImplementedException(); }  //MPYM
        private byte mmio_r2136() { throw new NotImplementedException(); }  //MPYH
        private byte mmio_r2137() { throw new NotImplementedException(); }  //SLHV
        private byte mmio_r2138() { throw new NotImplementedException(); }  //OAMDATAREAD
        private byte mmio_r2139() { throw new NotImplementedException(); }  //VMDATALREAD
        private byte mmio_r213a() { throw new NotImplementedException(); }  //VMDATAHREAD
        private byte mmio_r213b() { throw new NotImplementedException(); }  //CGDATAREAD
        private byte mmio_r213c() { throw new NotImplementedException(); }  //OPHCT
        private byte mmio_r213d() { throw new NotImplementedException(); }  //OPVCT
        private byte mmio_r213e() { throw new NotImplementedException(); }  //STAT77
        private byte mmio_r213f() { throw new NotImplementedException(); }  //STAT78

        private void mmio_reset() { throw new NotImplementedException(); }
        public byte mmio_read(uint addr) { throw new NotImplementedException(); }
        public void mmio_write(uint addr, byte data) { throw new NotImplementedException(); }

        private Background bg1;
        private Background bg2;
        private Background bg3;
        private Background bg4;
        private Sprite oam;
        private Window window;
        private Screen screen;

        private ushort[] surface;
        private ushort[] output;

        public ushort[] Output
        {
            get { return output; }
        }

        private byte ppu1_version;
        private byte ppu2_version;

        private Display display;

        private static void Enter() { throw new NotImplementedException(); }
        private void add_clocks(uint clocks) { throw new NotImplementedException(); }

        private void scanline() { throw new NotImplementedException(); }
        private void frame() { throw new NotImplementedException(); }

        public Processor Processor
        {
            get { throw new NotImplementedException(); }
        }
    }
}
