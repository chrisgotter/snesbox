using System;

namespace Snes.Memory
{
    class StaticRAM : Memory
    {
        public byte[] data() { throw new NotImplementedException(); }
        public override uint size() { throw new NotImplementedException(); }

        public override byte read(uint addr) { throw new NotImplementedException(); }
        public override void write(uint addr, byte n) { throw new NotImplementedException(); }
        public byte this[uint addr] { get { throw new NotImplementedException(); } }

        public StaticRAM(uint size) { throw new NotImplementedException(); }

        private byte[] data_;
        private uint size_;
    }
}
