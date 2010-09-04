
namespace Snes.PPU
{
    partial class PPU
    {
        partial class Background
        {
            public class Regs
            {
                uint tiledata_addr;
                uint screen_addr;
                uint screen_size;
                uint mosaic;
                bool tile_size;

                uint mode;
                uint priority0;
                uint priority1;

                bool main_enabled;
                bool sub_enabled;

                uint hoffset;
                uint voffset;
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
