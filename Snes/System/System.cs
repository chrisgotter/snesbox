using System;
using Snes.Chip.BSX;
using Snes.Chip.CX4;
using Snes.Chip.DSP1;
using Snes.Chip.DSP2;
using Snes.Chip.DSP3;
using Snes.Chip.DSP4;
using Snes.Chip.MSU1;
using Snes.Chip.OBC1;
using Snes.Chip.SA1;
using Snes.Chip.SDD1;
using Snes.Chip.Serial;
using Snes.Chip.SPC7110;
using Snes.Chip.SRTC;
using Snes.Chip.ST0010;
using Snes.Chip.ST0011;
using Snes.Chip.ST0018;
using Snes.Chip.SuperFX;
using Snes.Chip.SuperGameBoy;
using Snes.Memory;

namespace Snes.System
{
    class System
    {
        public static System system = new System();

        public enum Region : uint { NTSC = 0, PAL = 1, Autodetect = 2 }
        public enum ExpansionPortDevice : uint { None = 0, BSX = 1 }

        public void run()
        {
            Scheduler.Scheduler.scheduler.sync = Scheduler.Scheduler.SynchronizeMode.None;

            Scheduler.Scheduler.scheduler.enter();
            if (Scheduler.Scheduler.scheduler.exit_reason == Scheduler.Scheduler.ExitReason.FrameEvent)
            {
                Input.Input.input.update();
                Video.Video.video.update();
            }
        }

        public void runtosave()
        {
            if (CPU.CPU.Threaded == true)
            {
                Scheduler.Scheduler.scheduler.sync = Scheduler.Scheduler.SynchronizeMode.CPU;
                runthreadtosave();
            }

            if (SMP.SMP.Threaded == true)
            {
                Scheduler.Scheduler.scheduler.thread = SMP.SMP.smp.Processor.thread;
                runthreadtosave();
            }

            if (PPU.PPU.Threaded == true)
            {
                Scheduler.Scheduler.scheduler.thread = PPU.PPU.ppu.Processor.thread;
                runthreadtosave();
            }

            if (DSP.DSP.Threaded == true)
            {
                Scheduler.Scheduler.scheduler.thread = DSP.DSP.dsp.thread;
                runthreadtosave();
            }

            for (uint i = 0; i < CPU.CPU.cpu.coprocessors.Count; i++)
            {
                Processor chip = CPU.CPU.cpu.coprocessors[(int)i];
                Scheduler.Scheduler.scheduler.thread = chip.thread;
                runthreadtosave();
            }
        }

        public void init(Interface.Interface interface_)
        {
            inter = interface_;

            SuperGameBoy.supergameboy.init();
            SuperFX.superfx.init();
            SA1.sa1.init();
            BSXBase.bsxbase.init();
            BSXCart.bsxcart.init();
            BSXFlash.bsxflash.init();
            SRTC.srtc.init();
            SDD1.sdd1.init();
            SPC7110.spc7110.init();
            CX4.cx4.init();
            DSP1.dsp1.init();
            DSP2.dsp2.init();
            DSP3.dsp3.init();
            DSP4.dsp4.init();
            OBC1.obc1.init();
            ST0010.st0010.init();
            ST0011.st0011.init();
            ST0018.st0018.init();
            MSU1.msu1.init();
            Serial.serial.init();

            Video.Video.video.init();
            Audio.Audio.audio.init();
            Input.Input.input.init();
        }

        public void term()
        {
        }

