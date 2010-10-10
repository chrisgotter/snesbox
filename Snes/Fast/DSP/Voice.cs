
namespace Snes.Fast
{
    partial class SPCDSP
    {
        public class Voice
        {
            int[] buf = new int[brr_buf_size * 2];// decoded samples (twice the size to simplify wrap handling)
            int buf_pos;            // place in buffer where next samples will be decoded
            int interp_pos;         // relative fractional position in sample (0x1000 = 1.0)
            int brr_addr;           // address of current BRR block
            int brr_offset;         // current decoding offset in BRR block
            byte[] regs;          // pointer to voice's DSP registers
            int vbit;               // bitmask for voice: 0x01 for voice 0, 0x02 for voice 1, etc.
            int kon_delay;          // KON delay/current setup phase
            EnvMode env_mode;
            int env;                // current envelope level
            int hidden_env;         // used by GAIN mode 7, very obscure quirk
            byte t_envx_out;
        }
    }
}
