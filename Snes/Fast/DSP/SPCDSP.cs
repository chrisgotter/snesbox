using System;
using System.Diagnostics;
using System.IO;

namespace Snes.Fast
{
    partial class SPCDSP
    {
        public delegate void DSPCopyFunction(Stream io, object state, uint size);

        // Setup

        private void Clamp16(ref int io)
        {
            if ((short)io != io)
                io = (io >> 31) ^ 0x7FFF;
        }


        // Initializes DSP and has it use the 64K RAM provided
        public void init(object ram_64k)
        {
            m.ram = (byte[])ram_64k;
            mute_voices(0);
            disable_surround(false);
            set_output(null, 0);
            reset();

#if DEBUG
            unchecked
            {
                // be sure this sign-extends
                Debug.Assert((short)0x8000 == -0x8000);

                // be sure right shift preserves sign
                Debug.Assert((-1 >> 1) == -1);

                // check clamp macro
                int i;
                i = +0x8000;
                Clamp16(ref i);
                Debug.Assert(i == +0x7FFF);
                i = -0x8001;
                Clamp16(ref i);
                Debug.Assert(i == -0x8000);
            }
#endif
        }

        // Sets destination for output samples. If out is NULL or out_size is 0,
        // doesn't generate any.
        public void set_output(short[] _out, int out_size)
        {
            Debug.Assert((out_size & 1) == 0); // must be even
            if (ReferenceEquals(_out, null))
            {
                _out = m.extra;
                out_size = extra_size;
            }
            m._out = new ArraySegment<short>(_out, 0, out_size);
        }

        // Number of samples written to output since it was last set, always
        // a multiple of 2. Undefined if more samples were generated than
        // output buffer could hold.
        public int sample_count()
        {
            return m._out.Offset;
        }

        // Emulation

        public static readonly byte[] initial_regs = new byte[SPCDSP.register_count] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xE0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

        // Resets DSP to power-on state
        public void reset()
        {
            load(initial_regs);
        }

        // Emulates pressing reset switch on SNES
        public void soft_reset()
        {
            m.regs[(int)GlobalReg.flg] = 0xE0;
            soft_reset_common();
        }

        // Reads/writes DSP registers. For accuracy, you must first call run()
        // to catch the DSP up to present.
        public int read(int addr)
        {
            Debug.Assert((uint)addr < register_count);
            return m.regs[addr];
        }

        public void write(int addr, int data)
        {
            Debug.Assert((uint)addr < register_count);

            m.regs[addr] = (byte)data;
            switch ((VoiceReg)(addr & 0x0F))
            {
                case VoiceReg.envx:
                    m.envx_buf = (byte)data;
                    break;
                case VoiceReg.outx:
                    m.outx_buf = (byte)data;
                    break;
                case (VoiceReg)0x0C:
                    if (addr == (int)GlobalReg.kon)
                        m.new_kon = (byte)data;

                    if (addr == (int)GlobalReg.endx) // always cleared, regardless of data written
                    {
                        m.endx_buf = 0;
                        m.regs[(int)GlobalReg.endx] = 0;
                    }
                    break;
            }
        }

        public void V(string clock, int voice)
        {
            this.GetType().GetMethod("voice_" + clock).Invoke(this, new object[] { m.voices[voice] });
        }

        public bool Phase(int n, int clocks_remain)
        {
            return Convert.ToBoolean(n) && !Convert.ToBoolean(--clocks_remain);
        }

