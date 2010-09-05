using System;

namespace Snes.Cheat
{
    class Cheat : CheatCode
    {
        public enum Type : uint { ProActionReplay, GameGenie }

        public bool enabled() { throw new NotImplementedException(); }
        public void enable(bool state) { throw new NotImplementedException(); }
        public void synchronize() { throw new NotImplementedException(); }
        public bool read(uint addr, ref byte data) { throw new NotImplementedException(); }

        public bool active() { throw new NotImplementedException(); }
        public bool exists(uint addr) { throw new NotImplementedException(); }

        public Cheat() { throw new NotImplementedException(); }

        public static bool decode(string s, ref uint addr, ref byte data, Type type) { throw new NotImplementedException(); }
        public static bool encode(string s, uint addr, byte data, Type type) { throw new NotImplementedException(); }

        private byte[] bitmask = new byte[0x200000];
        private bool system_enabled;
        private bool code_enabled;
        private bool cheat_enabled;
        private uint mirror(uint addr) { throw new NotImplementedException(); }
    }
}
