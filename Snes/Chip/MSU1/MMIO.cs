﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snes.Chip.MSU1
{
    partial class MSU1
    {
        private class MMIO
        {
            uint data_offset;
            uint audio_offset;
            ushort audio_track;
            byte audio_volume;
            bool data_busy;
            bool audio_busy;
            bool audio_repeat;
            bool audio_play;
        }
    }
}
