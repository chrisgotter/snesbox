
namespace Snes
{
    partial class PPU
    {
        private partial class Sprite
        {
            public class State
            {
                uint x;
                uint y;

                uint item_count;
                uint tile_count;

                bool active;
                byte[,] item = new byte[2, 32];
                TileItem[,] tile = new TileItem[2, 34];
            }
        }
    }
}