        public void power()
        {
            region = Configuration.Configuration.config.region;
            expansion = Configuration.Configuration.config.expansion_port;
            if (region == Region.Autodetect)
            {
                region = (Cartridge.Cartridge.cartridge.region == Cartridge.Cartridge.Region.NTSC ? Region.NTSC : Region.PAL);
            }

            cpu_frequency = region == Region.NTSC ? Configuration.Configuration.config.cpu.ntsc_frequency : Configuration.Configuration.config.cpu.pal_frequency;
            apu_frequency = region == Region.NTSC ? Configuration.Configuration.config.smp.ntsc_frequency : Configuration.Configuration.config.smp.pal_frequency;

            Bus.bus.power();
            for (uint i = 0x2100; i <= 0x213f; i++)
            {
                MMIOAccess.mmio.map(i, PPU.PPU.ppu);
            }
            for (uint i = 0x2140; i <= 0x217f; i++)
            {
                MMIOAccess.mmio.map(i, CPU.CPU.cpu);
            }
            for (uint i = 0x2180; i <= 0x2183; i++)
            {
                MMIOAccess.mmio.map(i, CPU.CPU.cpu);
            }
            for (uint i = 0x4016; i <= 0x4017; i++)
            {
                MMIOAccess.mmio.map(i, CPU.CPU.cpu);
            }
            for (uint i = 0x4200; i <= 0x421f; i++)
            {
                MMIOAccess.mmio.map(i, CPU.CPU.cpu);
            }
            for (uint i = 0x4300; i <= 0x437f; i++)
            {
                MMIOAccess.mmio.map(i, CPU.CPU.cpu);
            }

            Audio.Audio.audio.coprocessor_enable(false);
            if (expansion == ExpansionPortDevice.BSX)
            {
                BSXBase.bsxbase.enable();
            }
            if (!ReferenceEquals(MappedRAM.bsxflash.data(), null))
            {
                BSXFlash.bsxflash.enable();
            }
            if (Cartridge.Cartridge.cartridge.mode == Cartridge.Cartridge.Mode.Bsx)
            {
                BSXCart.bsxcart.enable();
            }
            if (Cartridge.Cartridge.cartridge.mode == Cartridge.Cartridge.Mode.SuperGameBoy)
            {
                SuperGameBoy.supergameboy.enable();
            }

            if (Cartridge.Cartridge.cartridge.has_superfx)
            {
                SuperFX.superfx.enable();
            }
            if (Cartridge.Cartridge.cartridge.has_sa1)
            {
                SA1.sa1.enable();
            }
            if (Cartridge.Cartridge.cartridge.has_srtc)
            {
                SRTC.srtc.enable();
            }
            if (Cartridge.Cartridge.cartridge.has_sdd1)
            {
                SDD1.sdd1.enable();
            }
            if (Cartridge.Cartridge.cartridge.has_spc7110)
            {
                SPC7110.spc7110.enable();
            }
            if (Cartridge.Cartridge.cartridge.has_cx4)
            {
                CX4.cx4.enable();
            }
            if (Cartridge.Cartridge.cartridge.has_dsp1)
            {
                DSP1.dsp1.enable();
            }
            if (Cartridge.Cartridge.cartridge.has_dsp2)
            {
                DSP2.dsp2.enable();
            }
            if (Cartridge.Cartridge.cartridge.has_dsp3)
            {
                DSP3.dsp3.enable();
            }
            if (Cartridge.Cartridge.cartridge.has_dsp4)
            {
                DSP4.dsp4.enable();
            }
            if (Cartridge.Cartridge.cartridge.has_obc1)
            {
                OBC1.obc1.enable();
            }
            if (Cartridge.Cartridge.cartridge.has_st0010)
            {
                ST0010.st0010.enable();
            }
            if (Cartridge.Cartridge.cartridge.has_st0011)
            {
                ST0011.st0011.enable();
            }
            if (Cartridge.Cartridge.cartridge.has_st0018)
            {
                ST0018.st0018.enable();
            }
            if (Cartridge.Cartridge.cartridge.has_msu1)
            {
                MSU1.msu1.enable();
            }
            if (Cartridge.Cartridge.cartridge.has_serial)
            {
                Serial.serial.enable();
            }

            CPU.CPU.cpu.power();
            SMP.SMP.smp.power();
            DSP.DSP.dsp.power();
            PPU.PPU.ppu.power();

            if (expansion == ExpansionPortDevice.BSX)
            {
                BSXBase.bsxbase.power();
            }
            if (!ReferenceEquals(MappedRAM.bsxflash.data(), null))
            {
                BSXFlash.bsxflash.power();
            }
            if (Cartridge.Cartridge.cartridge.mode == Cartridge.Cartridge.Mode.Bsx)
            {
                BSXCart.bsxcart.power();
            }
            if (Cartridge.Cartridge.cartridge.mode == Cartridge.Cartridge.Mode.SuperGameBoy)
            {
                SuperGameBoy.supergameboy.power();
            }

            if (Cartridge.Cartridge.cartridge.has_superfx)
            {
                SuperFX.superfx.power();
            }
            if (Cartridge.Cartridge.cartridge.has_sa1)
            {
                SA1.sa1.power();
            }
            if (Cartridge.Cartridge.cartridge.has_srtc)
            {
                SRTC.srtc.power();
            }
            if (Cartridge.Cartridge.cartridge.has_sdd1)
            {
                SDD1.sdd1.power();
            }
            if (Cartridge.Cartridge.cartridge.has_spc7110)
            {
                SPC7110.spc7110.power();
            }
            if (Cartridge.Cartridge.cartridge.has_cx4)
            {
                CX4.cx4.power();
            }
            if (Cartridge.Cartridge.cartridge.has_dsp1)
            {
                DSP1.dsp1.power();
            }
            if (Cartridge.Cartridge.cartridge.has_dsp2)
            {
                DSP2.dsp2.power();
            }
            if (Cartridge.Cartridge.cartridge.has_dsp3)
            {
                DSP3.dsp3.power();
            }
            if (Cartridge.Cartridge.cartridge.has_dsp4)
            {
                DSP4.dsp4.power();
            }
            if (Cartridge.Cartridge.cartridge.has_obc1)
            {
                OBC1.obc1.power();
            }
            if (Cartridge.Cartridge.cartridge.has_st0010)
            {
                ST0010.st0010.power();
            }
            if (Cartridge.Cartridge.cartridge.has_st0011)
            {
                ST0011.st0011.power();
            }
            if (Cartridge.Cartridge.cartridge.has_st0018)
            {
                ST0018.st0018.power();
            }
            if (Cartridge.Cartridge.cartridge.has_msu1)
            {
                MSU1.msu1.power();
            }
            if (Cartridge.Cartridge.cartridge.has_serial)
            {
                Serial.serial.power();
            }

            if (Cartridge.Cartridge.cartridge.mode == Cartridge.Cartridge.Mode.SuperGameBoy)
            {
                CPU.CPU.cpu.coprocessors.Add(SuperGameBoy.supergameboy.Coprocessor);
            }
            if (Cartridge.Cartridge.cartridge.has_superfx)
            {
                CPU.CPU.cpu.coprocessors.Add(SuperFX.superfx);
            }
            if (Cartridge.Cartridge.cartridge.has_sa1)
            {
                CPU.CPU.cpu.coprocessors.Add(SA1.sa1.Coprocessor);
            }
            if (Cartridge.Cartridge.cartridge.has_msu1)
            {
                CPU.CPU.cpu.coprocessors.Add(MSU1.msu1);
            }
            if (Cartridge.Cartridge.cartridge.has_serial)
            {
                CPU.CPU.cpu.coprocessors.Add(Serial.serial);
            }

            Scheduler.Scheduler.scheduler.init();

            Input.Input.input.port_set_device(Convert.ToBoolean(0), Configuration.Configuration.config.controller_port1);
            Input.Input.input.port_set_device(Convert.ToBoolean(1), Configuration.Configuration.config.controller_port2);
            Input.Input.input.update();
        }

