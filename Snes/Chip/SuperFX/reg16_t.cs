using System;

namespace Snes.Chip.SuperFX
{
    partial class SuperFX
    {
        public class reg16_t
        {
            public delegate void Modify(ushort i);
            public ushort data;
            public Modify on_modify;

            public static explicit operator uint(reg16_t reg) { throw new NotImplementedException(); }
            public ushort Assign(ushort i) { throw new NotImplementedException(); }

            public static reg16_t operator ++(reg16_t reg) { throw new NotImplementedException(); }
            public static reg16_t operator --(reg16_t reg) { throw new NotImplementedException(); }
            public static uint operator |(reg16_t reg, uint i) { throw new NotImplementedException(); }
            public static uint operator ^(reg16_t reg, uint i) { throw new NotImplementedException(); }
            public static uint operator &(reg16_t reg, uint i) { throw new NotImplementedException(); }
            public static uint operator <<(reg16_t reg, int i) { throw new NotImplementedException(); }
            public static uint operator >>(reg16_t reg, int i) { throw new NotImplementedException(); }
            public static uint operator +(reg16_t reg, uint i) { throw new NotImplementedException(); }
            public static uint operator -(reg16_t reg, uint i) { throw new NotImplementedException(); }
            public static uint operator *(reg16_t reg, uint i) { throw new NotImplementedException(); }
            public static uint operator /(reg16_t reg, uint i) { throw new NotImplementedException(); }
            public static uint operator %(reg16_t reg, uint i) { throw new NotImplementedException(); }

            public reg16_t() { throw new NotImplementedException(); }
        }
    }
}