        // Runs DSP for specified number of clocks (~1024000 per second). Every 32 clocks
        // a pair of samples is be generated.
        public void run(int clocks_remain)
        {
            Debug.Assert(clocks_remain > 0);

            int phase = m.phase;
            m.phase = (phase + clocks_remain) & 31;
            switch (phase)
            {
                case 0:
                    if (Phase(0, clocks_remain))
                        break;
                    V("V5", 0);
                    V("V2", 1);
                    goto case 1;
                case 1:
                    if (Phase(1, clocks_remain))
                        break;
                    V("V6", 0);
                    V("V3", 1);
                    goto case 2;
                case 2:
                    if (Phase(2, clocks_remain))
                        break;
                    V("V7_V4_V1", 0);
                    goto case 3;
                case 3:
                    if (Phase(3, clocks_remain))
                        break;
                    V("V8_V5_V2", 0);
                    goto case 4;
                case 4:
                    if (Phase(4, clocks_remain))
                        break;
                    V("V9_V6_V3", 0);
                    goto case 5;
                case 5:
                    if (Phase(5, clocks_remain))
                        break;
                    V("V7_V4_V1", 1);
                    goto case 6;
                case 6:
                    if (Phase(6, clocks_remain))
                        break;
                    V("V8_V5_V2", 1);
                    goto case 7;
                case 7:
                    if (Phase(7, clocks_remain))
                        break;
                    V("V9_V6_V3", 1);
                    goto case 8;
                case 8:
                    if (Phase(8, clocks_remain))
                        break;
                    V("V7_V4_V1", 2);
                    goto case 9;
                case 9:
                    if (Phase(9, clocks_remain))
                        break;
                    V("V8_V5_V2", 2);
                    goto case 10;
                case 10:
                    if (Phase(10, clocks_remain))
                        break;
                    V("V9_V6_V3", 2);
                    goto case 11;
                case 11:
                    if (Phase(11, clocks_remain))
                        break;
                    V("V7_V4_V1", 3);
                    goto case 12;
                case 12:
                    if (Phase(12, clocks_remain))
                        break;
                    V("V8_V5_V2", 3);
                    goto case 13;
                case 13:
                    if (Phase(13, clocks_remain))
                        break;
                    V("V9_V6_V3", 3);
                    goto case 14;
                case 14:
                    if (Phase(14, clocks_remain))
                        break;
                    V("V7_V4_V1", 4);
                    goto case 15;
                case 15:
                    if (Phase(15, clocks_remain))
                        break;
                    V("V8_V5_V2", 4);
                    goto case 16;
                case 16:
                    if (Phase(16, clocks_remain))
                        break;
                    V("V9_V6_V3", 4);
                    goto case 17;
                case 17:
                    if (Phase(17, clocks_remain))
                        break;
                    V("V1", 0);
                    V("V7", 5);
                    V("V4", 6);
                    goto case 18;
                case 18:
                    if (Phase(18, clocks_remain))
                        break;
                    V("V8_V5_V2", 5);
                    goto case 19;
                case 19:
                    if (Phase(19, clocks_remain))
                        break;
                    V("V9_V6_V3", 5);
                    goto case 20;
                case 20:
                    if (Phase(20, clocks_remain))
                        break;
                    V("V1", 1);
                    V("V7", 6);
                    V("V4", 7);
                    goto case 21;
                case 21:
                    if (Phase(21, clocks_remain))
                        break;
                    V("V8", 6);
                    V("V5", 7);
                    V("V2", 0);  /* t_brr_next_addr order dependency */
                    goto case 22;
                case 22:
                    if (Phase(22, clocks_remain))
                        break;
                    V("V3a", 0);
                    V("V9", 6);
                    V("V6", 7);
                    echo_22();
                    goto case 23;
                case 23:
                    if (Phase(23, clocks_remain))
                        break;
                    V("V7", 7);
                    echo_23();
                    goto case 24;
                case 24:
                    if (Phase(24, clocks_remain))
                        break;
                    V("V8", 7);
                    echo_24();
                    goto case 25;
                case 25:
                    if (Phase(25, clocks_remain))
                        break;
                    V("V3b", 0);
                    V("V9", 7);
                    echo_25();
                    goto case 26;
                case 26:
                    if (Phase(26, clocks_remain))
                        break;
                    echo_26();
                    goto case 27;
                case 27:
                    if (Phase(27, clocks_remain))
                        break;
                    misc_27();
                    echo_27();
                    goto case 28;
                case 28:
                    if (Phase(28, clocks_remain))
                        break;
                    misc_28();
                    echo_28();
                    goto case 29;
                case 29:
                    if (Phase(29, clocks_remain))
                        break;
                    misc_29();
                    echo_29();
                    goto case 30;
                case 30:
                    if (Phase(30, clocks_remain))
                        break;
                    misc_30();
                    V("V3c", 0);
                    echo_30();
                    goto case 31;
                case 31:
                    if (Phase(31, clocks_remain))
                        break;
                    V("V4", 0);
                    V("V1", 2);

                    if (Convert.ToBoolean(--clocks_remain))
                    {
                        goto case 0;
                    }
                    else
                    {
                        break;
                    }
            }
        }

        // Sound control

        // Mutes voices corresponding to non-zero bits in mask (issues repeated KOFF events).
        // Reduces emulation accuracy.
        public const int voice_count = 8;

        public void mute_voices(int mask)
        {
            m.mute_mask = mask;
        }

