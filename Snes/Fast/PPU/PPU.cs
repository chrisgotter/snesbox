using System;
using Nall;

namespace Snes.Fast
{
    partial class PPU : IPPUCounter, IProcessor, IMMIO
    {
        public static PPU ppu = new PPU();

        public void step(uint clocks)
        {
            Processor.clock += clocks;
        }

        public void synchronize_cpu()
        {
#if THREADED
            if (Processor.clock >= 0 && Scheduler.scheduler.sync != Scheduler.SynchronizeMode.All)
            {
                Libco.Switch(CPU.cpu.Processor.thread);
            }
#else
            while(clock >= 0) 
            {
                cpu.enter();
            }
#endif
        }

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
        public ArraySegment<ushort> output;

        public byte ppu1_version;
        public byte ppu2_version;

        public static void Enter()
        {
            PPU.ppu.enter();
        }

        public void add_clocks(uint clocks)
        {
            PPUCounter.tick(clocks);
            step(clocks);
            synchronize_cpu();
        }

        public byte region;
        public uint line;

        public enum Region { NTSC = 0, PAL = 1 };
        public enum ID { BG1 = 0, BG2 = 1, BG3 = 2, BG4 = 3, OAM = 4, BACK = 5, COL = 5 };
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
        public void scanline()
        {
            line = PPUCounter.vcounter();

            if (line == 0)
            {
                frame();

                //RTO flag reset
                regs.time_over = false;
                regs.range_over = false;
            }

            if (line == 1)
            {
                //mosaic reset
                for (int bg = (int)ID.BG1; bg <= (int)ID.BG4; bg++)
                {
                    regs.bg_y[bg] = 1;
                }
                regs.mosaic_countdown = (ushort)(regs.mosaic_size + 1);
                regs.mosaic_countdown--;
            }
            else
            {
                for (int bg = (int)ID.BG1; bg <= (int)ID.BG4; bg++)
                {
                    if (!regs.mosaic_enabled[bg] || !Convert.ToBoolean(regs.mosaic_countdown))
                    {
                        regs.bg_y[bg] = (ushort)line;
                    }
                }
                if (!Convert.ToBoolean(regs.mosaic_countdown))
                {
                    regs.mosaic_countdown = (ushort)(regs.mosaic_size + 1);
                }
                regs.mosaic_countdown--;
            }
        }

        public void render_scanline()
        {
            if (line >= 1 && line < (!overscan() ? 225 : 240))
            {
                render_line_oam_rto();
                render_line();
            }
        }

        public void frame()
        {
            System.system.frame();

            if (PPUCounter.field() == Convert.ToBoolean(0))
            {
                display.interlace = regs.interlace;
                regs.scanlines = (regs.overscan == false) ? (ushort)224 : (ushort)239;
            }
        }

        public void enter()
        {
            while (true)
            {
                if (Scheduler.scheduler.sync == Scheduler.SynchronizeMode.All)
                {
                    Scheduler.scheduler.exit(Scheduler.ExitReason.SynchronizeEvent);
                }

                //H =    0 (initialize)
                scanline();
                add_clocks(10);

                //H =   10 (cache mode7 registers + OAM address reset)
                cache.m7_hofs = regs.m7_hofs;
                cache.m7_vofs = regs.m7_vofs;
                cache.m7a = regs.m7a;
                cache.m7b = regs.m7b;
                cache.m7c = regs.m7c;
                cache.m7d = regs.m7d;
                cache.m7x = regs.m7x;
                cache.m7y = regs.m7y;
                if (PPUCounter.vcounter() == (!overscan() ? 225 : 240))
                {
                    if (regs.display_disabled == false)
                    {
                        regs.oam_addr = (ushort)(regs.oam_baseaddr << 1);
                        regs.oam_firstsprite = (regs.oam_priority == false) ? (byte)0 : (byte)((regs.oam_addr >> 2) & 127);
                    }
                }
                add_clocks(502);

                //H =  512 (render)
                render_scanline();
                add_clocks(640);

                //H = 1152 (cache OBSEL)
                if (cache.oam_basesize != regs.oam_basesize)
                {
                    cache.oam_basesize = regs.oam_basesize;
                    sprite_list_valid = false;
                }
                cache.oam_nameselect = regs.oam_nameselect;
                cache.oam_tdaddr = regs.oam_tdaddr;
                add_clocks(PPUCounter.lineclocks() - 1152U);  //seek to start of next scanline

            }
        }

