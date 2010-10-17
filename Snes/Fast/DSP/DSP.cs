#if FAST_DSP
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Nall;

namespace Snes
{
    class DSP : IProcessor
    {
        public static DSP dsp = new DSP();

        public void step(uint clocks)
        {
            Processor.clock += clocks;
        }

        public void synchronize_smp()
        {
#if THREADED
            if (Processor.clock >= 0 && Scheduler.scheduler.sync != Scheduler.SynchronizeMode.All)
            {
                Libco.Switch(SMP.smp.Processor.thread);
            }
#else
            while (Processor.clock >= 0)
            {
                SMP.smp.enter();
            }
#endif
        }

        public byte read(byte addr)
        {
            return (byte)spc_dsp.read(addr);
        }

        public void write(byte addr, byte data)
        {
            spc_dsp.write(addr, data);
        }

        public void enter()
        {
            spc_dsp.run(1);
            step(24);

            int count = spc_dsp.sample_count();
            if (count > 0)
            {
                for (uint n = 0; n < count; n += 2)
                {
                    Audio.audio.sample(samplebuffer[n + 0], samplebuffer[n + 1]);
                }
                spc_dsp.set_output(samplebuffer, 8192);
            }
        }

        public void power()
        {
            spc_dsp.init(StaticRAM.apuram.data());
            spc_dsp.reset();
            spc_dsp.set_output(samplebuffer, 8192);
        }

        public void reset()
        {
            spc_dsp.soft_reset();
            spc_dsp.set_output(samplebuffer, 8192);
        }

        public static void dsp_state_save(Stream _out, object _in, uint size)
        {
            new BinaryFormatter().Serialize(_out, _in);
        }

        public static void dsp_state_load(Stream _in, object _out, uint size)
        {
            _out = new BinaryFormatter().Deserialize(_in);
        }

        public void serialize(Serializer s)
        {
            Processor.serialize(s);
            s.array(samplebuffer, "samplebuffer");

            byte[] state = new byte[SPCDSP.state_size];
            MemoryStream p = new MemoryStream(state);
            if (s.mode() == Serializer.Mode.Save)
            {
                spc_dsp.copy_state(p, dsp_state_save);
                s.array(state, "state");
            }
            else if (s.mode() == Serializer.Mode.Load)
            {
                s.array(state, "state");
                spc_dsp.copy_state(p, dsp_state_load);
            }
            else
            {
                s.array(state, "state");
            }
        }

        public bool property(uint id, string name, string value)
        {
            return false;
        }

        private SPCDSP spc_dsp = new SPCDSP();
        private short[] samplebuffer = new short[8192];

        private Processor _processor = new Processor();
        public Processor Processor
        {
            get
            {
                return _processor;
            }
        }
    }
}
#endif