        // State

        // Resets DSP and uses supplied values to initialize registers
        public const int register_count = 128;

        public void load(byte[] regs)
        {
            Array.Copy(regs, m.regs, m.regs.Length);

            //TODO: What the HELL is this doing?
            //memset( &m.regs [register_count], 0, offsetof (state_t,ram) - register_count );

            // Internal state
            for (int i = voice_count; --i >= 0; )
            {
                Voice v = m.voices[i];
                v.brr_offset = 1;
                v.vbit = 1 << i;
                v.regs = new ArraySegment<byte>(m.regs, i * 0x10, m.regs.Length - (i * 0x10));
            }
            m.new_kon = m.regs[(int)GlobalReg.kon];
            m.t_dir = m.regs[(int)GlobalReg.dir];
            m.t_esa = m.regs[(int)GlobalReg.esa];

            soft_reset_common();
        }

        // Saves/loads exact emulator state
        public const int state_size = 640; // maximum space needed when saving

        public void copy_state(Stream io, DSPCopyFunction copy)
        {
            SPCStateCopier copier = new SPCStateCopier(io, copy);

            // DSP registers
            copier.copy(m.regs, register_count);

            // Internal state

            // Voices
            for (int i = 0; i < voice_count; i++)
            {
                Voice v = m.voices[i];

                // BRR buffer
                for (int j = 0; j < brr_buf_size; j++)
                {
                    int s = v.buf[j];
                    copier.SPCCopy(sizeof(short), s);
                    v.buf[j] = v.buf[j + brr_buf_size] = s;
                }

                copier.SPCCopy(sizeof(ushort), v.interp_pos);
                copier.SPCCopy(sizeof(ushort), v.brr_addr);
                copier.SPCCopy(sizeof(ushort), v.env);
                copier.SPCCopy(sizeof(short), v.hidden_env);
                copier.SPCCopy(sizeof(byte), v.buf_pos);
                copier.SPCCopy(sizeof(byte), v.brr_offset);
                copier.SPCCopy(sizeof(byte), v.kon_delay);
                {
                    int mode = (int)v.env_mode;
                    copier.SPCCopy(sizeof(byte), mode);
                    v.env_mode = (EnvMode)mode;
                }
                copier.SPCCopy(sizeof(byte), v.t_envx_out);

                copier.extra();
            }

            // Echo history
            for (int i = 0; i < echo_hist_size; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    int s = m.echo_hist[i][j];
                    copier.SPCCopy(sizeof(short), s);
                    m.echo_hist[i][j] = s; // write back at offset 0
                }
            }
            m.echo_hist_pos = m.echo_hist[0];
            Array.Copy(m.echo_hist, m.echo_hist[echo_hist_size], echo_hist_size * m.echo_hist[0].Length);

            // Misc
            copier.SPCCopy(sizeof(byte), m.every_other_sample);
            copier.SPCCopy(sizeof(byte), m.kon);

            copier.SPCCopy(sizeof(ushort), m.noise);
            copier.SPCCopy(sizeof(ushort), m.counter);
            copier.SPCCopy(sizeof(ushort), m.echo_offset);
            copier.SPCCopy(sizeof(ushort), m.echo_length);
            copier.SPCCopy(sizeof(byte), m.phase);

            copier.SPCCopy(sizeof(byte), m.new_kon);
            copier.SPCCopy(sizeof(byte), m.endx_buf);
            copier.SPCCopy(sizeof(byte), m.envx_buf);
            copier.SPCCopy(sizeof(byte), m.outx_buf);

            copier.SPCCopy(sizeof(byte), m.t_pmon);
            copier.SPCCopy(sizeof(byte), m.t_non);
            copier.SPCCopy(sizeof(byte), m.t_eon);
            copier.SPCCopy(sizeof(byte), m.t_dir);
            copier.SPCCopy(sizeof(byte), m.t_koff);

            copier.SPCCopy(sizeof(ushort), m.t_brr_next_addr);
            copier.SPCCopy(sizeof(byte), m.t_adsr0);
            copier.SPCCopy(sizeof(byte), m.t_brr_header);
            copier.SPCCopy(sizeof(byte), m.t_brr_byte);
            copier.SPCCopy(sizeof(byte), m.t_srcn);
            copier.SPCCopy(sizeof(byte), m.t_esa);
            copier.SPCCopy(sizeof(byte), m.t_echo_enabled);

