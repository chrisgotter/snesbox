using System;

namespace Snes
{
    public delegate byte SingleByteFunction(byte x);
    public delegate byte DoubleByteFunction(byte x, byte y);
    public delegate ushort DoubleUShortFunction(ushort x, ushort y);

    abstract partial class SMPCore
    {
        public byte op_readpc() { throw new NotImplementedException(); }
        public byte op_readstack() { throw new NotImplementedException(); }
        public void op_writestack(byte data) { throw new NotImplementedException(); }
        public byte op_readaddr(ushort addr) { throw new NotImplementedException(); }
        public void op_writeaddr(ushort addr, byte data) { throw new NotImplementedException(); }
        public byte op_readdp(byte addr) { throw new NotImplementedException(); }
        public void op_writedp(byte addr, byte data) { throw new NotImplementedException(); }

        public void disassemble_opcode(byte[] output, ushort addr) { throw new NotImplementedException(); }
        public byte disassemble_read(ushort addr) { throw new NotImplementedException(); }
        public ushort relb(byte offset, int op_len) { throw new NotImplementedException(); }

        public Regs regs;
        public ushort dp, sp, rd, wr, bit, ya;

        public abstract void op_io();
        public abstract byte op_read(ushort addr);
        public abstract void op_write(ushort addr, byte data);

        public byte op_adc(byte x, byte y) { throw new NotImplementedException(); }
        public ushort op_addw(ushort x, ushort y) { throw new NotImplementedException(); }
        public byte op_and(byte x, byte y) { throw new NotImplementedException(); }
        public byte op_cmp(byte x, byte y) { throw new NotImplementedException(); }
        public ushort op_cmpw(ushort x, ushort y) { throw new NotImplementedException(); }
        public byte op_eor(byte x, byte y) { throw new NotImplementedException(); }
        public byte op_inc(byte x) { throw new NotImplementedException(); }
        public byte op_dec(byte x) { throw new NotImplementedException(); }
        public byte op_or(byte x, byte y) { throw new NotImplementedException(); }
        public byte op_sbc(byte x, byte y) { throw new NotImplementedException(); }
        public ushort op_subw(ushort x, ushort y) { throw new NotImplementedException(); }
        public byte op_asl(byte x) { throw new NotImplementedException(); }
        public byte op_lsr(byte x) { throw new NotImplementedException(); }
        public byte op_rol(byte x) { throw new NotImplementedException(); }
        public byte op_ror(byte x) { throw new NotImplementedException(); }

        public void op_mov_reg_reg(int to, int from) { throw new NotImplementedException(); }
        public void op_mov_sp_x() { throw new NotImplementedException(); }
        public void op_mov_reg_const(int n) { throw new NotImplementedException(); }
        public void op_mov_a_ix() { throw new NotImplementedException(); }
        public void op_mov_a_ixinc() { throw new NotImplementedException(); }
        public void op_mov_reg_dp(int n) { throw new NotImplementedException(); }
        public void op_mov_reg_dpr(int n, int i) { throw new NotImplementedException(); }
        public void op_mov_reg_addr(int n) { throw new NotImplementedException(); }
        public void op_mov_a_addrr(int n) { throw new NotImplementedException(); }
        public void op_mov_a_idpx() { throw new NotImplementedException(); }
        public void op_mov_a_idpy() { throw new NotImplementedException(); }
        public void op_mov_dp_dp() { throw new NotImplementedException(); }
        public void op_mov_dp_const() { throw new NotImplementedException(); }
        public void op_mov_ix_a() { throw new NotImplementedException(); }
        public void op_mov_ixinc_a() { throw new NotImplementedException(); }
        public void op_mov_dp_reg(int n) { throw new NotImplementedException(); }
        public void op_mov_dpr_reg(int n, int i) { throw new NotImplementedException(); }
        public void op_mov_addr_reg(int n) { throw new NotImplementedException(); }
        public void op_mov_addrr_a(int n) { throw new NotImplementedException(); }
        public void op_mov_idpx_a() { throw new NotImplementedException(); }
        public void op_mov_idpy_a() { throw new NotImplementedException(); }
        public void op_movw_ya_dp() { throw new NotImplementedException(); }
        public void op_movw_dp_ya() { throw new NotImplementedException(); }
        public void op_mov1_c_bit() { throw new NotImplementedException(); }
        public void op_mov1_bit_c() { throw new NotImplementedException(); }

