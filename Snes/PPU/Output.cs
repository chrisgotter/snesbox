
namespace Snes
{
    partial class PPU
    {
        partial class Background
        {
            public class Output
            {
                public class Flag
                {
                    public uint priority;  //0 = none (transparent)
                    public uint palette;
                    public uint tile;
                }
                public Flag main, sub;
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
