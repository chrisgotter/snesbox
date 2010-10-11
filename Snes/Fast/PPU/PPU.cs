using System;
using Nall;

namespace Snes.Fast
{
    partial class PPU : IPPUCounter, IProcessor, IMMIO
    {
        public static PPU ppu = new PPU();

        public void step(uint clocks) { throw new NotImplementedException(); }
        public void synchronize_cpu() { throw new NotImplementedException(); }

        public ushort get_vram_address() { throw new NotImplementedException(); }

        public byte vram_mmio_read(ushort addr) { throw new NotImplementedException(); }
        public void vram_mmio_write(ushort addr, byte data) { throw new NotImplementedException(); }

        public byte oam_mmio_read(ushort addr) { throw new NotImplementedException(); }
        public void oam_mmio_write(ushort addr, byte data) { throw new NotImplementedException(); }

        public byte cgram_mmio_read(ushort addr) { throw new NotImplementedException(); }
        public void cgram_mmio_write(ushort addr, byte data) { throw new NotImplementedException(); }

        public Regs regs = new Regs();

        public void mmio_w2100(byte value) { throw new NotImplementedException(); }  //INIDISP
        public void mmio_w2101(byte value) { throw new NotImplementedException(); }  //OBSEL
        public void mmio_w2102(byte value) { throw new NotImplementedException(); }  //OAMADDL
        public void mmio_w2103(byte value) { throw new NotImplementedException(); }  //OAMADDH
        public void mmio_w2104(byte value) { throw new NotImplementedException(); }  //OAMDATA
        public void mmio_w2105(byte value) { throw new NotImplementedException(); }  //BGMODE
        public void mmio_w2106(byte value) { throw new NotImplementedException(); }  //MOSAIC
        public void mmio_w2107(byte value) { throw new NotImplementedException(); }  //BG1SC
        public void mmio_w2108(byte value) { throw new NotImplementedException(); }  //BG2SC
        public void mmio_w2109(byte value) { throw new NotImplementedException(); }  //BG3SC
        public void mmio_w210a(byte value) { throw new NotImplementedException(); }  //BG4SC
        public void mmio_w210b(byte value) { throw new NotImplementedException(); }  //BG12NBA
        public void mmio_w210c(byte value) { throw new NotImplementedException(); }  //BG34NBA
        public void mmio_w210d(byte value) { throw new NotImplementedException(); }  //BG1HOFS
        public void mmio_w210e(byte value) { throw new NotImplementedException(); }  //BG1VOFS
        public void mmio_w210f(byte value) { throw new NotImplementedException(); }  //BG2HOFS
        public void mmio_w2110(byte value) { throw new NotImplementedException(); }  //BG2VOFS
        public void mmio_w2111(byte value) { throw new NotImplementedException(); }  //BG3HOFS
        public void mmio_w2112(byte value) { throw new NotImplementedException(); }  //BG3VOFS
        public void mmio_w2113(byte value) { throw new NotImplementedException(); }  //BG4HOFS
        public void mmio_w2114(byte value) { throw new NotImplementedException(); }  //BG4VOFS
        public void mmio_w2115(byte value) { throw new NotImplementedException(); }  //VMAIN
        public void mmio_w2116(byte value) { throw new NotImplementedException(); }  //VMADDL
        public void mmio_w2117(byte value) { throw new NotImplementedException(); }  //VMADDH
        public void mmio_w2118(byte value) { throw new NotImplementedException(); }  //VMDATAL
        public void mmio_w2119(byte value) { throw new NotImplementedException(); }  //VMDATAH
        public void mmio_w211a(byte value) { throw new NotImplementedException(); }  //M7SEL
        public void mmio_w211b(byte value) { throw new NotImplementedException(); }  //M7A
        public void mmio_w211c(byte value) { throw new NotImplementedException(); }  //M7B
        public void mmio_w211d(byte value) { throw new NotImplementedException(); }  //M7C
        public void mmio_w211e(byte value) { throw new NotImplementedException(); }  //M7D
        public void mmio_w211f(byte value) { throw new NotImplementedException(); }  //M7X
        public void mmio_w2120(byte value) { throw new NotImplementedException(); }  //M7Y
        public void mmio_w2121(byte value) { throw new NotImplementedException(); }  //CGADD
        public void mmio_w2122(byte value) { throw new NotImplementedException(); }  //CGDATA
        public void mmio_w2123(byte value) { throw new NotImplementedException(); }  //W12SEL
        public void mmio_w2124(byte value) { throw new NotImplementedException(); }  //W34SEL
        public void mmio_w2125(byte value) { throw new NotImplementedException(); }  //WOBJSEL
        public void mmio_w2126(byte value) { throw new NotImplementedException(); }  //WH0
        public void mmio_w2127(byte value) { throw new NotImplementedException(); }  //WH1
        public void mmio_w2128(byte value) { throw new NotImplementedException(); }  //WH2
        public void mmio_w2129(byte value) { throw new NotImplementedException(); }  //WH3
        public void mmio_w212a(byte value) { throw new NotImplementedException(); }  //WBGLOG
        public void mmio_w212b(byte value) { throw new NotImplementedException(); }  //WOBJLOG
        public void mmio_w212c(byte value) { throw new NotImplementedException(); }  //TM
        public void mmio_w212d(byte value) { throw new NotImplementedException(); }  //TS
        public void mmio_w212e(byte value) { throw new NotImplementedException(); }  //TMW
        public void mmio_w212f(byte value) { throw new NotImplementedException(); }  //TSW
        public void mmio_w2130(byte value) { throw new NotImplementedException(); }  //CGWSEL
        public void mmio_w2131(byte value) { throw new NotImplementedException(); }  //CGADDSUB
        public void mmio_w2132(byte value) { throw new NotImplementedException(); }  //COLDATA
        public void mmio_w2133(byte value) { throw new NotImplementedException(); }  //SETINI

