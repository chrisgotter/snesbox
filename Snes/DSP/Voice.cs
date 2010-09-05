using Nall;

namespace Snes.DSP
{
    partial class DSP
    {
        class Voice
        {
            ModuloArray<int> buffer = new ModuloArray<int>(brr_buf_size);  //decoded samples
            int buf_pos;     //place in buffer where next samples will be decoded
            int interp_pos;  //relative fractional position in sample (0x1000 = 1.0)
            int brr_addr;    //address of current BRR block
            int brr_offset;  //current decoding offset in BRR block
            int vbit;        //bitmask for voice: 0x01 for voice 0, 0x02 for voice 1, etc
            int vidx;        //voice channel register index: 0x00 for voice 0, 0x10 for voice 1, etc
            int kon_delay;   //KON delay/current setup phase
            int env_mode;
            int env;         //current envelope level
            int t_envx_out;
            int hidden_env;  //used by GAIN mode 7, very obscure quirk
        }
    }
}
