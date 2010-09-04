using System;

namespace Snes.Cheat
{
    class CheatCode
    {
        public bool Enabled { get; set; }
        public uint[] addr;
        public byte[] data;

        public string CheatString { set { throw new NotImplementedException(); } }
        public CheatCode() { throw new NotImplementedException(); }
    }
}
