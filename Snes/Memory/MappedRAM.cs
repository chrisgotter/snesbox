using System;

namespace Snes.Memory
{
    class MappedRAM : Memory
    {
        public static MappedRAM cartrom;
        public static MappedRAM cartram;
        public static MappedRAM cartrtc;
        public static MappedRAM bsxflash;
        public static MappedRAM bsxram;
        public static MappedRAM bsxpram;
        public static MappedRAM stArom;
        public static MappedRAM stAram;
        public static MappedRAM stBrom;
        public static MappedRAM stBram;
        public static MappedRAM gbrom;
        public static MappedRAM gbram;
        public static MappedRAM gbrtc;

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
