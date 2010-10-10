using System;
using System.Collections.ObjectModel;
using Nall;

namespace Snes.Fast
{
    partial class CPU : CPUCore, IPPUCounter, IProcessor, IMMIO
    {
        public Collection<IProcessor> coprocessors = new Collection<IProcessor>();
        public void step(uint clocks) { throw new NotImplementedException(); }
        public void synchronize_smp() { throw new NotImplementedException(); }
        public void synchronize_ppu() { throw new NotImplementedException(); }
        public void synchronize_coprocessor() { throw new NotImplementedException(); }

        public byte pio() { throw new NotImplementedException(); }
        public bool joylatch() { throw new NotImplementedException(); }
        public override bool interrupt_pending() { throw new NotImplementedException(); }
        public byte port_read(byte port) { throw new NotImplementedException(); }
        public void port_write(byte port, byte data) { throw new NotImplementedException(); }
        public byte mmio_read(uint addr) { throw new NotImplementedException(); }
        public void mmio_write(uint addr, byte data) { throw new NotImplementedException(); }

        public override void op_io() { throw new NotImplementedException(); }
        public override byte op_read(uint addr) { throw new NotImplementedException(); }
        public override void op_write(uint addr, byte data) { throw new NotImplementedException(); }

        public void enter() { throw new NotImplementedException(); }
        public void power() { throw new NotImplementedException(); }
        public void reset() { throw new NotImplementedException(); }

        public void serialize(Serializer s) { throw new NotImplementedException(); }
        public CPU() { throw new NotImplementedException(); }

        //cpu
        private static void Enter() { throw new NotImplementedException(); }
        private void op_step() { throw new NotImplementedException(); }
        private void op_irq(ushort vector) { throw new NotImplementedException(); }

        //timing
        private enum QueueEvent : uint { DramRefresh, HdmaRun, ControllerLatch, }

        private PriorityQueue queue;
        private void queue_event(uint id) { throw new NotImplementedException(); }
        public override void last_cycle() { throw new NotImplementedException(); }
        private void add_clocks(uint clocks) { throw new NotImplementedException(); }
        private void scanline() { throw new NotImplementedException(); }
        private void run_auto_joypad_poll() { throw new NotImplementedException(); }

        //memory
        private uint speed(uint addr) { throw new NotImplementedException(); }

        //dma
        private bool dma_transfer_valid(byte bbus, uint abus) { throw new NotImplementedException(); }
        private bool dma_addr_valid(uint abus) { throw new NotImplementedException(); }
        private byte dma_read(uint abus) { throw new NotImplementedException(); }
        private void dma_write(bool valid, uint addr, byte data) { throw new NotImplementedException(); }
        private void dma_transfer(bool direction, byte bbus, uint abus) { throw new NotImplementedException(); }
        private byte dma_bbus(uint i, uint index) { throw new NotImplementedException(); }
        private uint dma_addr(uint i) { throw new NotImplementedException(); }
        private uint hdma_addr(uint i) { throw new NotImplementedException(); }
        private uint hdma_iaddr(uint i) { throw new NotImplementedException(); }
        private void dma_run() { throw new NotImplementedException(); }
        private bool hdma_active_after(uint i) { throw new NotImplementedException(); }
        private void hdma_update(uint i) { throw new NotImplementedException(); }
        private void hdma_run() { throw new NotImplementedException(); }
        private void hdma_init() { throw new NotImplementedException(); }
        private void dma_reset() { throw new NotImplementedException(); }

        //registers
        private byte[] port_data = new byte[4];

        Channel[] channel = new Channel[8];

        private Status status = new Status();

        private PPUCounter _ppuCounter = new PPUCounter();
        public PPUCounter PPUCounter
        {
            get
            {
                return _ppuCounter;
            }
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
