using System;
using Nall;

namespace Snes
{
    partial class Bus
    {
        public static Bus bus = new Bus();

        public uint mirror(uint addr, uint size) { throw new NotImplementedException(); }
        public void map(uint addr, Memory access, uint offset) { throw new NotImplementedException(); }
        public enum MapMode : uint { Direct, Linear, Shadow }
        public void map(MapMode mode, byte bank_lo, byte bank_hi, ushort addr_lo, ushort addr_hi, Memory access, uint offset = 0, uint size = 0) { throw new NotImplementedException(); }

        public byte read(uint24 addr) { throw new NotImplementedException(); }
        public void write(uint24 addr, byte data) { throw new NotImplementedException(); }

        public bool load_cart() { throw new NotImplementedException(); }
        public void unload_cart() { throw new NotImplementedException(); }

        public void power() { throw new NotImplementedException(); }
        public void reset() { throw new NotImplementedException(); }

        public Page[] page = new Page[65536];

        private void map_reset() { throw new NotImplementedException(); }
        private void map_xml() { throw new NotImplementedException(); }
        private void map_system() { throw new NotImplementedException(); }
    }
}
