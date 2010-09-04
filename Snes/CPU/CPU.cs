using System;
using Nall;
using Snes.Memory;
using Snes.PPU;

namespace Snes.CPU
{
    abstract partial class CPU : CPUCore, IPPUCounter, IProcessor, IMMIO
    {
        public const bool Threaded = true;
        public Processor[] coprocessors;
        public void step(uint clocks) { throw new NotImplementedException(); }
        public void synchronize_smp() { throw new NotImplementedException(); }
        public void synchronize_ppu() { throw new NotImplementedException(); }
        public void synchronize_coprocessor() { throw new NotImplementedException(); }

        public byte port_read(uint2 port) { throw new NotImplementedException(); }
        public void port_write(uint2 port, byte data) { throw new NotImplementedException(); }

        public byte pio() { throw new NotImplementedException(); }
        public bool joylatch() { throw new NotImplementedException(); }
        public override bool interrupt_pending() { throw new NotImplementedException(); }

        public void enter() { throw new NotImplementedException(); }
        public void power() { throw new NotImplementedException(); }
        public void reset() { throw new NotImplementedException(); }

        public CPU() { throw new NotImplementedException(); }

        private Channel[] channel = new Channel[8];
        private Pipe pipe;

        private void dma_add_clocks(uint clocks) { throw new NotImplementedException(); }
        private bool dma_transfer_valid(byte bbus, uint abus) { throw new NotImplementedException(); }
        private bool dma_addr_valid(uint abus) { throw new NotImplementedException(); }
        private byte dma_read(uint abus) { throw new NotImplementedException(); }
        private void dma_write(bool valid, uint addr = 0, byte data = 0) { throw new NotImplementedException(); }
        private void dma_transfer(bool direction, byte bbus, uint abus) { throw new NotImplementedException(); }

        private byte dma_bbus(uint i, uint channel) { throw new NotImplementedException(); }
        private uint dma_addr(uint i) { throw new NotImplementedException(); }
        private uint hdma_addr(uint i) { throw new NotImplementedException(); }
        private uint hdma_iaddr(uint i) { throw new NotImplementedException(); }

        private byte dma_enabled_channels() { throw new NotImplementedException(); }
        private bool hdma_active(uint i) { throw new NotImplementedException(); }
        private bool hdma_active_after(uint i) { throw new NotImplementedException(); }
        private byte hdma_enabled_channels() { throw new NotImplementedException(); }
        private byte hdma_active_channels() { throw new NotImplementedException(); }

        private void dma_run() { throw new NotImplementedException(); }
        private void hdma_update(uint i) { throw new NotImplementedException(); }
        private void hdma_run() { throw new NotImplementedException(); }
        private void hdma_init_reset() { throw new NotImplementedException(); }
        private void hdma_init() { throw new NotImplementedException(); }

        private void dma_power() { throw new NotImplementedException(); }
        private void dma_reset() { throw new NotImplementedException(); }

        public override void op_io() { throw new NotImplementedException(); }
        public override byte op_read(uint addr) { throw new NotImplementedException(); }
        public override void op_write(uint addr, byte data) { throw new NotImplementedException(); }
        private uint speed(uint addr) { throw new NotImplementedException(); }

        private void mmio_power() { throw new NotImplementedException(); }
        private void mmio_reset() { throw new NotImplementedException(); }
        public byte mmio_read(uint addr) { throw new NotImplementedException(); }
        public void mmio_write(uint addr, byte data) { throw new NotImplementedException(); }

        private byte mmio_r2180() { throw new NotImplementedException(); }
        private byte mmio_r4016() { throw new NotImplementedException(); }
        private byte mmio_r4017() { throw new NotImplementedException(); }
        private byte mmio_r4210() { throw new NotImplementedException(); }
        private byte mmio_r4211() { throw new NotImplementedException(); }
        private byte mmio_r4212() { throw new NotImplementedException(); }
        private byte mmio_r4213() { throw new NotImplementedException(); }
        private byte mmio_r4214() { throw new NotImplementedException(); }
        private byte mmio_r4215() { throw new NotImplementedException(); }
        private byte mmio_r4216() { throw new NotImplementedException(); }
        private byte mmio_r4217() { throw new NotImplementedException(); }
        private byte mmio_r4218() { throw new NotImplementedException(); }
        private byte mmio_r4219() { throw new NotImplementedException(); }
        private byte mmio_r421a() { throw new NotImplementedException(); }
        private byte mmio_r421b() { throw new NotImplementedException(); }
        private byte mmio_r421c() { throw new NotImplementedException(); }
        private byte mmio_r421d() { throw new NotImplementedException(); }
        private byte mmio_r421e() { throw new NotImplementedException(); }
        private byte mmio_r421f() { throw new NotImplementedException(); }
        private byte mmio_r43x0(byte i) { throw new NotImplementedException(); }
        private byte mmio_r43x1(byte i) { throw new NotImplementedException(); }
        private byte mmio_r43x2(byte i) { throw new NotImplementedException(); }
        private byte mmio_r43x3(byte i) { throw new NotImplementedException(); }
        private byte mmio_r43x4(byte i) { throw new NotImplementedException(); }
        private byte mmio_r43x5(byte i) { throw new NotImplementedException(); }
        private byte mmio_r43x6(byte i) { throw new NotImplementedException(); }
        private byte mmio_r43x7(byte i) { throw new NotImplementedException(); }
        private byte mmio_r43x8(byte i) { throw new NotImplementedException(); }
        private byte mmio_r43x9(byte i) { throw new NotImplementedException(); }
        private byte mmio_r43xa(byte i) { throw new NotImplementedException(); }
        private byte mmio_r43xb(byte i) { throw new NotImplementedException(); }

