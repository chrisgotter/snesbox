
namespace Snes.PPU
{
    partial class PPU
    {
        partial class Background
        {
            public class Output
            {
                class Flag
                {
                    uint priority;  //0 = none (transparent)
                    uint palette;
                    uint tile;
                }
                Flag main, sub;
            }
        }
    }

    partial class PPU
    {
        partial class Sprite
        {
            public class Output
            {
                class Flag
                {
                    uint priority;  //0 = none (transparent)
                    uint palette;
                }
                Flag main, sub;
            }
        }
    }

    partial class PPU
    {
        partial class Window
        {
            public class Output
            {
                class Flag
                {
                    bool color_enable;
                }
                Flag main, sub;
            }
        }
    }
}