        public void power()
        {
            ppu1_version = (byte)Configuration.config.ppu1.version;
            ppu2_version = (byte)Configuration.config.ppu2.version;

            for (uint i = 0; i < StaticRAM.vram.size(); i++)
            {
                StaticRAM.vram[i] = 0x00;
            }
            for (uint i = 0; i < StaticRAM.oam.size(); i++)
            {
                StaticRAM.oam[i] = 0x00;
            }
            for (uint i = 0; i < StaticRAM.cgram.size(); i++)
            {
                StaticRAM.cgram[i] = 0x00;
            }
            flush_tiledata_cache();

            region = (byte)(System.system.region == System.Region.NTSC ? 0 : 1);  //0 = NTSC, 1 = PAL

            regs.ioamaddr = 0x0000;
            regs.icgramaddr = 0x01ff;

            //$2100
            regs.display_disabled = true;
            regs.display_brightness = 15;

            //$2101
            regs.oam_basesize = 0;
            regs.oam_nameselect = 0;
            regs.oam_tdaddr = 0x0000;

            cache.oam_basesize = 0;
            cache.oam_nameselect = 0;
            cache.oam_tdaddr = 0x0000;

            //$2102-$2103
            regs.oam_baseaddr = 0x0000;
            regs.oam_addr = 0x0000;
            regs.oam_priority = false;
            regs.oam_firstsprite = 0;

            //$2104
            regs.oam_latchdata = 0x00;

            //$2105
            regs.bg_tilesize[(int)ID.BG1] = Convert.ToBoolean(0);
            regs.bg_tilesize[(int)ID.BG2] = Convert.ToBoolean(0);
            regs.bg_tilesize[(int)ID.BG3] = Convert.ToBoolean(0);
            regs.bg_tilesize[(int)ID.BG4] = Convert.ToBoolean(0);
            regs.bg3_priority = Convert.ToBoolean(0);
            regs.bg_mode = 0;

            //$2106
            regs.mosaic_size = 0;
            regs.mosaic_enabled[(int)ID.BG1] = false;
            regs.mosaic_enabled[(int)ID.BG2] = false;
            regs.mosaic_enabled[(int)ID.BG3] = false;
            regs.mosaic_enabled[(int)ID.BG4] = false;
            regs.mosaic_countdown = 0;

            //$2107-$210a
            regs.bg_scaddr[(int)ID.BG1] = 0x0000;
            regs.bg_scaddr[(int)ID.BG2] = 0x0000;
            regs.bg_scaddr[(int)ID.BG3] = 0x0000;
            regs.bg_scaddr[(int)ID.BG4] = 0x0000;
            regs.bg_scsize[(int)ID.BG1] = (byte)SC.SC_32x32;
            regs.bg_scsize[(int)ID.BG2] = (byte)SC.SC_32x32;
            regs.bg_scsize[(int)ID.BG3] = (byte)SC.SC_32x32;
            regs.bg_scsize[(int)ID.BG4] = (byte)SC.SC_32x32;

            //$210b-$210c
            regs.bg_tdaddr[(int)ID.BG1] = 0x0000;
            regs.bg_tdaddr[(int)ID.BG2] = 0x0000;
            regs.bg_tdaddr[(int)ID.BG3] = 0x0000;
            regs.bg_tdaddr[(int)ID.BG4] = 0x0000;

            //$210d-$2114
            regs.bg_ofslatch = 0x00;
            regs.m7_hofs = regs.m7_vofs = 0x0000;
            regs.bg_hofs[(int)ID.BG1] = regs.bg_vofs[(int)ID.BG1] = 0x0000;
            regs.bg_hofs[(int)ID.BG2] = regs.bg_vofs[(int)ID.BG2] = 0x0000;
            regs.bg_hofs[(int)ID.BG3] = regs.bg_vofs[(int)ID.BG3] = 0x0000;
            regs.bg_hofs[(int)ID.BG4] = regs.bg_vofs[(int)ID.BG4] = 0x0000;

            //$2115
            regs.vram_incmode = Convert.ToBoolean(1);
            regs.vram_mapping = 0;
            regs.vram_incsize = 1;

            //$2116-$2117
            regs.vram_addr = 0x0000;

            //$211a
            regs.mode7_repeat = 0;
            regs.mode7_vflip = false;
            regs.mode7_hflip = false;

            //$211b-$2120
            regs.m7_latch = 0x00;
            regs.m7a = 0x0000;
            regs.m7b = 0x0000;
            regs.m7c = 0x0000;
            regs.m7d = 0x0000;
            regs.m7x = 0x0000;
            regs.m7y = 0x0000;

            //$2121
            regs.cgram_addr = 0x0000;

            //$2122
            regs.cgram_latchdata = 0x00;

            //$2123-$2125
            regs.window1_enabled[(int)ID.BG1] = false;
            regs.window1_enabled[(int)ID.BG2] = false;
            regs.window1_enabled[(int)ID.BG3] = false;
            regs.window1_enabled[(int)ID.BG4] = false;
            regs.window1_enabled[(int)ID.OAM] = false;
            regs.window1_enabled[(int)ID.COL] = false;

            regs.window1_invert[(int)ID.BG1] = false;
            regs.window1_invert[(int)ID.BG2] = false;
            regs.window1_invert[(int)ID.BG3] = false;
            regs.window1_invert[(int)ID.BG4] = false;
            regs.window1_invert[(int)ID.OAM] = false;
            regs.window1_invert[(int)ID.COL] = false;

            regs.window2_enabled[(int)ID.BG1] = false;
            regs.window2_enabled[(int)ID.BG2] = false;
            regs.window2_enabled[(int)ID.BG3] = false;
            regs.window2_enabled[(int)ID.BG4] = false;
            regs.window2_enabled[(int)ID.OAM] = false;
            regs.window2_enabled[(int)ID.COL] = false;

            regs.window2_invert[(int)ID.BG1] = false;
            regs.window2_invert[(int)ID.BG2] = false;
            regs.window2_invert[(int)ID.BG3] = false;
            regs.window2_invert[(int)ID.BG4] = false;
            regs.window2_invert[(int)ID.OAM] = false;
            regs.window2_invert[(int)ID.COL] = false;

            //$2126-$2129
            regs.window1_left = 0x00;
            regs.window1_right = 0x00;
            regs.window2_left = 0x00;
            regs.window2_right = 0x00;

            //$212a-$212b
            regs.window_mask[(int)ID.BG1] = 0;
            regs.window_mask[(int)ID.BG2] = 0;
            regs.window_mask[(int)ID.BG3] = 0;
            regs.window_mask[(int)ID.BG4] = 0;
            regs.window_mask[(int)ID.OAM] = 0;
            regs.window_mask[(int)ID.COL] = 0;

            //$212c-$212d
            regs.bg_enabled[(int)ID.BG1] = false;
            regs.bg_enabled[(int)ID.BG2] = false;
            regs.bg_enabled[(int)ID.BG3] = false;
            regs.bg_enabled[(int)ID.BG4] = false;
            regs.bg_enabled[(int)ID.OAM] = false;
            regs.bgsub_enabled[(int)ID.BG1] = false;
            regs.bgsub_enabled[(int)ID.BG2] = false;
            regs.bgsub_enabled[(int)ID.BG3] = false;
            regs.bgsub_enabled[(int)ID.BG4] = false;
            regs.bgsub_enabled[(int)ID.OAM] = false;

            //$212e-$212f
            regs.window_enabled[(int)ID.BG1] = false;
            regs.window_enabled[(int)ID.BG2] = false;
            regs.window_enabled[(int)ID.BG3] = false;
            regs.window_enabled[(int)ID.BG4] = false;
            regs.window_enabled[(int)ID.OAM] = false;
            regs.sub_window_enabled[(int)ID.BG1] = false;
            regs.sub_window_enabled[(int)ID.BG2] = false;
            regs.sub_window_enabled[(int)ID.BG3] = false;
            regs.sub_window_enabled[(int)ID.BG4] = false;
            regs.sub_window_enabled[(int)ID.OAM] = false;

            //$2130
            regs.color_mask = 0;
            regs.colorsub_mask = 0;
            regs.addsub_mode = false;
            regs.direct_color = false;

            //$2131
            regs.color_mode = Convert.ToBoolean(0);
            regs.color_halve = false;
            regs.color_enabled[(int)ID.BACK] = false;
            regs.color_enabled[(int)ID.OAM] = false;
            regs.color_enabled[(int)ID.BG4] = false;
            regs.color_enabled[(int)ID.BG3] = false;
            regs.color_enabled[(int)ID.BG2] = false;
            regs.color_enabled[(int)ID.BG1] = false;

            //$2132
            regs.color_r = 0x00;
            regs.color_g = 0x00;
            regs.color_b = 0x00;
            regs.color_rgb = 0x0000;

            //$2133
            regs.mode7_extbg = false;
            regs.pseudo_hires = false;
            regs.overscan = false;
            regs.scanlines = 224;
            regs.oam_interlace = false;
            regs.interlace = false;

            //$2137
            regs.hcounter = 0;
            regs.vcounter = 0;
            regs.latch_hcounter = Convert.ToBoolean(0);
            regs.latch_vcounter = Convert.ToBoolean(0);
            regs.counters_latched = false;

            //$2139-$213a
            regs.vram_readbuffer = 0x0000;

            //$213e
            regs.time_over = false;
            regs.range_over = false;

            reset();
        }