        private void mmio_w2180(byte data) { throw new NotImplementedException(); }
        private void mmio_w2181(byte data) { throw new NotImplementedException(); }
        private void mmio_w2182(byte data) { throw new NotImplementedException(); }
        private void mmio_w2183(byte data) { throw new NotImplementedException(); }
        private void mmio_w4016(byte data) { throw new NotImplementedException(); }
        private void mmio_w4200(byte data) { throw new NotImplementedException(); }
        private void mmio_w4201(byte data) { throw new NotImplementedException(); }
        private void mmio_w4202(byte data) { throw new NotImplementedException(); }
        private void mmio_w4203(byte data) { throw new NotImplementedException(); }
        private void mmio_w4204(byte data) { throw new NotImplementedException(); }
        private void mmio_w4205(byte data) { throw new NotImplementedException(); }
        private void mmio_w4206(byte data) { throw new NotImplementedException(); }
        private void mmio_w4207(byte data) { throw new NotImplementedException(); }
        private void mmio_w4208(byte data) { throw new NotImplementedException(); }
        private void mmio_w4209(byte data) { throw new NotImplementedException(); }
        private void mmio_w420a(byte data) { throw new NotImplementedException(); }
        private void mmio_w420b(byte data) { throw new NotImplementedException(); }
        private void mmio_w420c(byte data) { throw new NotImplementedException(); }
        private void mmio_w420d(byte data) { throw new NotImplementedException(); }
        private void mmio_w43x0(byte i, byte data) { throw new NotImplementedException(); }
        private void mmio_w43x1(byte i, byte data) { throw new NotImplementedException(); }
        private void mmio_w43x2(byte i, byte data) { throw new NotImplementedException(); }
        private void mmio_w43x3(byte i, byte data) { throw new NotImplementedException(); }
        private void mmio_w43x4(byte i, byte data) { throw new NotImplementedException(); }
        private void mmio_w43x5(byte i, byte data) { throw new NotImplementedException(); }
        private void mmio_w43x6(byte i, byte data) { throw new NotImplementedException(); }
        private void mmio_w43x7(byte i, byte data) { throw new NotImplementedException(); }
        private void mmio_w43x8(byte i, byte data) { throw new NotImplementedException(); }
        private void mmio_w43x9(byte i, byte data) { throw new NotImplementedException(); }
        private void mmio_w43xa(byte i, byte data) { throw new NotImplementedException(); }
        private void mmio_w43xb(byte i, byte data) { throw new NotImplementedException(); }

        //timing.cpp
        private uint dma_counter() { throw new NotImplementedException(); }

        private void add_clocks(uint clocks) { throw new NotImplementedException(); }
        private void scanline() { throw new NotImplementedException(); }

        private void alu_edge() { throw new NotImplementedException(); }
        private void dma_edge() { throw new NotImplementedException(); }
        public override void last_cycle() { throw new NotImplementedException(); }

        private void timing_power() { throw new NotImplementedException(); }
        private void timing_reset() { throw new NotImplementedException(); }

        //irq.cpp
        private void poll_interrupts() { throw new NotImplementedException(); }
        private void nmitimen_update(byte data) { throw new NotImplementedException(); }
        private bool rdnmi() { throw new NotImplementedException(); }
        private bool timeup() { throw new NotImplementedException(); }

        private bool nmi_test() { throw new NotImplementedException(); }
        private bool irq_test() { throw new NotImplementedException(); }

        //joypad.cpp
        private void run_auto_joypad_poll() { throw new NotImplementedException(); }

        private byte cpu_version;

        private Status status;
        private ALU alu;

        private static void Enter() { throw new NotImplementedException(); }
        private void op_irq() { throw new NotImplementedException(); }

        public Processor Processor
        {
            get { throw new NotImplementedException(); }
        }

        public PPUCounter PPUCounter
        {
            get { throw new NotImplementedException(); }
        }
    }
}
