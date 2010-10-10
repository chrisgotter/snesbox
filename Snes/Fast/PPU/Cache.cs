
namespace Snes.Fast
{
    partial class PPU
    {
        public class Cache
        {
            //$2101
            byte oam_basesize;
            byte oam_nameselect;
            ushort oam_tdaddr;

            //$210d-$210e
            ushort m7_hofs, m7_vofs;

            //$211b-$2120
            ushort m7a, m7b, m7c, m7d, m7x, m7y;
        }
    }
}
