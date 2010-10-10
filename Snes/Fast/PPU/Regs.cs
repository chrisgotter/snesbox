
namespace Snes.Fast
{
    partial class PPU
    {
        public class Regs
        {
            //open bus support
            byte ppu1_mdr, ppu2_mdr;

            //bg line counters
            ushort[] bg_y = new ushort[4];

            //internal state
            ushort ioamaddr;
            ushort icgramaddr;

            //$2100
            bool display_disabled;
            byte display_brightness;

            //$2101
            byte oam_basesize;
            byte oam_nameselect;
            ushort oam_tdaddr;

            //$2102-$2103
            ushort oam_baseaddr;
            ushort oam_addr;
            bool oam_priority;
            byte oam_firstsprite;

            //$2104
            byte oam_latchdata;

            //$2105
            bool[] bg_tilesize = new bool[4];
            bool bg3_priority;
            byte bg_mode;

            //$2106
            byte mosaic_size;
            bool[] mosaic_enabled = new bool[4];
            ushort mosaic_countdown;

            //$2107-$210a
            ushort[] bg_scaddr = new ushort[4];
            byte[] bg_scsize = new byte[4];

            //$210b-$210c
            ushort[] bg_tdaddr = new ushort[4];

            //$210d-$2114
            byte bg_ofslatch;
            ushort m7_hofs, m7_vofs;
            ushort[] bg_hofs = new ushort[4];
            ushort[] bg_vofs = new ushort[4];

            //$2115
            bool vram_incmode;
            byte vram_mapping;
            byte vram_incsize;

            //$2116-$2117
            ushort vram_addr;

            //$211a
            byte mode7_repeat;
            bool mode7_vflip;
            bool mode7_hflip;

            //$211b-$2120
            byte m7_latch;
            ushort m7a, m7b, m7c, m7d, m7x, m7y;

            //$2121
            ushort cgram_addr;

            //$2122
            byte cgram_latchdata;

            //$2123-$2125
            bool[] window1_enabled = new bool[6];
            bool[] window1_invert = new bool[6];
            bool[] window2_enabled = new bool[6];
            bool[] window2_invert = new bool[6];

            //$2126-$2129
            byte window1_left, window1_right;
            byte window2_left, window2_right;

            //$212a-$212b
            byte[] window_mask = new byte[6];

            //$212c-$212d
            bool[] bg_enabled = new bool[5], bgsub_enabled = new bool[5];

            //$212e-$212f
            bool[] window_enabled = new bool[5], sub_window_enabled = new bool[5];

            //$2130
            byte color_mask, colorsub_mask;
            bool addsub_mode;
            bool direct_color;

            //$2131
            bool color_mode, color_halve;
            bool[] color_enabled = new bool[6];

            //$2132
            byte color_r, color_g, color_b;
            ushort color_rgb;

            //$2133
            //overscan and interlace are checked once per frame to
            //determine if entire frame should be interlaced/non-interlace
            //and overscan adjusted. therefore, the variables act sort of
            //like a buffer, but they do still affect internal rendering
            bool mode7_extbg;
            bool pseudo_hires;
            bool overscan;
            ushort scanlines;
            bool oam_interlace;
            bool interlace;

            //$2137
            ushort hcounter, vcounter;
            bool latch_hcounter, latch_vcounter;
            bool counters_latched;

            //$2139-$213a
            ushort vram_readbuffer;

            //$213e
            bool time_over, range_over;
            ushort oam_itemcount, oam_tilecount;
        }
    }
}
