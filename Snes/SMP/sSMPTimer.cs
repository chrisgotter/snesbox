using System;

namespace Snes.SMP
{
    partial class SMP
    {
        class sSMPTimer
        {
            public byte stage0_ticks;
            public byte stage1_ticks;
            public byte stage2_ticks;
            public byte stage3_ticks;
            public bool current_line;
            public bool enabled;
            public byte target;

            public void tick() { throw new NotImplementedException(); }
            public void sync_stage1() { throw new NotImplementedException(); }

            public sSMPTimer(uint timer_frequency) { throw new NotImplementedException(); }
        }
    }
}
