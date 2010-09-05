using System;

namespace Snes.Chip.SuperFX
{
    partial class SuperFX
    {
        public class regs_t
        {
            public byte pipeline;
            public ushort ramaddr;

            public reg16_t[] r = new reg16_t[16];    //general purpose registers
            public sfr_t sfr;        //status flag register
            public byte pbr;        //program bank register
            public byte rombr;      //game pack ROM bank register
            public bool rambr;       //game pack RAM bank register
            public ushort cbr;       //cache base register
            public byte scbr;       //screen base register
            public scmr_t scmr;      //screen mode register
            public byte colr;       //color register
            public por_t por;        //plot option register
            public bool bramr;       //back-up RAM register
            public byte vcr;        //version code register
            public cfgr_t cfgr;      //config register
            public bool clsr;        //clock select register

            public uint romcl;   //clock ticks until romdr is valid
            public byte romdr;      //ROM buffer data register

            public uint ramcl;   //clock ticks until ramdr is valid
            public ushort ramar;     //RAM buffer address register
            public byte ramdr;      //RAM buffer data register

            public uint sreg, dreg;
            public reg16_t sr() { throw new NotImplementedException(); }  //source register (from)
            public reg16_t dr() { throw new NotImplementedException(); }  //destination register (to)

            public void reset() { throw new NotImplementedException(); }
        }
    }
}