            copier.SPCCopy(sizeof(short), m.t_main_out[0]);
            copier.SPCCopy(sizeof(short), m.t_main_out[1]);
            copier.SPCCopy(sizeof(short), m.t_echo_out[0]);
            copier.SPCCopy(sizeof(short), m.t_echo_out[1]);
            copier.SPCCopy(sizeof(short), m.t_echo_in[0]);
            copier.SPCCopy(sizeof(short), m.t_echo_in[1]);

            copier.SPCCopy(sizeof(ushort), m.t_dir_addr);
            copier.SPCCopy(sizeof(ushort), m.t_pitch);
            copier.SPCCopy(sizeof(short), m.t_output);
            copier.SPCCopy(sizeof(ushort), m.t_echo_ptr);
            copier.SPCCopy(sizeof(byte), m.t_looped);

            copier.extra();
        }

        // Returns non-zero if new key-on events occurred since last call
        public bool check_kon()
        {
            bool old = m.kon_check;
            m.kon_check = Convert.ToBoolean(0);
            return old;
        }

        // DSP register addresses

        // Global registers
        public enum GlobalReg { mvoll = 0x0c, mvolr = 0x1c, evoll = 0x2c, evolr = 0x3c, kon = 0x4c, koff = 0x5c, flg = 0x6c, endx = 0x7c, efb = 0x0d, pmon = 0x2d, non = 0x3d, eon = 0x4d, dir = 0x5d, esa = 0x6d, edl = 0x7d, fir = 0x0f }

        // Voice registers
        private enum VoiceReg { voll = 0x00, volr = 0x01, pitchl = 0x02, pitchh = 0x03, srcn = 0x04, adsr0 = 0x05, adsr1 = 0x06, gain = 0x07, envx = 0x08, outx = 0x09 }

        public const int extra_size = 16;

        public short[] extra()
        {
            return m.extra;
        }

        public ArraySegment<short> out_pos()
        {
            return m._out;
        }

        public void disable_surround(bool disable) { } // not supported

        public const int echo_hist_size = 8;

        public enum EnvMode { release, attack, decay, sustain }

        public const int brr_buf_size = 12;

        private const int brr_block_size = 9;

        private State m = new State();

        private void init_counter()
        {
            m.counter = 0;
        }

        private const int simple_counter_range = 2048 * 5 * 3; // 30720
        private void run_counters()
        {
            if (--m.counter < 0)
            {
                m.counter = simple_counter_range - 1;
            }
        }

        private static readonly uint[] counter_rates = new uint[32] { simple_counter_range + 1 /* never fires*/, 2048, 1536, 1280, 1024, 768, 640, 512, 384, 320, 256, 192, 160, 128, 96, 80, 64, 48, 40, 32, 24, 20, 16, 12, 10, 8, 6, 5, 4, 3, 2, 1 };
        private static readonly uint[] counter_offsets = new uint[32] { 1, 0, 1040, 536, 0, 1040, 536, 0, 1040, 536, 0, 1040, 536, 0, 1040, 536, 0, 1040, 536, 0, 1040, 536, 0, 1040, 536, 0, 1040, 536, 0, 1040, 0, 0 };

        private uint read_counter(int rate)
        {
            return ((uint)m.counter + counter_offsets[rate]) % counter_rates[rate];
        }

