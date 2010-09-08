using System;

namespace Snes
{
    public class ByteRef
    {
        public ByteRef(byte value) { throw new NotImplementedException(); }
    }

    partial class SMPCore
    {
        public class RegYA
        {
            public ByteRef hi, lo;

            public static explicit operator ushort(RegYA reg) { throw new NotImplementedException(); }

            public RegYA Assign(ushort data) { throw new NotImplementedException(); }

            public RegYA(ByteRef hi_, ByteRef lo_) { throw new NotImplementedException(); }
        }
    }
}
