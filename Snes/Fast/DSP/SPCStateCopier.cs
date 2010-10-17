﻿#if FAST_DSP
using System;
using System.IO;

namespace Snes
{
    class SPCStateCopier
    {
        private SPCDSP.DSPCopyFunction func;
        private Stream buf;

        public SPCStateCopier(Stream p, SPCDSP.DSPCopyFunction f)
        {
            func = f;
            buf = p;
        }

        public void copy(object state, uint size)
        {
            func(buf, state, size);
        }

        public int copy_int(int state, int size)
        {
            byte[] s = BitConverter.GetBytes((ushort)state);
            func(buf, s, (uint)size);
            return BitConverter.ToUInt16(s, 0);
        }

        public void skip(int count)
        {
            if (count > 0)
            {
                byte[] temp = new byte[64];

                do
                {
                    int n = temp.Length;
                    if (n > count)
                        n = count;
                    count -= n;
                    func(buf, temp, (uint)n);
                }
                while (Convert.ToBoolean(count));
            }
        }

        public void extra()
        {
            int n = 0;
            SPCCopy(sizeof(byte), n);
            skip(n);
        }

        public void SPCCopy(int size, object state)
        {
            state = copy_int(Convert.ToInt32(state), size);
        }
    }
}
#endif
