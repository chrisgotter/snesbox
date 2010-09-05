using System;

namespace Snes.Chip.SuperFX
{
    partial class SuperFX
    {
        public class sfr_t
        {
            public byte irq;   //interrupt flag
            public byte b;     //WITH flag
            public byte ih;    //immediate higher 8-bit flag
            public byte il;    //immediate lower 8-bit flag
            public byte alt2;  //ALT2 mode
            public byte alt1;  //ALT2 instruction mode
            public byte r;     //ROM r14 read flag
            public byte g;     //GO flag
            public byte ov;    //overflow flag
            public byte s;     //sign flag
            public byte cy;    //carry flag
            public byte z;     //zero flag

            public static explicit operator uint(sfr_t flag) { throw new NotImplementedException(); }

            public sfr_t Assign(ushort data) { throw new NotImplementedException(); }
        }
    }
}