        public void reset()
        {
            Processor.create("PPU", Enter, System.system.cpu_frequency);
            PPUCounter.reset();
            Array.Clear(surface, 0, surface.Length);

            frame();

            //$2100
            regs.display_disabled = true;

            display.interlace = false;
            display.overscan = false;
            regs.scanlines = 224;

            Array.Clear(sprite_list, 0, sprite_list.Length);
            sprite_list_valid = false;

            //open bus support
            regs.ppu1_mdr = 0xff;
            regs.ppu2_mdr = 0xff;

            //bg line counters
            regs.bg_y[0] = 0;
            regs.bg_y[1] = 0;
            regs.bg_y[2] = 0;
            regs.bg_y[3] = 0;
        }

        public void serialize(Serializer s) { throw new NotImplementedException(); }

        public PPU()
        {
            //TODO: remove this hack
            surface = new ushort[1024 * 1024];
            output = new ArraySegment<ushort>(surface, 16 * 512, surface.Length - (16 * 512));

            alloc_tiledata_cache();

            for (uint l = 0; l < 16; l++)
            {
                for (uint i = 0; i < 4096; i++)
                {
                    mosaic_table[l,i] = (ushort)((i / (l + 1)) * (l + 1));
                }
            }

            for (uint l = 0; l < 16; l++)
            {
                for (uint r = 0; r < 32; r++)
                {
                    for (uint g = 0; g < 32; g++)
                    {
                        for (uint b = 0; b < 32; b++)
                        {
                            double luma = (double)l / 15.0;
                            uint ar = (uint)(luma * r + 0.5);
                            uint ag = (uint)(luma * g + 0.5);
                            uint ab = (uint)(luma * b + 0.5);
                            light_table[l,(r << 10) + (g << 5) + b] = (ushort)((ab << 10) + (ag << 5) + ar);
                        }
                    }
                }
            }
        }

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
