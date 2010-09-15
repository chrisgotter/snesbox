﻿using System;
using Nall;

namespace Snes
{
    partial class SMP : SMPCore, IProcessor
    {
        public static SMP smp = new SMP();

        public const bool Threaded = true;

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
            if (status.ram_writable && !status.ram_disabled)
            {
                StaticRAM.apuram[addr] = data;
            }
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

        private void op_buswrite(ushort addr, byte data)
        {
            if ((addr & 0xfff0) == 0x00f0)
            {  //$00f0-00ff
                switch (addr)
                {
                    case 0xf0:
                        {  //TEST
                            if (regs.p.p)
                            {
                                break;  //writes only valid when P flag is clear
                            }

                            status.clock_speed = (byte)((data >> 6) & 3);
                            status.timer_speed = (byte)((data >> 4) & 3);
                            status.timers_enabled = Convert.ToBoolean(data & 0x08);
                            status.ram_disabled = Convert.ToBoolean(data & 0x04);
                            status.ram_writable = Convert.ToBoolean(data & 0x02);
                            status.timers_disabled = Convert.ToBoolean(data & 0x01);

                            status.timer_step = (uint)((1 << status.clock_speed) + (2 << status.timer_speed));

                            t0.sync_stage1();
                            t1.sync_stage1();
                            t2.sync_stage1();
                        } break;
                    case 0xf1:
                        {  //CONTROL
                            status.iplrom_enabled = Convert.ToBoolean(data & 0x80);

                            if (Convert.ToBoolean(data & 0x30))
                            {
                                //one-time clearing of APU port read registers,
                                //emulated by simulating CPU writes of 0x00
                                synchronize_cpu();
                                if (Convert.ToBoolean(data & 0x20))
                                {
                                    CPU.cpu.port_write(new uint2(2), 0x00);
                                    CPU.cpu.port_write(new uint2(3), 0x00);
                                }
                                if (Convert.ToBoolean(data & 0x10))
                                {
                                    CPU.cpu.port_write(new uint2(0), 0x00);
                                    CPU.cpu.port_write(new uint2(1), 0x00);
                                }
                            }

                            //0->1 transistion resets timers
                            if (t2.enabled == false && Convert.ToBoolean(data & 0x04))
                            {
                                t2.stage2_ticks = 0;
                                t2.stage3_ticks = 0;
                            }
                            t2.enabled = Convert.ToBoolean(data & 0x04);

                            if (t1.enabled == false && Convert.ToBoolean(data & 0x02))
                            {
                                t1.stage2_ticks = 0;
                                t1.stage3_ticks = 0;
                            }
                            t1.enabled = Convert.ToBoolean(data & 0x02);

                            if (t0.enabled == false && Convert.ToBoolean(data & 0x01))
                            {
                                t0.stage2_ticks = 0;
                                t0.stage3_ticks = 0;
                            }
                            t0.enabled = Convert.ToBoolean(data & 0x01);
                        } break;
                    case 0xf2:
                        {  //DSPADDR
                            status.dsp_addr = data;
                        } break;
                    case 0xf3:
                        {  //DSPDATA
                            //0x80-0xff are read-only mirrors of 0x00-0x7f
                            if (!Convert.ToBoolean(status.dsp_addr & 0x80))
                            {
                                DSP.dsp.write((byte)(status.dsp_addr & 0x7f), data);
                            }
                        } break;
                    case 0xf4:    //CPUIO0
                    case 0xf5:    //CPUIO1
                    case 0xf6:    //CPUIO2
                    case 0xf7:
                        {  //CPUIO3
                            synchronize_cpu();
                            port_write(new uint2(addr), data);
                        } break;
                    case 0xf8:
                        {  //RAM0
                            status.ram0 = data;
                        } break;
                    case 0xf9:
                        {  //RAM1
                            status.ram1 = data;
                        } break;
                    case 0xfa:
                        {  //T0TARGET
                            t0.target = data;
                        } break;
                    case 0xfb:
                        {  //T1TARGET
                            t1.target = data;
                        } break;
                    case 0xfc:
                        {  //T2TARGET
                            t2.target = data;
                        } break;
                    case 0xfd:    //T0OUT
                    case 0xfe:    //T1OUT
                    case 0xff:
                        {  //T2OUT -- read-only registers
                        }
                        break;
                }
            }

            //all writes, even to MMIO registers, appear on bus
            ram_write(addr, data);
        }

        public override void op_io()
        {
            add_clocks(24);
            cycle_edge();
        }

        public override byte op_read(ushort addr)
        {
            add_clocks(12);
            byte r = op_busread(addr);
            add_clocks(12);
            cycle_edge();
            return r;
        }

        public override void op_write(ushort addr, byte data)
        {
            add_clocks(24);
            op_buswrite(addr, data);
            cycle_edge();
        }

        private sSMPTimer t0 = new sSMPTimer(192);
        private sSMPTimer t1 = new sSMPTimer(192);
        private sSMPTimer t2 = new sSMPTimer(24);

        private void add_clocks(uint clocks)
        {
            step(clocks);
            synchronize_dsp();

            //forcefully sync S-SMP to S-CPU in case chips are not communicating
            //sync if S-SMP is more than 24 samples ahead of S-CPU
            if (Processor.clock > +(768 * 24 * (long)24000000))
            {
                synchronize_cpu();
            }
        }

        private void cycle_edge()
        {
            t0.tick();
            t1.tick();
            t2.tick();

            //TEST register S-SMP speed control
            //24 clocks have already been added for this cycle at this point
            switch (status.clock_speed)
            {
                case 0:
                    break;                       //100% speed
                case 1:
                    add_clocks(24);
                    break;       // 50% speed
                case 2:
                    while (true)
                    {
                        add_clocks(24);  //  0% speed -- locks S-SMP
                    }
                case 3:
                    add_clocks(24 * 9);
                    break;   // 10% speed
            }
        }

        private static readonly byte[] iplrom = new byte[64];
        public static byte[] Iplrom
        {
            get { return SMP.iplrom; }
        }


        private Status status = new Status();

        private static void Enter()
        {
            SMP.smp.enter();
        }

        private void op_step()
        {
            this.opcode_table[op_readpc()].Invoke();
        }

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
