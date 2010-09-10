using System;

namespace Snes
{
    partial class SMPCore
    {
        public class Regs
        {
            public ushort pc;
            public byte[] r = new byte[4];
            public byte[] a, x, y, sp;
            public RegYA ya;
            public Flag p = new Flag();

            public Regs()
            {
                //TODO: Verify array segments simulate &byte
                a = new ArraySegment<byte>(r, 0, 1).Array;
                x = new ArraySegment<byte>(r, 1, 1).Array;
                y = new ArraySegment<byte>(r, 2, 1).Array;
                sp = new ArraySegment<byte>(r, 3, 1).Array;
                ya = new RegYA(new ArraySegment<byte>(r, 2, 1).Array, new ArraySegment<byte>(r, 0, 1).Array);
            }
        }
    }
}
