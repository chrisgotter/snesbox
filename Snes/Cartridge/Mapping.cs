using System;
using Snes.Memory;

namespace Snes.Cartridge
{
    partial class Cartridge
    {
        public class Mapping
        {
            public Snes.Memory.Memory memory;
            public IMMIO mmio;
            public Bus.MapMode mode;
            public uint banklo;
            public uint bankhi;
            public uint addrlo;
            public uint addrhi;
            public uint offset;
            public uint size;

            public Mapping() { throw new NotImplementedException(); }
            public Mapping(Snes.Memory.Memory memory) { throw new NotImplementedException(); }
            public Mapping(IMMIO mmio) { throw new NotImplementedException(); }
        }
    }
}