        public void op_bra() { throw new NotImplementedException(); }
        public void op_branch(int flag, int value) { throw new NotImplementedException(); }
        public void op_bitbranch(int mask, int value) { throw new NotImplementedException(); }
        public void op_cbne_dp() { throw new NotImplementedException(); }
        public void op_cbne_dpx() { throw new NotImplementedException(); }
        public void op_dbnz_dp() { throw new NotImplementedException(); }
        public void op_dbnz_y() { throw new NotImplementedException(); }
        public void op_jmp_addr() { throw new NotImplementedException(); }
        public void op_jmp_iaddrx() { throw new NotImplementedException(); }
        public void op_call() { throw new NotImplementedException(); }
        public void op_pcall() { throw new NotImplementedException(); }
        public void op_tcall(int n) { throw new NotImplementedException(); }
        public void op_brk() { throw new NotImplementedException(); }
        public void op_ret() { throw new NotImplementedException(); }
        public void op_reti() { throw new NotImplementedException(); }

        public void op_read_reg_const(DoubleByteFunction op, int n) { throw new NotImplementedException(); }
        public void op_read_a_ix(DoubleByteFunction op) { throw new NotImplementedException(); }
        public void op_read_reg_dp(DoubleByteFunction op, int n) { throw new NotImplementedException(); }
        public void op_read_a_dpx() { throw new NotImplementedException(); }
        public void op_read_reg_addr(DoubleByteFunction op, int n) { throw new NotImplementedException(); }
        public void op_read_a_addrr(DoubleByteFunction op, int n) { throw new NotImplementedException(); }
        public void op_read_a_idpx() { throw new NotImplementedException(); }
        public void op_read_a_idpy() { throw new NotImplementedException(); }
        public void op_read_ix_iy() { throw new NotImplementedException(); }
        public void op_read_dp_dp() { throw new NotImplementedException(); }
        public void op_read_dp_const() { throw new NotImplementedException(); }
        public void op_read_ya_dp(DoubleUShortFunction op) { throw new NotImplementedException(); }
        public void op_cmpw_ya_dp() { throw new NotImplementedException(); }
        public void op_and1_bit(int op) { throw new NotImplementedException(); }
        public void op_eor1_bit() { throw new NotImplementedException(); }
        public void op_not1_bit() { throw new NotImplementedException(); }
        public void op_or1_bit(int op) { throw new NotImplementedException(); }

        public void op_adjust_reg(SingleByteFunction op, int n) { throw new NotImplementedException(); }
        public void op_adjust_dp(SingleByteFunction op) { throw new NotImplementedException(); }
        public void op_adjust_dpx(SingleByteFunction op) { throw new NotImplementedException(); }
        public void op_adjust_addr(SingleByteFunction op) { throw new NotImplementedException(); }
        public void op_adjust_addr_a(int op) { throw new NotImplementedException(); }
        public void op_adjustw_dp(int adjust) { throw new NotImplementedException(); }

        public void op_nop() { throw new NotImplementedException(); }
        public void op_wait() { throw new NotImplementedException(); }
        public void op_xcn() { throw new NotImplementedException(); }
        public void op_daa() { throw new NotImplementedException(); }
        public void op_das() { throw new NotImplementedException(); }
        public void op_setbit(int mask, int value) { throw new NotImplementedException(); }
        public void op_notc() { throw new NotImplementedException(); }
        public void op_seti(int value) { throw new NotImplementedException(); }
        public void op_setbit_dp(int op, int value) { throw new NotImplementedException(); }
        public void op_push_reg(int n) { throw new NotImplementedException(); }
        public void op_push_p() { throw new NotImplementedException(); }
        public void op_pop_reg(int n) { throw new NotImplementedException(); }
        public void op_pop_p() { throw new NotImplementedException(); }
        public void op_mul_ya() { throw new NotImplementedException(); }
        public void op_div_ya_x() { throw new NotImplementedException(); }

        public Operation[] opcode_table = new Operation[256];
        public void initialize_opcode_table() { throw new NotImplementedException(); }

        public SMPCore() { throw new NotImplementedException(); }
    }
}
