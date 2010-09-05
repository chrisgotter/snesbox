using System;

namespace Snes.Chip.ST0018
{
    partial class ST0018
    {
        public void init() { throw new NotImplementedException(); }
        public void enable() { throw new NotImplementedException(); }
        public void power() { throw new NotImplementedException(); }
        public void reset() { throw new NotImplementedException(); }

        public byte mmio_read(uint addr) { throw new NotImplementedException(); }
        public void mmio_write(uint addr, byte data) { throw new NotImplementedException(); }

        public enum mode_t { Waiting, BoardUpload }
        public regs_t regs;

        public enum PieceID
        {
            Pawn = 0x00,  //foot soldier
            Lance = 0x04,  //incense chariot
            Knight = 0x08,  //cassia horse
            Silver = 0x0c,  //silver general
            Gold = 0x10,  //gold general
            Rook = 0x14,  //flying chariot
            Bishop = 0x18,  //angle mover
            King = 0x1c,  //king
        }

        public enum PieceFlag
        {
            PlayerA = 0x20,
            PlayerB = 0x40,
        }

        public byte[] board = new byte[9 * 9 + 16];

        private void op_board_upload() { throw new NotImplementedException(); }
        private void op_board_upload(byte data) { throw new NotImplementedException(); }
        private void op_b2() { throw new NotImplementedException(); }
        private void op_b3() { throw new NotImplementedException(); }
        private void op_b4() { throw new NotImplementedException(); }
        private void op_b5() { throw new NotImplementedException(); }
        private void op_query_chip() { throw new NotImplementedException(); }
    }
}
