#if COMPATIBILITY || PERFORMANCE
namespace Snes
{
    partial class PPU
    {
        public class OamTileItem
        {
            public ushort x, y, pri, pal, tile;
            public bool hflip;
        }
    }
}
#endif
