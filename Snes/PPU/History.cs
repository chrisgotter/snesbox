
namespace Snes
{
    partial class PPUCounter
    {
        private class History
        {
            bool[] field = new bool[2048];
            ushort[] vcounter = new ushort[2048];
            ushort[] hcounter = new ushort[2048];

            int index;
        }
    }
}
