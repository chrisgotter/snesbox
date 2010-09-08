using Nall;

namespace Snes
{
    partial class CPU
    {
        private class Channel
        {
            //$420b
            bool dma_enabled;

            //$420c
            bool hdma_enabled;

            //$43x0
            bool direction;
            bool indirect;
            bool unused;
            bool reverse_transfer;
            bool fixed_transfer;
            uint3 transfer_mode;

            //$43x1
            byte dest_addr;

            //$43x2-$43x3
            ushort source_addr;

            //$43x4
            byte source_bank;

            //$43x5-$43x6
            //union {
            //ushort transfer_size;
            //ushort indirect_addr;
            //}

            //$43x7
            private byte indirect_bank;

            //$43x8-$43x9
            private ushort hdma_addr;

            //$43xa
            private byte line_counter;

            //$43xb/$43xf
            private byte unknown;

            //internal state
            bool hdma_completed;
            bool hdma_do_transfer;
        }
    }
}