        public void reset()
        {
            Bus.bus.reset();
            CPU.CPU.cpu.reset();
            SMP.SMP.smp.reset();
            DSP.DSP.dsp.reset();
            PPU.PPU.ppu.reset();

            if (expansion == ExpansionPortDevice.BSX)
            {
                BSXBase.bsxbase.reset();
            }
            if (!ReferenceEquals(MappedRAM.bsxflash.data(), null))
            {
                BSXFlash.bsxflash.reset();
            }
            if (Cartridge.Cartridge.cartridge.mode == Cartridge.Cartridge.Mode.Bsx)
            {
                BSXCart.bsxcart.reset();
            }
            if (Cartridge.Cartridge.cartridge.mode == Cartridge.Cartridge.Mode.SuperGameBoy)
            {
                SuperGameBoy.supergameboy.reset();
            }

            if (Cartridge.Cartridge.cartridge.has_superfx)
            {
                SuperFX.superfx.reset();
            }
            if (Cartridge.Cartridge.cartridge.has_sa1)
            {
                SA1.sa1.reset();
            }
            if (Cartridge.Cartridge.cartridge.has_srtc)
            {
                SRTC.srtc.reset();
            }
            if (Cartridge.Cartridge.cartridge.has_sdd1)
            {
                SDD1.sdd1.reset();
            }
            if (Cartridge.Cartridge.cartridge.has_spc7110)
            {
                SPC7110.spc7110.reset();
            }
            if (Cartridge.Cartridge.cartridge.has_cx4)
            {
                CX4.cx4.reset();
            }
            if (Cartridge.Cartridge.cartridge.has_dsp1)
            {
                DSP1.dsp1.reset();
            }
            if (Cartridge.Cartridge.cartridge.has_dsp2)
            {
                DSP2.dsp2.reset();
            }
            if (Cartridge.Cartridge.cartridge.has_dsp3)
            {
                DSP3.dsp3.reset();
            }
            if (Cartridge.Cartridge.cartridge.has_dsp4)
            {
                DSP4.dsp4.reset();
            }
            if (Cartridge.Cartridge.cartridge.has_obc1)
            {
                OBC1.obc1.reset();
            }
            if (Cartridge.Cartridge.cartridge.has_st0010)
            {
                ST0010.st0010.reset();
            }
            if (Cartridge.Cartridge.cartridge.has_st0011)
            {
                ST0011.st0011.reset();
            }
            if (Cartridge.Cartridge.cartridge.has_st0018)
            {
                ST0018.st0018.reset();
            }
            if (Cartridge.Cartridge.cartridge.has_msu1)
            {
                MSU1.msu1.reset();
            }
            if (Cartridge.Cartridge.cartridge.has_serial)
            {
                Serial.serial.reset();
            }

            if (Cartridge.Cartridge.cartridge.mode == Cartridge.Cartridge.Mode.SuperGameBoy)
            {
                CPU.CPU.cpu.coprocessors.Add(SuperGameBoy.supergameboy.Coprocessor);
            }
            if (Cartridge.Cartridge.cartridge.has_superfx)
            {
                CPU.CPU.cpu.coprocessors.Add(SuperFX.superfx);
            }
            if (Cartridge.Cartridge.cartridge.has_sa1)
            {
                CPU.CPU.cpu.coprocessors.Add(SA1.sa1.Coprocessor);
            }
            if (Cartridge.Cartridge.cartridge.has_msu1)
            {
                CPU.CPU.cpu.coprocessors.Add(MSU1.msu1);
            }
            if (Cartridge.Cartridge.cartridge.has_serial)
            {
                CPU.CPU.cpu.coprocessors.Add(Serial.serial);
            }

            Scheduler.Scheduler.scheduler.init();

            Input.Input.input.port_set_device(Convert.ToBoolean(0), Configuration.Configuration.config.controller_port1);
            Input.Input.input.port_set_device(Convert.ToBoolean(1), Configuration.Configuration.config.controller_port2);
            Input.Input.input.update();

        }

