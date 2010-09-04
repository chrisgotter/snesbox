using System;

namespace Snes.CPU
{
    partial class CPUCore
    {
        public class reg16_t
        {
            public ushort w;
            public byte l { get { throw new NotImplementedException(); } }
            public byte h { get { throw new NotImplementedException(); } }
            public static explicit operator uint(reg16_t reg) { throw new NotImplementedException(); }
            public uint Assign(uint i) { throw new NotImplementedException(); }
            public static uint operator |(reg16_t left, uint i) { throw new NotImplementedException(); }
            public static uint operator ^(reg16_t left, uint i) { throw new NotImplementedException(); }
            public static uint operator &(reg16_t left, uint i) { throw new NotImplementedException(); }
            public static uint operator <<(reg16_t left, int i) { throw new NotImplementedException(); }
            public static uint operator >>(reg16_t left, int i) { throw new NotImplementedException(); }
            public static uint operator +(reg16_t left, uint i) { throw new NotImplementedException(); }
            public static uint operator -(reg16_t left, uint i) { throw new NotImplementedException(); }
            public static uint operator *(reg16_t left, uint i) { throw new NotImplementedException(); }
            public static uint operator /(reg16_t left, uint i) { throw new NotImplementedException(); }
            public static uint operator %(reg16_t left, uint i) { throw new NotImplementedException(); }
            public reg16_t() { throw new NotImplementedException(); }
        }
    }
}
