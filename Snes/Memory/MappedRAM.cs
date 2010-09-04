using System;

namespace Snes.Memory
{
    class MappedRAM : Memory
    {
        public void reset() { throw new NotImplementedException(); }
        public void map(byte[] source, uint length) { throw new NotImplementedException(); }
        public void copy(byte[] data, uint size) { throw new NotImplementedException(); }

        public void write_protect(bool status) { throw new NotImplementedException(); }
        public byte[] data() { throw new NotImplementedException(); }
        public override uint size() { throw new NotImplementedException(); }

        public override byte read(uint addr) { throw new NotImplementedException(); }
        public override void write(uint addr, byte n) { throw new NotImplementedException(); }
        public byte this[uint addr] { get { throw new NotImplementedException(); } }

        public MappedRAM() { throw new NotImplementedException(); }

        private byte[] data_;
        private uint size_;
        private bool write_protect_;
    }
}
