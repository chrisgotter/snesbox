
namespace Snes
{
    partial class PPU
    {
        public class Regs
        {
            public byte ppu1_mdr;
            public byte ppu2_mdr;

            public ushort vram_readbuffer;
            public byte oam_latchdata;
            public byte cgram_latchdata;
            public byte bgofs_latchdata;
            public byte mode7_latchdata;
            public bool counters_latched;
            public bool latch_hcounter;
            public bool latch_vcounter;

            public ushort ioamaddr;
            public ushort icgramaddr;

            //$2100  INIDISP
            public bool display_disabled;
            public uint display_brightness;

            //$2102  OAMADDL
            //$2103  OAMADDH
            public ushort oam_baseaddr;
            public ushort oam_addr;
            public bool oam_priority;

            //$2105  BGMODE
            public bool bg3_priority;
            public byte bgmode;

            //$210d  BG1HOFS
            public ushort mode7_hoffset;

            //$210e  BG1VOFS
            public ushort mode7_voffset;

            //$2115  VMAIN
            public bool vram_incmode;
            public byte vram_mapping;
            public byte vram_incsize;

            //$2116  VMADDL
            //$2117  VMADDH
            public ushort vram_addr;

            //$211a  M7SEL
            public byte mode7_repeat;
            public bool mode7_vflip;
            public bool mode7_hflip;

            //$211b  M7A
            public ushort m7a;

            //$211c  M7B
            public ushort m7b;

            //$211d  M7C
            public ushort m7c;

            //$211e  M7D
            public ushort m7d;

            //$211f  M7X
            public ushort m7x;

            //$2120  M7Y
            public ushort m7y;

            //$2121  CGADD
            public ushort cgram_addr;

            //$2133  SETINI
            public bool mode7_extbg;
            public bool pseudo_hires;
            public bool overscan;
            public bool interlace;

            //$213c  OPHCT
            public ushort hcounter;

            //$213d  OPVCT
            public ushort vcounter;
        }
    }

    partial class PPU
    {
        partial class Background
        {
            public class Regs
            {
                public uint tiledata_addr;
                public uint screen_addr;
                public uint screen_size;
                public uint mosaic;
                public bool tile_size;

                public uint mode;
                public uint priority0;
                public uint priority1;

                public bool main_enabled;
                public bool sub_enabled;

                public uint hoffset;
                public uint voffset;
            }
        }
    }

    partial class PPU
    {
        partial class Screen
        {
            public class Regs
            {
                bool addsub_mode;
                bool direct_color;

                bool color_mode;
                bool color_halve;
                bool bg1_color_enable;
                bool bg2_color_enable;
                bool bg3_color_enable;
                bool bg4_color_enable;
                bool oam_color_enable;
                bool back_color_enable;

                byte color_b;
                byte color_g;
                byte color_r;
            }
        }
    }

    partial class PPU
    {
        partial class Sprite
        {
            public class Regs
            {
                bool main_enabled;
                bool sub_enabled;
                bool interlace;

                byte base_size;
                byte nameselect;
                ushort tiledata_addr;
                byte first_sprite;

                uint priority0;
                uint priority1;
                uint priority2;
                uint priority3;

                bool time_over;
                bool range_over;
            }
        }
    }

    partial class PPU
    {
        partial class Window
        {
            public class Regs
            {
                bool bg1_one_enable;
                bool bg1_one_invert;
                bool bg1_two_enable;
                bool bg1_two_invert;

                bool bg2_one_enable;
                bool bg2_one_invert;
                bool bg2_two_enable;
                bool bg2_two_invert;

                bool bg3_one_enable;
                bool bg3_one_invert;
                bool bg3_two_enable;
                bool bg3_two_invert;

                bool bg4_one_enable;
                bool bg4_one_invert;
                bool bg4_two_enable;
                bool bg4_two_invert;

                bool oam_one_enable;
                bool oam_one_invert;
                bool oam_two_enable;
                bool oam_two_invert;

                bool col_one_enable;
                bool col_one_invert;
                bool col_two_enable;
                bool col_two_invert;

                byte one_left;
                byte one_right;
                byte two_left;
                byte two_right;

                byte bg1_mask;
                byte bg2_mask;
                byte bg3_mask;
                byte bg4_mask;
                byte oam_mask;
                byte col_mask;

                bool bg1_main_enable;
                bool bg1_sub_enable;
                bool bg2_main_enable;
                bool bg2_sub_enable;
                bool bg3_main_enable;
                bool bg3_sub_enable;
                bool bg4_main_enable;
                bool bg4_sub_enable;
                bool oam_main_enable;
                bool oam_sub_enable;

                byte col_main_mask;
                byte col_sub_mask;
            }
        }
    }
}