        public void unload()
        {
            if (Cartridge.Cartridge.cartridge.mode == Cartridge.Cartridge.Mode.SuperGameBoy)
            {
                SuperGameBoy.supergameboy.unload();
            }
        }

        public void frame()
        {
        }

        public void scanline()
        {
            Video.Video.video.scanline();
            if (CPU.CPU.cpu.PPUCounter.vcounter() == 241)
            {
                Scheduler.Scheduler.scheduler.exit(Scheduler.Scheduler.ExitReason.FrameEvent);
            }
        }

        //return *active* system information (settings are cached upon power-on)
        public Region region { get; private set; }
        public ExpansionPortDevice expansion { get; private set; }
        public uint cpu_frequency { get; private set; }
        public uint apu_frequency { get; private set; }

        public System()
        {
            region = Region.Autodetect;
            expansion = ExpansionPortDevice.None;
        }

        private Interface.Interface inter;
        public Interface.Interface Interface
        {
            get { return inter; }
        }

        private void runthreadtosave()
        {
            while (true)
            {
                Scheduler.Scheduler.scheduler.enter();
                if (Scheduler.Scheduler.scheduler.exit_reason == Scheduler.Scheduler.ExitReason.SynchronizeEvent)
                {
                    break;
                }
                if (Scheduler.Scheduler.scheduler.exit_reason == Scheduler.Scheduler.ExitReason.FrameEvent)
                {
                    Input.Input.input.update();
                    Video.Video.video.update();
                }
            }
        }
    }
}
