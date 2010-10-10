using System;

namespace Snes.Fast
{
    public delegate void DSPCopyFunction(byte[] io, object state, uint size);

    partial class SPCDSP
    {
        // Setup

        // Initializes DSP and has it use the 64K RAM provided
        public void init(object ram_64k) { throw new NotImplementedException(); }

        // Sets destination for output samples. If out is NULL or out_size is 0,
        // doesn't generate any.
        public void set_output(short[] _out, int out_size) { throw new NotImplementedException(); }

        // Number of samples written to output since it was last set, always
        // a multiple of 2. Undefined if more samples were generated than
        // output buffer could hold.
        public int sample_count() { throw new NotImplementedException(); }

        // Emulation

        // Resets DSP to power-on state
        public void reset() { throw new NotImplementedException(); }

        // Emulates pressing reset switch on SNES
        public void soft_reset() { throw new NotImplementedException(); }

        // Reads/writes DSP registers. For accuracy, you must first call run()
        // to catch the DSP up to present.
        public int read(int addr) { throw new NotImplementedException(); }
        public void write(int addr, int data) { throw new NotImplementedException(); }

        // Runs DSP for specified number of clocks (~1024000 per second). Every 32 clocks
        // a pair of samples is be generated.
        public void run(int clock_count) { throw new NotImplementedException(); }

        // Sound control

        // Mutes voices corresponding to non-zero bits in mask (issues repeated KOFF events).
        // Reduces emulation accuracy.
        public const int voice_count = 8;
        public void mute_voices(int mask) { throw new NotImplementedException(); }

        // State

        // Resets DSP and uses supplied values to initialize registers
        public const int register_count = 128;
        public void load(byte[] regs) { throw new NotImplementedException(); }

        // Saves/loads exact emulator state
        public const int state_size = 640; // maximum space needed when saving
        public void copy_state(byte[] io, DSPCopyFunction copyFunction) { throw new NotImplementedException(); }

        // Returns non-zero if new key-on events occurred since last call
        public bool check_kon() { throw new NotImplementedException(); }

        // DSP register addresses

        // Global registers
        public enum GlobalReg { mvoll = 0x0c, mvolr = 0x1c, evoll = 0x2c, evolr = 0x3c, kon = 0x4c, koff = 0x5c, flg = 0x6c, endx = 0x7c, efb = 0x0d, pmon = 0x2d, non = 0x3d, eon = 0x4d, dir = 0x5d, esa = 0x6d, edl = 0x7d, fir = 0x0f }

        // Voice registers
        private enum VoiceReg { voll = 0x00, volr = 0x01, pitchl = 0x02, pitchh = 0x03, srcn = 0x04, adsr0 = 0x05, adsr1 = 0x06, gain = 0x07, envx = 0x08, outx = 0x09 }

        public const int extra_size = 16;
        public short[] extra() { throw new NotImplementedException(); }
        public short[] out_pos() { throw new NotImplementedException(); }

        public const int echo_hist_size = 8;

        public enum EnvMode { release, attack, decay, sustain }
        public const int brr_buf_size = 12;
        private const int brr_block_size = 9;

        private State m = new State();

        private void init_counter() { throw new NotImplementedException(); }
        private void run_counters() { throw new NotImplementedException(); }
        private uint read_counter(int rate) { throw new NotImplementedException(); }

        private int interpolate(Voice v) { throw new NotImplementedException(); }
        private void run_envelope(Voice v) { throw new NotImplementedException(); }
        private void decode_brr(Voice v) { throw new NotImplementedException(); }

        private void misc_27() { throw new NotImplementedException(); }
        private void misc_28() { throw new NotImplementedException(); }
        private void misc_29() { throw new NotImplementedException(); }
        private void misc_30() { throw new NotImplementedException(); }

        private void voice_output(Voice v, int ch) { throw new NotImplementedException(); }
        private void voice_V1(Voice v) { throw new NotImplementedException(); }
        private void voice_V2(Voice v) { throw new NotImplementedException(); }
        private void voice_V3(Voice v) { throw new NotImplementedException(); }
        private void voice_V3a(Voice v) { throw new NotImplementedException(); }
        private void voice_V3b(Voice v) { throw new NotImplementedException(); }
        private void voice_V3c(Voice v) { throw new NotImplementedException(); }
        private void voice_V4(Voice v) { throw new NotImplementedException(); }
        private void voice_V5(Voice v) { throw new NotImplementedException(); }
        private void voice_V6(Voice v) { throw new NotImplementedException(); }
        private void voice_V7(Voice v) { throw new NotImplementedException(); }
        private void voice_V8(Voice v) { throw new NotImplementedException(); }
        private void voice_V9(Voice v) { throw new NotImplementedException(); }
        private void voice_V7_V4_V1(Voice v) { throw new NotImplementedException(); }
        private void voice_V8_V5_V2(Voice v) { throw new NotImplementedException(); }
        private void voice_V9_V6_V3(Voice v) { throw new NotImplementedException(); }

        private void echo_read(int ch) { throw new NotImplementedException(); }
        private int echo_output(int ch) { throw new NotImplementedException(); }
        private void echo_write(int ch) { throw new NotImplementedException(); }
        private void echo_22() { throw new NotImplementedException(); }
        private void echo_23() { throw new NotImplementedException(); }
        private void echo_24() { throw new NotImplementedException(); }
        private void echo_25() { throw new NotImplementedException(); }
        private void echo_26() { throw new NotImplementedException(); }
        private void echo_27() { throw new NotImplementedException(); }
        private void echo_28() { throw new NotImplementedException(); }
        private void echo_29() { throw new NotImplementedException(); }
        private void echo_30() { throw new NotImplementedException(); }

        private void soft_reset_common() { throw new NotImplementedException(); }
    }
}
