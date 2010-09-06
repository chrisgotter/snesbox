using System;

namespace Snes.Cheat
{
    class CheatCode
    {
        public bool enabled;
        public uint[] addr;
        public byte[] data;

        public bool Assign(string s) { throw new NotImplementedException(); }
        public CheatCode() { throw new NotImplementedException(); }
    }
}
