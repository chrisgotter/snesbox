
namespace Snes.Fast
{
    partial class SPCDSP
    {
        private class State
        {
            byte[] regs = new byte[register_count];

            // Echo history keeps most recent 8 samples (twice the size to simplify wrap handling)
            int[][] echo_hist = new int[echo_hist_size * 2][];
            int[] echo_hist_pos; // &echo_hist [0 to 7]

            int every_other_sample; // toggles every sample
            int kon;                // KON value when last checked
            int noise;
            int counter;
            int echo_offset;        // offset from ESA in echo buffer
            int echo_length;        // number of bytes that echo_offset will stop at
            int phase;              // next clock cycle to run (0-31)
            bool kon_check;         // set when a new KON occurs

            // Hidden registers also written to when main register is written to
            int new_kon;
            byte endx_buf;
            byte envx_buf;
            byte outx_buf;

            // Temporary state between clocks

            // read once per sample
            int t_pmon;
            int t_non;
            int t_eon;
            int t_dir;
            int t_koff;

            // read a few clocks ahead then used
            int t_brr_next_addr;
            int t_adsr0;
            int t_brr_header;
            int t_brr_byte;
            int t_srcn;
            int t_esa;
            int t_echo_enabled;

            // internal state that is recalculated every sample
            int t_dir_addr;
            int t_pitch;
            int t_output;
            int t_looped;
            int t_echo_ptr;

            // left/right sums
            int[] t_main_out = new int[2];
            int[] t_echo_out = new int[2];
            int[] t_echo_in = new int[2];

            Voice[] voices = new Voice[voice_count];

            // non-emulation state
            byte[] ram; // 64K shared RAM between DSP and SMP
            int mute_mask;
            short[] _out;
            short[] out_end;
            short[] out_begin;
            short[] extra = new short[extra_size];
        }
    }
}