        private static readonly short[] gauss = new short[512]
        {
            0,   0,   0,   0,   0,   0,   0,   0,   0,   0,   0,   0,   0,   0,   0,   0,
            1,   1,   1,   1,   1,   1,   1,   1,   1,   1,   1,   2,   2,   2,   2,   2,
            2,   2,   3,   3,   3,   3,   3,   4,   4,   4,   4,   4,   5,   5,   5,   5,
            6,   6,   6,   6,   7,   7,   7,   8,   8,   8,   9,   9,   9,  10,  10,  10,
            11,  11,  11,  12,  12,  13,  13,  14,  14,  15,  15,  15,  16,  16,  17,  17,
            18,  19,  19,  20,  20,  21,  21,  22,  23,  23,  24,  24,  25,  26,  27,  27,
            28,  29,  29,  30,  31,  32,  32,  33,  34,  35,  36,  36,  37,  38,  39,  40,
            41,  42,  43,  44,  45,  46,  47,  48,  49,  50,  51,  52,  53,  54,  55,  56,
            58,  59,  60,  61,  62,  64,  65,  66,  67,  69,  70,  71,  73,  74,  76,  77,
            78,  80,  81,  83,  84,  86,  87,  89,  90,  92,  94,  95,  97,  99, 100, 102,
            104, 106, 107, 109, 111, 113, 115, 117, 118, 120, 122, 124, 126, 128, 130, 132,
            134, 137, 139, 141, 143, 145, 147, 150, 152, 154, 156, 159, 161, 163, 166, 168,
            171, 173, 175, 178, 180, 183, 186, 188, 191, 193, 196, 199, 201, 204, 207, 210,
            212, 215, 218, 221, 224, 227, 230, 233, 236, 239, 242, 245, 248, 251, 254, 257,
            260, 263, 267, 270, 273, 276, 280, 283, 286, 290, 293, 297, 300, 304, 307, 311,
            314, 318, 321, 325, 328, 332, 336, 339, 343, 347, 351, 354, 358, 362, 366, 370,
            374, 378, 381, 385, 389, 393, 397, 401, 405, 410, 414, 418, 422, 426, 430, 434,
            439, 443, 447, 451, 456, 460, 464, 469, 473, 477, 482, 486, 491, 495, 499, 504,
            508, 513, 517, 522, 527, 531, 536, 540, 545, 550, 554, 559, 563, 568, 573, 577,
            582, 587, 592, 596, 601, 606, 611, 615, 620, 625, 630, 635, 640, 644, 649, 654,
            659, 664, 669, 674, 678, 683, 688, 693, 698, 703, 708, 713, 718, 723, 728, 732,
            737, 742, 747, 752, 757, 762, 767, 772, 777, 782, 787, 792, 797, 802, 806, 811,
            816, 821, 826, 831, 836, 841, 846, 851, 855, 860, 865, 870, 875, 880, 884, 889,
            894, 899, 904, 908, 913, 918, 923, 927, 932, 937, 941, 946, 951, 955, 960, 965,
            969, 974, 978, 983, 988, 992, 997,1001,1005,1010,1014,1019,1023,1027,1032,1036,
            1040,1045,1049,1053,1057,1061,1066,1070,1074,1078,1082,1086,1090,1094,1098,1102,
            1106,1109,1113,1117,1121,1125,1128,1132,1136,1139,1143,1146,1150,1153,1157,1160,
            1164,1167,1170,1174,1177,1180,1183,1186,1190,1193,1196,1199,1202,1205,1207,1210,
            1213,1216,1219,1221,1224,1227,1229,1232,1234,1237,1239,1241,1244,1246,1248,1251,
            1253,1255,1257,1259,1261,1263,1265,1267,1269,1270,1272,1274,1275,1277,1279,1280,
            1282,1283,1284,1286,1287,1288,1290,1291,1292,1293,1294,1295,1296,1297,1297,1298,
            1299,1300,1300,1301,1302,1302,1303,1303,1303,1304,1304,1304,1304,1304,1305,1305,
        };

        private int interpolate(Voice v)
        { 	// Make pointers into gaussian based on fractional position between samples
            int offset = v.interp_pos >> 4 & 0xFF;
            ArraySegment<short> fwd = new ArraySegment<short>(gauss, 255 - offset, gauss.Length - (255 - offset));
            ArraySegment<short> rev = new ArraySegment<short>(gauss, offset, gauss.Length - offset); // mirror left half of gaussian

            ArraySegment<int> _in = new ArraySegment<int>(v.buf, (v.interp_pos >> 12) + v.buf_pos, v.buf.Length - (v.interp_pos >> 12) + v.buf_pos);
            int _out;
            _out = (fwd.Array[fwd.Offset + 0] * _in.Array[_in.Offset + 0]) >> 11;
            _out += (fwd.Array[fwd.Offset + 256] * _in.Array[_in.Offset + 1]) >> 11;
            _out += (rev.Array[rev.Offset + 256] * _in.Array[_in.Offset + 2]) >> 11;
            _out = (short)_out;
            _out += (rev.Array[rev.Offset + 0] * _in.Array[_in.Offset + 3]) >> 11;

            Clamp16(ref _out);
            _out &= ~1;
            return _out;
        }

