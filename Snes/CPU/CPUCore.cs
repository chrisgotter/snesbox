using System;

namespace Snes
{
    abstract partial class CPUCore
    {
        public byte op_readpc() { throw new NotImplementedException(); }
        public byte op_readstack() { throw new NotImplementedException(); }
        public byte op_readstackn() { throw new NotImplementedException(); }
        public byte op_readaddr(uint addr) { throw new NotImplementedException(); }
        public byte op_readlong(uint addr) { throw new NotImplementedException(); }
        public byte op_readdbr(uint addr) { throw new NotImplementedException(); }
        public byte op_readpbr(uint addr) { throw new NotImplementedException(); }
        public byte op_readdp(uint addr) { throw new NotImplementedException(); }
        public byte op_readsp(uint addr) { throw new NotImplementedException(); }
        public void op_writestack(byte data) { throw new NotImplementedException(); }
        public void op_writestackn(byte data) { throw new NotImplementedException(); }
        public void op_writeaddr(uint addr, byte data) { throw new NotImplementedException(); }
        public void op_writelong(uint addr, byte data) { throw new NotImplementedException(); }
        public void op_writedbr(uint addr, byte data) { throw new NotImplementedException(); }
        public void op_writepbr(uint addr, byte data) { throw new NotImplementedException(); }
        public void op_writedp(uint addr, byte data) { throw new NotImplementedException(); }
        public void op_writesp(uint addr, byte data) { throw new NotImplementedException(); }

        public enum OPTYPE { DP = 0, DPX, DPY, IDP, IDPX, IDPY, ILDP, ILDPY, ADDR, ADDRX, ADDRY, IADDRX, ILADDR, LONG, LONGX, SR, ISRY, ADDR_PC, IADDR_PC, RELB, RELW }

        public void disassemble_opcode(string output, uint addr) { throw new NotImplementedException(); }
        public byte dreadb(uint addr) { throw new NotImplementedException(); }
        public ushort dreadw(uint addr) { throw new NotImplementedException(); }
        public uint dreadl(uint addr) { throw new NotImplementedException(); }
        public uint decode(byte offset_type, uint addr) { throw new NotImplementedException(); }
        public byte opcode_length() { throw new NotImplementedException(); }

        public Regs regs = new Regs();
        public Reg24 aa = new Reg24();
        public Reg24 rd = new Reg24();
        public byte sp, dp;

        public abstract void op_io();
        public abstract byte op_read(uint addr);
        public abstract void op_write(uint addr, byte data);
        public abstract void last_cycle();
        public abstract bool interrupt_pending();

        public void op_io_irq() { throw new NotImplementedException(); }
        public void op_io_cond2() { throw new NotImplementedException(); }
        public void op_io_cond4(ushort x, ushort y) { throw new NotImplementedException(); }
        public void op_io_cond6(ushort addr) { throw new NotImplementedException(); }

        public void op_adc_b() { throw new NotImplementedException(); }
        public void op_adc_w() { throw new NotImplementedException(); }
        public void op_and_b() { throw new NotImplementedException(); }
        public void op_and_w() { throw new NotImplementedException(); }
        public void op_bit_b() { throw new NotImplementedException(); }
        public void op_bit_w() { throw new NotImplementedException(); }
        public void op_cmp_b() { throw new NotImplementedException(); }
        public void op_cmp_w() { throw new NotImplementedException(); }
        public void op_cpx_b() { throw new NotImplementedException(); }
        public void op_cpx_w() { throw new NotImplementedException(); }
        public void op_cpy_b() { throw new NotImplementedException(); }
        public void op_cpy_w() { throw new NotImplementedException(); }
        public void op_eor_b() { throw new NotImplementedException(); }
        public void op_eor_w() { throw new NotImplementedException(); }
        public void op_lda_b() { throw new NotImplementedException(); }
        public void op_lda_w() { throw new NotImplementedException(); }
        public void op_ldx_b() { throw new NotImplementedException(); }
        public void op_ldx_w() { throw new NotImplementedException(); }
        public void op_ldy_b() { throw new NotImplementedException(); }
        public void op_ldy_w() { throw new NotImplementedException(); }
        public void op_ora_b() { throw new NotImplementedException(); }
        public void op_ora_w() { throw new NotImplementedException(); }
        public void op_sbc_b() { throw new NotImplementedException(); }
        public void op_sbc_w() { throw new NotImplementedException(); }

