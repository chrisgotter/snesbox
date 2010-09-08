using System;

namespace Snes.PPU
{
    public delegate void Scanline();

    //PPUcounter emulates the H/V latch counters of the S-PPU2.
    //
    //real hardware has the S-CPU maintain its own copy of these counters that are
    //updated based on the state of the S-PPU Vblank and Hblank pins. emulating this
    //would require full lock-step synchronization for every clock tick.
    //to bypass this and allow the two to run out-of-order, both the CPU and PPU
    //classes inherit PPUcounter and keep their own counters.
    //the timers are kept in sync, as the only differences occur on V=240 and V=261,
    //based on interlace. thus, we need only synchronize and fetch interlace at any
    //point before this in the frame, which is handled internally by this class at
    //V=128.

    partial class PPUCounter : IPPUCounter
    {
        public void tick() { throw new NotImplementedException(); }
        public void tick(uint clocks) { throw new NotImplementedException(); }

        public bool field() { throw new NotImplementedException(); }
        public ushort vcounter() { throw new NotImplementedException(); }
        public ushort hcounter() { throw new NotImplementedException(); }
        public ushort hdot() { throw new NotImplementedException(); }
        public ushort lineclocks() { throw new NotImplementedException(); }

        public bool field(uint offset) { throw new NotImplementedException(); }
        public ushort vcounter(uint offset) { throw new NotImplementedException(); }
        public ushort hcounter(uint offset) { throw new NotImplementedException(); }

        public void reset() { throw new NotImplementedException(); }
        public Scanline scanline;

        private void vcounter_tick() { throw new NotImplementedException(); }

        private Status status;
        private History history;

        PPUCounter IPPUCounter.PPUCounter
        {
            get
            {
                return this;
            }
        }
    }
}