        private void run_envelope(Voice v)
        {
            int env = v.env;
            if (v.env_mode == EnvMode.release) // 60%
            {
                if ((env -= 0x8) < 0)
                {
                    env = 0;
                }
                v.env = env;
            }
            else
            {
                int rate;
                int env_data = v.regs.Array[v.regs.Offset + (int)VoiceReg.adsr1];
                if (Convert.ToBoolean(m.t_adsr0 & 0x80)) // 99% ADSR
                {
                    if (v.env_mode >= EnvMode.decay) // 99%
                    {
                        env--;
                        env -= env >> 8;
                        rate = env_data & 0x1F;
                        if (v.env_mode == EnvMode.decay) // 1%
                        {
                            rate = (m.t_adsr0 >> 3 & 0x0E) + 0x10;
                        }
                    }
                    else // env_attack
                    {
                        rate = (m.t_adsr0 & 0x0F) * 2 + 1;
                        env += rate < 31 ? 0x20 : 0x400;
                    }
                }
                else // GAIN
                {
                    int mode;
                    env_data = v.regs.Array[v.regs.Offset + (int)VoiceReg.gain];
                    mode = env_data >> 5;
                    if (mode < 4) // direct
                    {
                        env = env_data * 0x10;
                        rate = 31;
                    }
                    else
                    {
                        rate = env_data & 0x1F;
                        if (mode == 4) // 4: linear decrease
                        {
                            env -= 0x20;
                        }
                        else if (mode < 6) // 5: exponential decrease
                        {
                            env--;
                            env -= env >> 8;
                        }
                        else // 6,7: linear increase
                        {
                            env += 0x20;
                            if (mode > 6 && (uint)v.hidden_env >= 0x600)
                            {
                                env += 0x8 - 0x20; // 7: two-slope linear increase
                            }
                        }
                    }
                }

                // Sustain level
                if ((env >> 8) == (env_data >> 5) && v.env_mode == EnvMode.decay)
                {
                    v.env_mode = EnvMode.sustain;
                }

                v.hidden_env = env;

                // uint cast because linear decrease going negative also triggers this
                if ((uint)env > 0x7FF)
                {
                    env = (env < 0 ? 0 : 0x7FF);
                    if (v.env_mode == EnvMode.attack)
                    {
                        v.env_mode = EnvMode.decay;
                    }
                }

                if (!Convert.ToBoolean(read_counter(rate)))
                {
                    v.env = env; // nothing else is controlled by the counter
                }
            }
        }

        private void decode_brr(Voice v)
        {
            // Arrange the four input nybbles in 0xABCD order for easy decoding
            int nybbles = m.t_brr_byte * 0x100 + m.ram[(v.brr_addr + v.brr_offset + 1) & 0xFFFF];

            int header = m.t_brr_header;

            // Write to next four samples in circular buffer
            ArraySegment<int> pos = new ArraySegment<int>(v.buf, v.buf_pos, v.buf.Length - v.buf_pos);
            ArraySegment<int> end;
            if ((v.buf_pos += 4) >= brr_buf_size)
            {
                v.buf_pos = 0;
            }

            // Decode four samples
            for (end = new ArraySegment<int>(pos.Array, pos.Offset + 4, pos.Count - 4); pos.Offset < end.Offset; pos = new ArraySegment<int>(pos.Array, pos.Offset + 1, pos.Offset - 1), nybbles <<= 4)
            {
                // Extract nybble and sign-extend
                int s = (short)nybbles >> 12;

                // Shift sample based on header
                int shift = header >> 4;
                s = (s << shift) >> 1;
                if (shift >= 0xD) // handle invalid range
                {
                    s = (s >> 25) << 11; // same as: s = (s < 0 ? -0x800 : 0)
                }

                // Apply IIR filter (8 is the most commonly used)
                int filter = header & 0x0C;
                int p1 = pos.Array[pos.Offset + brr_buf_size - 1];
                int p2 = pos.Array[pos.Offset + brr_buf_size - 2] >> 1;
                if (filter >= 8)
                {
                    s += p1;
                    s -= p2;
                    if (filter == 8) // s += p1 * 0.953125 - p2 * 0.46875
                    {
                        s += p2 >> 4;
                        s += (p1 * -3) >> 6;
                    }
                    else // s += p1 * 0.8984375 - p2 * 0.40625
                    {
                        s += (p1 * -13) >> 7;
                        s += (p2 * 3) >> 4;
                    }
                }
                else if (Convert.ToBoolean(filter)) // s += p1 * 0.46875
                {
                    s += p1 >> 1;
                    s += (-p1) >> 5;
                }

                // Adjust and write sample
                Clamp16(ref  s);
                s = (short)(s * 2);
                pos.Array[pos.Offset + brr_buf_size] = pos.Array[pos.Offset + 0] = s; // second copy simplifies wrap-around
            }
        }

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
