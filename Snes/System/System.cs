using System;

namespace Snes
{
    class System
    {
        public static System Default = new System();

        public enum Region : uint
        {
            NTSC = 0, PAL = 1, Autodetect = 2
        }

        public enum ExpansionPortDevice : uint
        {
            None = 0, BSX = 1
        }

        public void run() { throw new NotImplementedException(); }
        public void runtosave() { throw new NotImplementedException(); }

        public void init() { throw new NotImplementedException(); }
        public void term() { throw new NotImplementedException(); }
        public void power() { throw new NotImplementedException(); }
        public void reset() { throw new NotImplementedException(); }
        public void unload() { throw new NotImplementedException(); }

        public void frame() { throw new NotImplementedException(); }
        public void scanline() { throw new NotImplementedException(); }

        //return *active* system information (settings are cached upon power-on)
        public Region region { get { throw new NotImplementedException(); } }
        public ExpansionPortDevice expansion { get { throw new NotImplementedException(); } }
        public uint cpu_frequency { get { throw new NotImplementedException(); } }
        public uint apu_frequency { get { throw new NotImplementedException(); } }

        public System() { throw new NotImplementedException(); }

        public Interface.Interface Interface;

        private void runthreadtosave() { throw new NotImplementedException(); }
    }
}
