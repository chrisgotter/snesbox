using System;

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
                    throw new NotImplementedException();
                }
            }

            public byte h
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public static explicit operator uint(Reg16 reg16)
            {
                throw new NotImplementedException();
            }

            public uint Assign(uint i)
            {
                throw new NotImplementedException();
            }

            public static uint operator |(Reg16 reg16, uint i)
            {
                throw new NotImplementedException();
            }

            public static uint operator ^(Reg16 reg16, uint i)
            {
                throw new NotImplementedException();
            }

            public static uint operator &(Reg16 reg16, uint i)
            {
                throw new NotImplementedException();
            }

            public static uint operator <<(Reg16 reg16, int i)
            {
                throw new NotImplementedException();
            }

            public static uint operator >>(Reg16 reg16, int i)
            {
                throw new NotImplementedException();
            }

            public static uint operator +(Reg16 reg16, uint i)
            {
                throw new NotImplementedException();
            }

            public static uint operator -(Reg16 reg16, uint i)
            {
                throw new NotImplementedException();
            }

            public static uint operator *(Reg16 reg16, uint i)
            {
                throw new NotImplementedException();
            }

            public static uint operator /(Reg16 reg16, uint i)
            {
                throw new NotImplementedException();
            }

            public static uint operator %(Reg16 reg16, uint i)
            {
                throw new NotImplementedException();
            }

            public Reg16()
            {
                throw new NotImplementedException();
            }
        }
    }
}
