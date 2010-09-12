using System;

namespace Nall
{
    public static class Bit
    {
        public static uint uclamp(int bits, uint x)
        {
            var y = (1U << bits) - 1;
            return (uint)(y + ((x - y) & -(x < y ? 1 : 0)));  //min(x, y);
        }

        public static uint uclip(int bits, uint x)
        {
            var m = (1U << bits) - 1;
            return (x & m);
        }

        public static int sclamp(int bits, int x)
        {
            var b = 1U << (bits - 1);
            var m = (1U << (bits - 1)) - 1;
            return (int)((x > m) ? m : (x < -b) ? -b : x);
        }

        public static int sclip(int bits, int x)
        {
            var b = 1U << (bits - 1);
            var m = (1U << bits) - 1;
            return (int)(((x & m) ^ b) - b);
        }

        public static uint bit(uint value)
        {
            return Convert.ToUInt32(Convert.ToBoolean(value));
        }
    }
}
