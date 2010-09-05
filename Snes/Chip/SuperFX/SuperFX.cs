using Snes.Memory;

namespace Snes.Chip.SuperFX
{
    class SuperFX : Coprocessor, IMMIO
    {
        public byte mmio_read(uint addr)
        {
            throw new global::System.NotImplementedException();
        }

        public void mmio_write(uint addr, byte data)
        {
            throw new global::System.NotImplementedException();
        }
    }
}