        public byte mmio_r2134() { throw new NotImplementedException(); }  //MPYL
        public byte mmio_r2135() { throw new NotImplementedException(); }  //MPYM
        public byte mmio_r2136() { throw new NotImplementedException(); }  //MPYH
        public byte mmio_r2137() { throw new NotImplementedException(); }  //SLHV
        public byte mmio_r2138() { throw new NotImplementedException(); }  //OAMDATAREAD
        public byte mmio_r2139() { throw new NotImplementedException(); }  //VMDATALREAD
        public byte mmio_r213a() { throw new NotImplementedException(); }  //VMDATAHREAD
        public byte mmio_r213b() { throw new NotImplementedException(); }  //CGDATAREAD
        public byte mmio_r213c() { throw new NotImplementedException(); }  //OPHCT
        public byte mmio_r213d() { throw new NotImplementedException(); }  //OPVCT
        public byte mmio_r213e() { throw new NotImplementedException(); }  //STAT77
        public byte mmio_r213f() { throw new NotImplementedException(); }  //STAT78

        public byte mmio_read(uint addr) { throw new NotImplementedException(); }
        public void mmio_write(uint addr, byte data) { throw new NotImplementedException(); }

        public void latch_counters() { throw new NotImplementedException(); }

        //render.cpp
        public void render_line_mode0() { throw new NotImplementedException(); }
        public void render_line_mode1() { throw new NotImplementedException(); }
        public void render_line_mode2() { throw new NotImplementedException(); }
        public void render_line_mode3() { throw new NotImplementedException(); }
        public void render_line_mode4() { throw new NotImplementedException(); }
        public void render_line_mode5() { throw new NotImplementedException(); }
        public void render_line_mode6() { throw new NotImplementedException(); }
        public void render_line_mode7() { throw new NotImplementedException(); }

        //cache.cpp
        public enum COLORDEPTH { COLORDEPTH_4 = 0, COLORDEPTH_16 = 1, COLORDEPTH_256 = 2 };
        public enum TILE { TILE_2BIT = 0, TILE_4BIT = 1, TILE_8BIT = 2 };

        public Pixel[] pixel_cache = new Pixel[256];

        public byte[] bg_tiledata = new byte[3];
        public byte[] bg_tiledata_state = new byte[3];  //0 = valid, 1 = dirty

        public void render_bg_tile(uint color_depth, ushort tile_num) { throw new NotImplementedException(); }
        public void flush_pixel_cache() { throw new NotImplementedException(); }
        public void alloc_tiledata_cache() { throw new NotImplementedException(); }
        public void flush_tiledata_cache() { throw new NotImplementedException(); }
        public void free_tiledata_cache() { throw new NotImplementedException(); }

