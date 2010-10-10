using System.Runtime.InteropServices;

namespace Snes.Fast
{
    partial class CPU
    {
        private class Channel
        {
            bool dma_enabled;
            bool hdma_enabled;

            bool direction;
            bool indirect;
            bool unused;
            bool reverse_transfer;
            bool fixed_transfer;
            byte transfer_mode;

            byte dest_addr;
            ushort source_addr;
            byte source_bank;

            [StructLayout(LayoutKind.Explicit)]
            public class Union
            {
                [FieldOffset(0)]
                public ushort transfer_size;
                [FieldOffset(0)]
                public ushort indirect_addr;
            }
            public Union union = new Union();

            byte indirect_bank;
            ushort hdma_addr;
            byte line_counter;
            byte unknown;

            bool hdma_completed;
            bool hdma_do_transfer;
        }
    }
}
