using Nall;

namespace Snes
{
    partial class CPUCore
    {
        public class Reg16
        {
            public ushort w;

            public byte l
            {
                get
                {
                    return Bit.LSB2(w, 0);
                }
            }

            public byte h
            {
                get
                {
                    return Bit.LSB2(w, 1);
                }
            }

            public static explicit operator uint(Reg16 reg16)
            {
                return reg16.w;
            }

            public uint Assign(uint i)
            {
                return w = (ushort)i;
            }

            public static uint operator |(Reg16 reg16, uint i)
            {
                return reg16.w |= (ushort)i;
            }

            public static uint operator ^(Reg16 reg16, uint i)
            {
                return reg16.w ^= (ushort)i;
            }

            public static uint operator &(Reg16 reg16, uint i)
            {
                return reg16.w &= (ushort)i;
            }

            public static uint operator <<(Reg16 reg16, int i)
            {
                return reg16.w <<= i;
            }

            public static uint operator >>(Reg16 reg16, int i)
            {
                return reg16.w >>= i;
            }

            public static uint operator +(Reg16 reg16, uint i)
            {
                return reg16.w += (ushort)i;
            }

            public static uint operator -(Reg16 reg16, uint i)
            {
                return reg16.w -= (ushort)i;
            }

            public static uint operator *(Reg16 reg16, uint i)
            {
                return reg16.w *= (ushort)i;
            }

            public static uint operator /(Reg16 reg16, uint i)
            {
                return reg16.w /= (ushort)i;
            }

            public static uint operator %(Reg16 reg16, uint i)
            {
                return reg16.w %= (ushort)i;
            }

            public Reg16()
            {
                w = 0;
            }
        }
    }
}
