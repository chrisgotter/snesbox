using System;

namespace Snes.Configuration
{
    partial class Configuration
    {
        public Snes.Input.Input.Device controller_port1;
        public Snes.Input.Input.Device controller_port2;
        public System.System.ExpansionPortDevice expansion_port;
        public System.System.Region region;

        public CPU cpu;
        public SMP smp;
        public PPU1 ppu1;
        public PPU2 ppu2;
        public SuperFX superfx;

        public Configuration() { throw new NotImplementedException(); }
    }
}