        public void op_inc_b() { throw new NotImplementedException(); }
        public void op_inc_w() { throw new NotImplementedException(); }
        public void op_dec_b() { throw new NotImplementedException(); }
        public void op_dec_w() { throw new NotImplementedException(); }
        public void op_asl_b() { throw new NotImplementedException(); }
        public void op_asl_w() { throw new NotImplementedException(); }
        public void op_lsr_b() { throw new NotImplementedException(); }
        public void op_lsr_w() { throw new NotImplementedException(); }
        public void op_rol_b() { throw new NotImplementedException(); }
        public void op_rol_w() { throw new NotImplementedException(); }
        public void op_ror_b() { throw new NotImplementedException(); }
        public void op_ror_w() { throw new NotImplementedException(); }
        public void op_trb_b() { throw new NotImplementedException(); }
        public void op_trb_w() { throw new NotImplementedException(); }
        public void op_tsb_b() { throw new NotImplementedException(); }
        public void op_tsb_w() { throw new NotImplementedException(); }

        public void op_read_const_b(Operation op) { throw new NotImplementedException(); }
        public void op_read_const_w(Operation op) { throw new NotImplementedException(); }
        public void op_read_bit_const_b() { throw new NotImplementedException(); }
        public void op_read_bit_const_w() { throw new NotImplementedException(); }
        public void op_read_addr_b(Operation op) { throw new NotImplementedException(); }
        public void op_read_addr_w(Operation op) { throw new NotImplementedException(); }
        public void op_read_addrx_b(Operation op) { throw new NotImplementedException(); }
        public void op_read_addrx_w(Operation op) { throw new NotImplementedException(); }
        public void op_read_addry_b(Operation op) { throw new NotImplementedException(); }
        public void op_read_addry_w(Operation op) { throw new NotImplementedException(); }
        public void op_read_long_b(Operation op) { throw new NotImplementedException(); }
        public void op_read_long_w(Operation op) { throw new NotImplementedException(); }
        public void op_read_longx_b(Operation op) { throw new NotImplementedException(); }
        public void op_read_longx_w(Operation op) { throw new NotImplementedException(); }
        public void op_read_dp_b(Operation op) { throw new NotImplementedException(); }
        public void op_read_dp_w(Operation op) { throw new NotImplementedException(); }
        public void op_read_dpr_b(Operation op, int n) { throw new NotImplementedException(); }
        public void op_read_dpr_w(Operation op, int n) { throw new NotImplementedException(); }
        public void op_read_idp_b(Operation op) { throw new NotImplementedException(); }
        public void op_read_idp_w(Operation op) { throw new NotImplementedException(); }
        public void op_read_idpx_b(Operation op) { throw new NotImplementedException(); }
        public void op_read_idpx_w(Operation op) { throw new NotImplementedException(); }
        public void op_read_idpy_b(Operation op) { throw new NotImplementedException(); }
        public void op_read_idpy_w(Operation op) { throw new NotImplementedException(); }
        public void op_read_ildp_b(Operation op) { throw new NotImplementedException(); }
        public void op_read_ildp_w(Operation op) { throw new NotImplementedException(); }
        public void op_read_ildpy_b(Operation op) { throw new NotImplementedException(); }
        public void op_read_ildpy_w(Operation op) { throw new NotImplementedException(); }
        public void op_read_sr_b(Operation op) { throw new NotImplementedException(); }
        public void op_read_sr_w(Operation op) { throw new NotImplementedException(); }
        public void op_read_isry_b(Operation op) { throw new NotImplementedException(); }
        public void op_read_isry_w(Operation op) { throw new NotImplementedException(); }

