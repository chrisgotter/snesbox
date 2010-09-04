using System;

namespace Nall
{
    public struct uint10
    {
        private uint data;

        public static explicit operator uint(uint10 number) { throw new NotImplementedException(); }
        public static uint10 operator ++(uint10 i) { throw new NotImplementedException(); }
        public static uint10 operator --(uint10 i) { throw new NotImplementedException(); }
        public static uint Assign(uint i) { throw new NotImplementedException(); }
        public static uint operator |(uint10 number, uint i) { throw new NotImplementedException(); }
        public static uint operator ^(uint10 number, uint i) { throw new NotImplementedException(); }
        public static uint operator &(uint10 number, uint i) { throw new NotImplementedException(); }
        public static uint operator <<(uint10 number, int i) { throw new NotImplementedException(); }
        public static uint operator >>(uint10 number, int i) { throw new NotImplementedException(); }
        public static uint operator +(uint10 number, uint i) { throw new NotImplementedException(); }
        public static uint operator -(uint10 number, uint i) { throw new NotImplementedException(); }
        public static uint operator *(uint10 number, uint i) { throw new NotImplementedException(); }
        public static uint operator /(uint10 number, uint i) { throw new NotImplementedException(); }
        public static uint operator %(uint10 number, uint i) { throw new NotImplementedException(); }

        public uint10(uint i) { throw new NotImplementedException(); }
    }
}
