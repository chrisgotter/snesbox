﻿using System;
using System.IO;

namespace Snes.Chip.MSU1
{
    partial class MSU1 : Coprocessor, Memory.IMMIO
    {
        public static void Enter() { throw new NotImplementedException(); }
        public void enter() { throw new NotImplementedException(); }
        public void init() { throw new NotImplementedException(); }
        public void enable() { throw new NotImplementedException(); }
        public void power() { throw new NotImplementedException(); }
        public void reset() { throw new NotImplementedException(); }

        public byte mmio_read(uint addr) { throw new NotImplementedException(); }
        public void mmio_write(uint addr, byte data) { throw new NotImplementedException(); }

        private FileStream datafile;
        private FileStream audiofile;

        private enum Flag
        {
            DataBusy = 0x80,
            AudioBusy = 0x40,
            AudioRepeating = 0x20,
            AudioPlaying = 0x10,
            Revision = 0x01,
        }

        private MMIO mmio;
    }
}