        public void op_write_addr_b(int n) { throw new NotImplementedException(); }
        public void op_write_addr_w(int n) { throw new NotImplementedException(); }
        public void op_write_addrr_b(int n, int i) { throw new NotImplementedException(); }
        public void op_write_addrr_w(int n, int i) { throw new NotImplementedException(); }
        public void op_write_longr_b(int i) { throw new NotImplementedException(); }
        public void op_write_longr_w(int n) { throw new NotImplementedException(); }
        public void op_write_dp_b(int n) { throw new NotImplementedException(); }
        public void op_write_dp_w(int n) { throw new NotImplementedException(); }
        public void op_write_dpr_b(int n, int i) { throw new NotImplementedException(); }
        public void op_write_dpr_w(int n, int i) { throw new NotImplementedException(); }
        public void op_sta_idp_b() { throw new NotImplementedException(); }
        public void op_sta_idp_w() { throw new NotImplementedException(); }
        public void op_sta_ildp_b() { throw new NotImplementedException(); }
        public void op_sta_ildp_w() { throw new NotImplementedException(); }
        public void op_sta_idpx_b() { throw new NotImplementedException(); }
        public void op_sta_idpx_w() { throw new NotImplementedException(); }
        public void op_sta_idpy_b() { throw new NotImplementedException(); }
        public void op_sta_idpy_w() { throw new NotImplementedException(); }
        public void op_sta_ildpy_b() { throw new NotImplementedException(); }
        public void op_sta_ildpy_w() { throw new NotImplementedException(); }
        public void op_sta_sr_b() { throw new NotImplementedException(); }
        public void op_sta_sr_w() { throw new NotImplementedException(); }
        public void op_sta_isry_b() { throw new NotImplementedException(); }
        public void op_sta_isry_w() { throw new NotImplementedException(); }

        public void op_adjust_imm_b(int n, int adjust) { throw new NotImplementedException(); }
        public void op_adjust_imm_w(int n, int adjust) { throw new NotImplementedException(); }
        public void op_asl_imm_b() { throw new NotImplementedException(); }
        public void op_asl_imm_w() { throw new NotImplementedException(); }
        public void op_lsr_imm_b() { throw new NotImplementedException(); }
        public void op_lsr_imm_w() { throw new NotImplementedException(); }
        public void op_rol_imm_b() { throw new NotImplementedException(); }
        public void op_rol_imm_w() { throw new NotImplementedException(); }
        public void op_ror_imm_b() { throw new NotImplementedException(); }
        public void op_ror_imm_w() { throw new NotImplementedException(); }
        public void op_adjust_addr_b(Operation op) { throw new NotImplementedException(); }
        public void op_adjust_addr_w(Operation op) { throw new NotImplementedException(); }
        public void op_adjust_addrx_b(Operation op) { throw new NotImplementedException(); }
        public void op_adjust_addrx_w(Operation op) { throw new NotImplementedException(); }
        public void op_adjust_dp_b(Operation op) { throw new NotImplementedException(); }
        public void op_adjust_dp_w(Operation op) { throw new NotImplementedException(); }
        public void op_adjust_dpx_b(Operation op) { throw new NotImplementedException(); }
        public void op_adjust_dpx_w(Operation op) { throw new NotImplementedException(); }

