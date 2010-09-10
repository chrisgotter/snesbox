using System;
using Nall;

namespace Snes
{
    partial class SMP : SMPCore, IProcessor
    {
        public static SMP smp = new SMP();

        public static readonly bool Threaded = true;

        public void step(uint clocks)
        {
            Processor.clock += (long)(clocks * (ulong)CPU.cpu.Processor.frequency);
            DSP.dsp.clock -= clocks;
        }

        public void synchronize_cpu()
        {
            if (CPU.Threaded == true)
            {
                if (Processor.clock >= 0 && Scheduler.scheduler.sync != Scheduler.SynchronizeMode.All)
                {
                    Libco.Switch(CPU.cpu.Processor.thread);
                }
            }
            else
            {
                while (Processor.clock >= 0)
                {
                    CPU.cpu.enter();
                }
            }
        }

        public void synchronize_dsp()
        {
            if (DSP.Threaded == true)
            {
                if (DSP.dsp.clock < 0 && Scheduler.scheduler.sync != Scheduler.SynchronizeMode.All)
                {
                    Libco.Switch(DSP.dsp.thread);
                }
            }
            else
            {
                while (DSP.dsp.clock < 0)
                {
                    DSP.dsp.enter();
                }
            }
        }

        public byte port_read(uint2 port)
        {
            return StaticRAM.apuram[0xf4 + (uint)port];
        }

        public void port_write(uint2 port, byte data)
        {
            StaticRAM.apuram[0xf4 + (uint)port] = data;
        }

        public void enter()
        {
            while (true)
            {
                if (Scheduler.scheduler.sync == Scheduler.SynchronizeMode.All)
                {
                    Scheduler.scheduler.exit(Scheduler.ExitReason.SynchronizeEvent);
                }

                op_step();
            }
        }

        public void power()
        {   //targets not initialized/changed upon reset
            t0.target = 0;
            t1.target = 0;
            t2.target = 0;

            reset();
        }

        public void reset()
        {
            Processor.create(Enter, System.system.apu_frequency);

            regs.pc = 0xffc0;
            regs.a[0] = 0x00;
            regs.x[0] = 0x00;
            regs.y[0] = 0x00;
            regs.sp[0] = 0xef;
            regs.p.Assign(0x02);

            for (uint i = 0; i < StaticRAM.apuram.size(); i++)
            {
                StaticRAM.apuram.write(i, 0x00);
            }

            status.clock_counter = 0;
            status.dsp_counter = 0;
            status.timer_step = 3;

            //$00f0
            status.clock_speed = 0;
            status.timer_speed = 0;
            status.timers_enabled = true;
            status.ram_disabled = false;
            status.ram_writable = true;
            status.timers_disabled = false;

            //$00f1
            status.iplrom_enabled = true;

            //$00f2
            status.dsp_addr = 0x00;

            //$00f8,$00f9
            status.ram0 = 0x00;
            status.ram1 = 0x00;

            t0.stage0_ticks = 0;
            t1.stage0_ticks = 0;
            t2.stage0_ticks = 0;

            t0.stage1_ticks = 0;
            t1.stage1_ticks = 0;
            t2.stage1_ticks = 0;

            t0.stage2_ticks = 0;
            t1.stage2_ticks = 0;
            t2.stage2_ticks = 0;

            t0.stage3_ticks = 0;
            t1.stage3_ticks = 0;
            t2.stage3_ticks = 0;

            t0.current_line = Convert.ToBoolean(0);
            t1.current_line = Convert.ToBoolean(0);
            t2.current_line = Convert.ToBoolean(0);

            t0.enabled = false;
            t1.enabled = false;
            t2.enabled = false;
        }

        public SMP() { }

        private byte ram_read(ushort addr)
        {
            if (addr >= 0xffc0 && status.iplrom_enabled)
            {
                return iplrom[addr & 0x3f];
            }
            if (status.ram_disabled)
            {
                return 0x5a;  //0xff on mini-SNES
            }
            return StaticRAM.apuram[addr];
        }

        private void ram_write(ushort addr, byte data)
        {   //writes to $ffc0-$ffff always go to apuram, even if the iplrom is enabled
            if (status.ram_writable && !status.ram_disabled) StaticRAM.apuram[addr] = data;
        }

        private byte op_busread(ushort addr)
        {
            byte r = default(byte);
            if ((addr & 0xfff0) == 0x00f0)
            {  //00f0-00ff
                switch (addr)
                {
                    case 0xf0:
                        {  //TEST -- write-only register
                            r = 0x00;
                        }
                        break;
                    case 0xf1:
                        {  //CONTROL -- write-only register
                            r = 0x00;
                        }
                        break;
                    case 0xf2:
                        {  //DSPADDR
                            r = status.dsp_addr;
                        }
                        break;
                    case 0xf3:
                        {  //DSPDATA
                            //0x80-0xff are read-only mirrors of 0x00-0x7f
                            r = DSP.dsp.read((byte)(status.dsp_addr & 0x7f));
                        }
                        break;
                    case 0xf4:    //CPUIO0
                    case 0xf5:    //CPUIO1
                    case 0xf6:    //CPUIO2
                    case 0xf7:
                        {  //CPUIO3
                            synchronize_cpu();
                            r = CPU.cpu.port_read(new uint2(addr));
                        }
                        break;
                    case 0xf8:
                        {  //RAM0
                            r = status.ram0;
                        } break;
                    case 0xf9:
                        {  //RAM1
                            r = status.ram1;
                        }
                        break;
                    case 0xfa:    //T0TARGET
                    case 0xfb:    //T1TARGET
                    case 0xfc:
                        {  //T2TARGET -- write-only registers
                            r = 0x00;
                        }
                        break;
                    case 0xfd:
                        {  //T0OUT -- 4-bit counter value
                            r = (byte)(t0.stage3_ticks & 15);
                            t0.stage3_ticks = 0;
                        }
                        break;
                    case 0xfe:
                        {  //T1OUT -- 4-bit counter value
                            r = (byte)(t1.stage3_ticks & 15);
                            t1.stage3_ticks = 0;
                        }
                        break;
                    case 0xff:
                        {  //T2OUT -- 4-bit counter value
                            r = (byte)(t2.stage3_ticks & 15);
                            t2.stage3_ticks = 0;
                        }
                        break;
                }
            }
            else
            {
                r = ram_read(addr);
            }

            return r;
        }

        private void op_buswrite(ushort addr, byte data) { throw new NotImplementedException(); }

        public override void op_io() { throw new NotImplementedException(); }

        public override byte op_read(ushort addr) { throw new NotImplementedException(); }

        public override void op_write(ushort addr, byte data) { throw new NotImplementedException(); }

        private sSMPTimer t0 = new sSMPTimer(192);
        private sSMPTimer t1 = new sSMPTimer(192);
        private sSMPTimer t2 = new sSMPTimer(24);

        private void add_clocks(uint clocks) { throw new NotImplementedException(); }

        private void cycle_edge() { throw new NotImplementedException(); }

        private static readonly byte[] iplrom = new byte[64];

        private Status status;

        private static void Enter() { throw new NotImplementedException(); }

        private void op_step() { throw new NotImplementedException(); }

        public Processor Processor
        {
            get { throw new NotImplementedException(); }
        }
    }
}
