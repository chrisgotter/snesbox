
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
                public class Flag
                {
                    public uint priority;  //0 = none (transparent)
                    public uint palette;
                }
                public Flag main, sub;
            }
        }
    }

    partial class PPU
    {
        partial class Window
        {
            public class Output
            {
                public class Flag
                {
                    public bool color_enable;
                }
                public Flag main, sub;
            }
        }
    }
}
