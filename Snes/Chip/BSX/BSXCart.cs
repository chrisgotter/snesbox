﻿using System;
using Snes.Memory;

namespace Snes.Chip.BSX
{
    partial class BSXCart : IMMIO
    {
        public void init() { throw new NotImplementedException(); }
        public void enable() { throw new NotImplementedException(); }
        public void power() { throw new NotImplementedException(); }
        public void reset() { throw new NotImplementedException(); }

        public byte mmio_read(uint addr) { throw new NotImplementedException(); }
        public void mmio_write(uint addr, byte data) { throw new NotImplementedException(); }

        public BSXCart() { throw new NotImplementedException(); }

        private Regs regs;

        private void update_memory_map() { throw new NotImplementedException(); }
    }
}