        public void op_branch(int bit, int val) { throw new NotImplementedException(); }
        public void op_bra() { throw new NotImplementedException(); }
        public void op_brl() { throw new NotImplementedException(); }
        public void op_jmp_addr() { throw new NotImplementedException(); }
        public void op_jmp_long() { throw new NotImplementedException(); }
        public void op_jmp_iaddr() { throw new NotImplementedException(); }
        public void op_jmp_iaddrx() { throw new NotImplementedException(); }
        public void op_jmp_iladdr() { throw new NotImplementedException(); }
        public void op_jsr_addr() { throw new NotImplementedException(); }
        public void op_jsr_long_e() { throw new NotImplementedException(); }
        public void op_jsr_long_n() { throw new NotImplementedException(); }
        public void op_jsr_iaddrx_e() { throw new NotImplementedException(); }
        public void op_jsr_iaddrx_n() { throw new NotImplementedException(); }
        public void op_rti_e() { throw new NotImplementedException(); }
        public void op_rti_n() { throw new NotImplementedException(); }
        public void op_rts() { throw new NotImplementedException(); }
        public void op_rtl_e() { throw new NotImplementedException(); }
        public void op_rtl_n() { throw new NotImplementedException(); }

        public void op_nop() { throw new NotImplementedException(); }
        public void op_wdm() { throw new NotImplementedException(); }
        public void op_xba() { throw new NotImplementedException(); }
        public void op_move_b(int adjust) { throw new NotImplementedException(); }
        public void op_move_w(int adjust) { throw new NotImplementedException(); }
        public void op_interrupt_e(int vectorE, int vectorN) { throw new NotImplementedException(); }
        public void op_interrupt_n(int vectorE, int vectorN) { throw new NotImplementedException(); }
        public void op_stp() { throw new NotImplementedException(); }
        public void op_wai() { throw new NotImplementedException(); }
        public void op_xce() { throw new NotImplementedException(); }
        public void op_flag(int mask, int value) { throw new NotImplementedException(); }
        public void op_pflag_e(int mode) { throw new NotImplementedException(); }
        public void op_pflag_n(int mode) { throw new NotImplementedException(); }
        public void op_transfer_b(int from, int to) { throw new NotImplementedException(); }
        public void op_transfer_w(int from, int to) { throw new NotImplementedException(); }
        public void op_tcs_e() { throw new NotImplementedException(); }
        public void op_tcs_n() { throw new NotImplementedException(); }
        public void op_tsx_b() { throw new NotImplementedException(); }
        public void op_tsx_w() { throw new NotImplementedException(); }
        public void op_txs_e() { throw new NotImplementedException(); }
        public void op_txs_n() { throw new NotImplementedException(); }
        public void op_push_b(int n) { throw new NotImplementedException(); }
        public void op_push_w(int n) { throw new NotImplementedException(); }
        public void op_phd_e() { throw new NotImplementedException(); }
        public void op_phd_n() { throw new NotImplementedException(); }
        public void op_phb() { throw new NotImplementedException(); }
        public void op_phk() { throw new NotImplementedException(); }
        public void op_php() { throw new NotImplementedException(); }
        public void op_pull_b(int n) { throw new NotImplementedException(); }
        public void op_pull_w(int n) { throw new NotImplementedException(); }
        public void op_pld_e() { throw new NotImplementedException(); }
        public void op_pld_n() { throw new NotImplementedException(); }
        public void op_plb() { throw new NotImplementedException(); }
        public void op_plp_e() { throw new NotImplementedException(); }
        public void op_plp_n() { throw new NotImplementedException(); }
        public void op_pea_e() { throw new NotImplementedException(); }
        public void op_pea_n() { throw new NotImplementedException(); }
        public void op_pei_e() { throw new NotImplementedException(); }
        public void op_pei_n() { throw new NotImplementedException(); }
        public void op_per_e() { throw new NotImplementedException(); }
        public void op_per_n() { throw new NotImplementedException(); }

        public Operation[] opcode_table;
        public Operation[] op_table = new Operation[256 * 5];
        public void initialize_opcode_table() { throw new NotImplementedException(); }
        public void update_table() { throw new NotImplementedException(); }

        public enum Table { EM = 0, MX = 256, Mx = 512, mX = 768, mx = 1024 }

        public CPUCore() { throw new NotImplementedException(); }
    }
}
