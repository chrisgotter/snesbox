
namespace Snes.Fast
{
    partial class PPU
    {
        public class SpriteItem
        {
            byte width, height;
            ushort x, y;
            byte character;
            bool use_nameselect;
            bool vflip, hflip;
            byte palette;
            byte priority;
            bool size;
        }
    }
}