        //windows.cpp
        public Window[] window = new Window[6];

        public void build_window_table(byte bg, bool mainscreen) { throw new NotImplementedException(); }
        public void build_window_tables(byte bg) { throw new NotImplementedException(); }

        //bg.cpp
        public BackgroundInfo[] bg_info = new BackgroundInfo[4];
        public void update_bg_info() { throw new NotImplementedException(); }

        public ushort bg_get_tile(uint bg, ushort x, ushort y) { throw new NotImplementedException(); }
        public void render_line_bg(uint mode, uint bg, uint color_depth, byte pri0_pos, byte pri1_pos) { throw new NotImplementedException(); }

        //oam.cpp
        public SpriteItem[] sprite_list = new SpriteItem[128];
        public bool sprite_list_valid;
        public uint active_sprite;

        public byte[] oam_itemlist = new byte[32];
        public OamTileItem[] oam_tilelist = new OamTileItem[34];

        public const int OAM_PRI_NONE = 4;
        public byte[] oam_line_pal = new byte[256], oam_line_pri = new byte[256];

        public void update_sprite_list(uint addr, byte data) { throw new NotImplementedException(); }
        public void build_sprite_list() { throw new NotImplementedException(); }
        public bool is_sprite_on_scanline() { throw new NotImplementedException(); }
        public void load_oam_tiles() { throw new NotImplementedException(); }
        public void render_oam_tile(int tile_num) { throw new NotImplementedException(); }
        public void render_line_oam_rto() { throw new NotImplementedException(); }
        public void render_line_oam(byte pri0_pos, byte pri1_pos, byte pri2_pos, byte pri3_pos) { throw new NotImplementedException(); }

        //mode7.cpp
        public void render_line_mode7(uint bg, byte pri0_pos, byte pri1_pos) { throw new NotImplementedException(); }

        //addsub.cpp
        public ushort addsub(uint x, uint y, bool halve) { throw new NotImplementedException(); }

        //line.cpp
        public ushort get_palette(byte index) { throw new NotImplementedException(); }
        public ushort get_direct_color(byte p, byte t) { throw new NotImplementedException(); }
        public ushort get_pixel_normal(uint x) { throw new NotImplementedException(); }
        public ushort get_pixel_swap(uint x) { throw new NotImplementedException(); }
        public void render_line_output() { throw new NotImplementedException(); }
        public void render_line_clear() { throw new NotImplementedException(); }

        public ushort[] surface;
        public ushort[] output;

        public byte ppu1_version;
        public byte ppu2_version;

        public static void Enter() { throw new NotImplementedException(); }
        public void add_clocks(uint clocks) { throw new NotImplementedException(); }

        public byte region;
        public uint line;

        public enum Region { NTSC = 0, PAL = 1 };
        public enum BG { BG1 = 0, BG2 = 1, BG3 = 2, BG4 = 3, OAM = 4, BACK = 5, COL = 5 };
        public enum SC { SC_32x32 = 0, SC_64x32 = 1, SC_32x64 = 2, SC_64x64 = 3 };

        public Display display = new Display();

        public Cache cache = new Cache();

        public bool interlace() { throw new NotImplementedException(); }
        public bool overscan() { throw new NotImplementedException(); }
        public bool hires() { throw new NotImplementedException(); }

        public ushort[,] light_table = new ushort[16, 32768];
        public ushort[,] mosaic_table = new ushort[16, 4096];
        public void render_line() { throw new NotImplementedException(); }

        public void update_oam_status() { throw new NotImplementedException(); }
        //required functions
        public void scanline() { throw new NotImplementedException(); }
        public void render_scanline() { throw new NotImplementedException(); }
        public void frame() { throw new NotImplementedException(); }
        public void enter() { throw new NotImplementedException(); }
        public void power() { throw new NotImplementedException(); }
        public void reset() { throw new NotImplementedException(); }

        public void serialize(Serializer s) { throw new NotImplementedException(); }
        public PPU() { throw new NotImplementedException(); }

        private PPUCounter _ppuCounter = new PPUCounter();
        public PPUCounter PPUCounter
        {
            get
            {
                return _ppuCounter;
            }
        }

        private Processor _processor = new Processor();
        public Processor Processor
        {
            get
            {
                return _processor;
            }
        }
    }
}
