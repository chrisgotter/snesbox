using System;

namespace Snes
{
    abstract class Memory
    {
        public virtual uint size() { throw new NotImplementedException(); }
        public abstract byte read(uint addr);
        public abstract void write(uint addr, byte data);
    }
}
