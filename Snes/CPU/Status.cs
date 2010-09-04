using Nall;

namespace Snes.CPU
{
    partial class CPU
    {
        private class Status
        {
            bool interrupt_pending;
            ushort interrupt_vector;

            uint clock_count;
            uint line_clocks;

            //timing
            bool irq_lock;

            uint dram_refresh_position;
            bool dram_refreshed;

            uint hdma_init_position;
            bool hdma_init_triggered;

            uint hdma_position;
            bool hdma_triggered;

            bool nmi_valid;
            bool nmi_line;
            bool nmi_transition;
            bool nmi_pending;
            bool nmi_hold;

            bool irq_valid;
            bool irq_line;
            bool irq_transition;
            bool irq_pending;
            bool irq_hold;

            bool reset_pending;

            //DMA
            bool dma_active;
            uint dma_counter;
            uint dma_clocks;
            bool dma_pending;
            bool hdma_pending;
            bool hdma_mode;  //0 = init, 1 = run

            //MMIO
            //$2140-217f
            byte[] port = new byte[4];

            //$2181-$2183
            uint17 wram_addr;

            //$4016-$4017
            bool joypad_strobe_latch;
            uint joypad1_bits;
            uint joypad2_bits;

            //$4200
            bool nmi_enabled;
            bool hirq_enabled, virq_enabled;
            bool auto_joypad_poll;

            //$4201
            byte pio;

            //$4202-$4203
            byte wrmpya;
            byte wrmpyb;

            //$4204-$4206
            ushort wrdiva;
            byte wrdivb;

            //$4207-$420a
            uint10 hirq_pos;
            uint10 virq_pos;

            //$420d
            uint rom_speed;

            //$4214-$4217
            ushort rddiv;
            ushort rdmpy;

            //$4218-$421f
            byte joy1l, joy1h;
            byte joy2l, joy2h;
            byte joy3l, joy3h;
            byte joy4l, joy4h;
        }
    }
}
