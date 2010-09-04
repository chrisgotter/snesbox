using System;

namespace Snes.CPU
{
    partial class CPUCore
    {
        public class reg24_t
        {
            public uint d;
            public ushort w { get { throw new NotImplementedException(); } }
            public ushort wh { get { throw new NotImplementedException(); } }
            public byte l { get { throw new NotImplementedException(); } }
            public byte h { get { throw new NotImplementedException(); } }
            public byte b { get { throw new NotImplementedException(); } }
            public byte bh { get { throw new NotImplementedException(); } }
            public static explicit operator uint(reg24_t reg) { throw new NotImplementedException(); }
            public uint Assign(uint i) { throw new NotImplementedException(); }
            public static uint operator |(reg24_t left, uint i) { throw new NotImplementedException(); }
            public static uint operator ^(reg24_t left, uint i) { throw new NotImplementedException(); }
            public static uint operator &(reg24_t left, uint i) { throw new NotImplementedException(); }
            public static uint operator <<(reg24_t left, int i) { throw new NotImplementedException(); }
            public static uint operator >>(reg24_t left, int i) { throw new NotImplementedException(); }
            public static uint operator +(reg24_t left, uint i) { throw new NotImplementedException(); }
            public static uint operator -(reg24_t left, uint i) { throw new NotImplementedException(); }
            public static uint operator *(reg24_t left, uint i) { throw new NotImplementedException(); }
            public static uint operator /(reg24_t left, uint i) { throw new NotImplementedException(); }
            public static uint operator %(reg24_t left, uint i) { throw new NotImplementedException(); }
            public reg24_t() { throw new NotImplementedException(); }
        }
    }
}
