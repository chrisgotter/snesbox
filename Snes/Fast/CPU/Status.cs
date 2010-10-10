
namespace Snes.Fast
{
    partial class CPU
    {
        private class Status
        {
            bool nmi_valid;
            bool nmi_line;
            bool nmi_transition;
            bool nmi_pending;

            bool irq_valid;
            bool irq_line;
            bool irq_transition;
            bool irq_pending;

            bool irq_lock;
            bool hdma_pending;

            uint wram_addr;

            bool joypad_strobe_latch;

            bool nmi_enabled;
            bool virq_enabled;
            bool hirq_enabled;
            bool auto_joypad_poll_enabled;

            byte pio;

            byte wrmpya;
            byte wrmpyb;
            ushort wrdiva;
            byte wrdivb;

            ushort htime;
            ushort vtime;

            uint rom_speed;

            ushort rddiv;
            ushort rdmpy;

            byte joy1l, joy1h;
            byte joy2l, joy2h;
            byte joy3l, joy3h;
            byte joy4l, joy4h;
        }
    }
}
