
namespace Snes
{
    partial class SMPCore
    {
        public class RegYA
        {
            public byte[] hi, lo;

            public static explicit operator ushort(RegYA reg)
            {
                return (ushort)((reg.hi[0] << 8) + reg.lo[0]);
            }

            public RegYA Assign(ushort data)
            {
                hi[0] = (byte)(data >> 8);
                lo[0] = (byte)data;
                return this;
            }

            public RegYA(byte[] hi_, byte[] lo_)
            {
                hi = hi_;
                lo = lo_;
            }
        }
    }
}
