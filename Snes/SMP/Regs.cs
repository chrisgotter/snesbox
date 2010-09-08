using System;

namespace Snes
{
    partial class SMPCore
    {
        public class Regs
        {
            public ushort pc;
            public ByteRef[] r = new ByteRef[4];
            public ByteRef a, x, y, sp;
            public RegYA ya;
            public SMPCore.Flag p;
            public Regs() { throw new NotImplementedException(); }
        }
    }
}
