using System;
using System.Text;
using Nall;

namespace Snes
{
    abstract partial class CPUCore
    {
        public byte op_readpc()
        {
            return op_read((uint)((regs.pc.b << 16) + regs.pc.w++));
        }

        public byte op_readstack()
        {
            if (regs.e)
            {
                regs.s.l++;
            }
            else
            {
                regs.s.w++;
            }
            return op_read(regs.s.w);
        }

        public byte op_readstackn()
        {
            return op_read(++regs.s.w);
        }

        public byte op_readaddr(uint addr)
        {
            return op_read(addr & 0xffff);
        }

        public byte op_readlong(uint addr)
        {
            return op_read(addr & 0xffffff);
        }

        public byte op_readdbr(uint addr)
        {
            return op_read((uint)(((regs.db << 16) + addr) & 0xffffff));
        }

        public byte op_readpbr(uint addr)
        {
            return op_read((uint)((regs.pc.b << 16) + (addr & 0xffff)));
        }

        public byte op_readdp(uint addr)
        {
            if (regs.e && regs.d.l == 0x00)
            {
                return op_read((regs.d & 0xff00) + ((regs.d + (addr & 0xffff)) & 0xff));
            }
            else
            {
                return op_read((regs.d + (addr & 0xffff)) & 0xffff);
            }
        }

        public byte op_readsp(uint addr)
        {
            return op_read((regs.s + (addr & 0xffff)) & 0xffff);
        }

        public void op_writestack(byte data)
        {
            op_write(regs.s.w, data);
            if (regs.e)
            {
                regs.s.l--;
            }
            else
            {
                regs.s.w--;
            }

        }

        public void op_writestackn(byte data)
        {
            op_write(regs.s.w--, data);
        }

        public void op_writeaddr(uint addr, byte data)
        {
            op_write(addr & 0xffff, data);
        }

        public void op_writelong(uint addr, byte data)
        {
            op_write(addr & 0xffffff, data);
        }

        public void op_writedbr(uint addr, byte data)
        {
            op_write((uint)(((regs.db << 16) + addr) & 0xffffff), data);
        }

        public void op_writepbr(uint addr, byte data)
        {
            op_write((uint)((regs.pc.b << 16) + (addr & 0xffff)), data);
        }

        public void op_writedp(uint addr, byte data)
        {
            if (regs.e && regs.d.l == 0x00)
            {
                op_write((regs.d & 0xff00) + ((regs.d + (addr & 0xffff)) & 0xff), data);
            }
            else
            {
                op_write((regs.d + (addr & 0xffff)) & 0xffff, data);
            }
        }

        public void op_writesp(uint addr, byte data)
        {
            op_write((regs.s + (addr & 0xffff)) & 0xffff, data);
        }

        public enum OPTYPE { DP = 0, DPX, DPY, IDP, IDPX, IDPY, ILDP, ILDPY, ADDR, ADDRX, ADDRY, IADDRX, ILADDR, LONG, LONGX, SR, ISRY, ADDR_PC, IADDR_PC, RELB, RELW }

        private static Reg24 pc = new Reg24();

        private static byte op8(byte op0)
        {
            return ((op0));
        }

        private static ushort op16(byte op0, byte op1)
        {
            return (ushort)((op0) | (op1 << 8));
        }

        private static uint op24(byte op0, byte op1, byte op2)
        {
            return (uint)((op0) | (op1 << 8) | (op2 << 16));
        }

        private static bool a8(Regs regs)
        {
            return regs.e || regs.p.m;
        }

        private static bool x8(Regs regs)
        {
            return regs.e || regs.p.x;
        }

        public void disassemble_opcode(byte[] output, uint addr)
        {
            string t = string.Empty;
            string s = new UTF8Encoding().GetString(output);

            pc.d = addr;
            s = string.Format("%.6x ", (uint)pc.d);

            byte op = dreadb(pc.d);
            pc.w++;
            byte op0 = dreadb(pc.d);
            pc.w++;
            byte op1 = dreadb(pc.d);
            pc.w++;
            byte op2 = dreadb(pc.d);

            switch (op)
            {
                case 0x00:
                    t = string.Format("brk #$%.2x              ", op8(op0));
                    break;
                case 0x01:
                    t = string.Format("ora ($%.2x,x)   [%.6x]", op8(op0), decode((byte)OPTYPE.IDPX, op8(op0)));
                    break;
                case 0x02:
                    t = string.Format("cop #$%.2x              ", op8(op0));
                    break;
                case 0x03:
                    t = string.Format("ora $%.2x,s     [%.6x]", op8(op0), decode((byte)OPTYPE.SR, op8(op0)));
                    break;
                case 0x04:
                    t = string.Format("tsb $%.2x       [%.6x]", op8(op0), decode((byte)OPTYPE.DP, op8(op0)));
                    break;
                case 0x05:
                    t = string.Format("ora $%.2x       [%.6x]", op8(op0), decode((byte)OPTYPE.DP, op8(op0)));
                    break;
                case 0x06:
                    t = string.Format("asl $%.2x       [%.6x]", op8(op0), decode((byte)OPTYPE.DP, op8(op0)));
                    break;
                case 0x07:
                    t = string.Format("ora [$%.2x]     [%.6x]", op8(op0), decode((byte)OPTYPE.ILDP, op8(op0)));
                    break;
                case 0x08:
                    t = string.Format("php                   ");
                    break;
                case 0x09:
                    if (a8(regs))
                    {
                        t = string.Format("ora #$%.2x              ", op8(op0));
                    }
                    else
                    {
                        t = string.Format("ora #$%.4x            ", op16(op0, op1));
                    }
                    break;
                case 0x0a:
                    t = string.Format("asl a                 ");
                    break;
                case 0x0b:
                    t = string.Format("phd                   ");
                    break;
                case 0x0c:
                    t = string.Format("tsb $%.4x     [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDR, op16(op0, op1)));
                    break;
                case 0x0d:
                    t = string.Format("ora $%.4x     [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDR, op16(op0, op1)));
                    break;
                case 0x0e:
                    t = string.Format("asl $%.4x     [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDR, op16(op0, op1)));
                    break;
                case 0x0f:
                    t = string.Format("ora $%.6x   [%.6x]", op24(op0, op1, op2), decode((byte)OPTYPE.LONG, op24(op0, op1, op2)));
                    break;
                case 0x10:
                    t = string.Format("bpl $%.4x     [%.6x]", (ushort)(decode((byte)OPTYPE.RELB, op8(op0))), decode((byte)OPTYPE.RELB, op8(op0)));
                    break;
                case 0x11:
                    t = string.Format("ora ($%.2x),y   [%.6x]", op8(op0), decode((byte)OPTYPE.IDPY, op8(op0)));
                    break;
                case 0x12:
                    t = string.Format("ora ($%.2x)     [%.6x]", op8(op0), decode((byte)OPTYPE.IDP, op8(op0)));
                    break;
                case 0x13:
                    t = string.Format("ora ($%.2x,s),y [%.6x]", op8(op0), decode((byte)OPTYPE.ISRY, op8(op0)));
                    break;
                case 0x14:
                    t = string.Format("trb $%.2x       [%.6x]", op8(op0), decode((byte)OPTYPE.DP, op8(op0)));
                    break;
                case 0x15:
                    t = string.Format("ora $%.2x,x     [%.6x]", op8(op0), decode((byte)OPTYPE.DPX, op8(op0)));
                    break;
                case 0x16:
                    t = string.Format("asl $%.2x,x     [%.6x]", op8(op0), decode((byte)OPTYPE.DPX, op8(op0)));
                    break;
                case 0x17:
                    t = string.Format("ora [$%.2x],y   [%.6x]", op8(op0), decode((byte)OPTYPE.ILDPY, op8(op0)));
                    break;
                case 0x18:
                    t = string.Format("clc                   ");
                    break;
                case 0x19:
                    t = string.Format("ora $%.4x,y   [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDRY, op16(op0, op1)));
                    break;
                case 0x1a:
                    t = string.Format("inc                   ");
                    break;
                case 0x1b:
                    t = string.Format("tcs                   ");
                    break;
                case 0x1c:
                    t = string.Format("trb $%.4x     [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDR, op16(op0, op1)));
                    break;
                case 0x1d:
                    t = string.Format("ora $%.4x,x   [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDRX, op16(op0, op1)));
                    break;
                case 0x1e:
                    t = string.Format("asl $%.4x,x   [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDRX, op16(op0, op1)));
                    break;
                case 0x1f:
                    t = string.Format("ora $%.6x,x [%.6x]", op24(op0, op1, op2), decode((byte)OPTYPE.LONGX, op24(op0, op1, op2)));
                    break;
                case 0x20:
                    t = string.Format("jsr $%.4x     [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDR_PC, op16(op0, op1)));
                    break;
                case 0x21:
                    t = string.Format("and ($%.2x,x)   [%.6x]", op8(op0), decode((byte)OPTYPE.IDPX, op8(op0)));
                    break;
                case 0x22:
                    t = string.Format("jsl $%.6x   [%.6x]", op24(op0, op1, op2), decode((byte)OPTYPE.LONG, op24(op0, op1, op2)));
                    break;
                case 0x23:
                    t = string.Format("and $%.2x,s     [%.6x]", op8(op0), decode((byte)OPTYPE.SR, op8(op0)));
                    break;
                case 0x24:
                    t = string.Format("bit $%.2x       [%.6x]", op8(op0), decode((byte)OPTYPE.DP, op8(op0)));
                    break;
                case 0x25:
                    t = string.Format("and $%.2x       [%.6x]", op8(op0), decode((byte)OPTYPE.DP, op8(op0)));
                    break;
                case 0x26:
                    t = string.Format("rol $%.2x       [%.6x]", op8(op0), decode((byte)OPTYPE.DP, op8(op0)));
                    break;
                case 0x27:
                    t = string.Format("and [$%.2x]     [%.6x]", op8(op0), decode((byte)OPTYPE.ILDP, op8(op0)));
                    break;
                case 0x28:
                    t = string.Format("plp                   ");
                    break;
                case 0x29:
                    if (a8(regs))
                    {
                        t = string.Format("and #$%.2x              ", op8(op0));
                    }
                    else
                    {
                        t = string.Format("and #$%.4x            ", op16(op0, op1));
                    }
                    break;
                case 0x2a:
                    t = string.Format("rol a                 ");
                    break;
                case 0x2b:
                    t = string.Format("pld                   ");
                    break;
                case 0x2c:
                    t = string.Format("bit $%.4x     [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDR, op16(op0, op1)));
                    break;
                case 0x2d:
                    t = string.Format("and $%.4x     [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDR, op16(op0, op1)));
                    break;
                case 0x2e:
                    t = string.Format("rol $%.4x     [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDR, op16(op0, op1)));
                    break;
                case 0x2f:
                    t = string.Format("and $%.6x   [%.6x]", op24(op0, op1, op2), decode((byte)OPTYPE.LONG, op24(op0, op1, op2)));
                    break;
                case 0x30:
                    t = string.Format("bmi $%.4x     [%.6x]", (ushort)(decode((byte)OPTYPE.RELB, op8(op0))), decode((byte)OPTYPE.RELB, op8(op0)));
                    break;
                case 0x31:
                    t = string.Format("and ($%.2x),y   [%.6x]", op8(op0), decode((byte)OPTYPE.IDPY, op8(op0)));
                    break;
                case 0x32:
                    t = string.Format("and ($%.2x)     [%.6x]", op8(op0), decode((byte)OPTYPE.IDP, op8(op0)));
                    break;
                case 0x33:
                    t = string.Format("and ($%.2x,s),y [%.6x]", op8(op0), decode((byte)OPTYPE.ISRY, op8(op0)));
                    break;
                case 0x34:
                    t = string.Format("bit $%.2x,x     [%.6x]", op8(op0), decode((byte)OPTYPE.DPX, op8(op0)));
                    break;
                case 0x35:
                    t = string.Format("and $%.2x,x     [%.6x]", op8(op0), decode((byte)OPTYPE.DPX, op8(op0)));
                    break;
                case 0x36:
                    t = string.Format("rol $%.2x,x     [%.6x]", op8(op0), decode((byte)OPTYPE.DPX, op8(op0)));
                    break;
                case 0x37:
                    t = string.Format("and [$%.2x],y   [%.6x]", op8(op0), decode((byte)OPTYPE.ILDPY, op8(op0)));
                    break;
                case 0x38:
                    t = string.Format("sec                   ");
                    break;
                case 0x39:
                    t = string.Format("and $%.4x,y   [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDRY, op16(op0, op1)));
                    break;
                case 0x3a:
                    t = string.Format("dec                   ");
                    break;
                case 0x3b:
                    t = string.Format("tsc                   ");
                    break;
                case 0x3c:
                    t = string.Format("bit $%.4x,x   [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDRX, op16(op0, op1)));
                    break;
                case 0x3d:
                    t = string.Format("and $%.4x,x   [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDRX, op16(op0, op1)));
                    break;
                case 0x3e:
                    t = string.Format("rol $%.4x,x   [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDRX, op16(op0, op1)));
                    break;
                case 0x3f:
                    t = string.Format("and $%.6x,x [%.6x]", op24(op0, op1, op2), decode((byte)OPTYPE.LONGX, op24(op0, op1, op2)));
                    break;
                case 0x40:
                    t = string.Format("rti                   ");
                    break;
                case 0x41:
                    t = string.Format("eor ($%.2x,x)   [%.6x]", op8(op0), decode((byte)OPTYPE.IDPX, op8(op0)));
                    break;
                case 0x42:
                    t = string.Format("wdm                   ");
                    break;
                case 0x43:
                    t = string.Format("eor $%.2x,s     [%.6x]", op8(op0), decode((byte)OPTYPE.SR, op8(op0)));
                    break;
                case 0x44:
                    t = string.Format("mvp $%.2x,$%.2x           ", op1, op8(op0));
                    break;
                case 0x45:
                    t = string.Format("eor $%.2x       [%.6x]", op8(op0), decode((byte)OPTYPE.DP, op8(op0)));
                    break;
                case 0x46:
                    t = string.Format("lsr $%.2x       [%.6x]", op8(op0), decode((byte)OPTYPE.DP, op8(op0)));
                    break;
                case 0x47:
                    t = string.Format("eor [$%.2x]     [%.6x]", op8(op0), decode((byte)OPTYPE.ILDP, op8(op0)));
                    break;
                case 0x48:
                    t = string.Format("pha                   ");
                    break;
                case 0x49:
                    if (a8(regs))
                    {
                        t = string.Format("eor #$%.2x              ", op8(op0));
                    }
                    else
                    {
                        t = string.Format("eor #$%.4x            ", op16(op0, op1));
                    }
                    break;
                case 0x4a:
                    t = string.Format("lsr a                 ");
                    break;
                case 0x4b:
                    t = string.Format("phk                   ");
                    break;
                case 0x4c:
                    t = string.Format("jmp $%.4x     [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDR_PC, op16(op0, op1)));
                    break;
                case 0x4d:
                    t = string.Format("eor $%.4x     [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDR, op16(op0, op1)));
                    break;
                case 0x4e:
                    t = string.Format("lsr $%.4x     [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDR, op16(op0, op1)));
                    break;
                case 0x4f:
                    t = string.Format("eor $%.6x   [%.6x]", op24(op0, op1, op2), decode((byte)OPTYPE.LONG, op24(op0, op1, op2)));
                    break;
                case 0x50:
                    t = string.Format("bvc $%.4x     [%.6x]", (ushort)(decode((byte)OPTYPE.RELB, op8(op0))), decode((byte)OPTYPE.RELB, op8(op0)));
                    break;
                case 0x51:
                    t = string.Format("eor ($%.2x),y   [%.6x]", op8(op0), decode((byte)OPTYPE.IDPY, op8(op0)));
                    break;
                case 0x52:
                    t = string.Format("eor ($%.2x)     [%.6x]", op8(op0), decode((byte)OPTYPE.IDP, op8(op0)));
                    break;
                case 0x53:
                    t = string.Format("eor ($%.2x,s),y [%.6x]", op8(op0), decode((byte)OPTYPE.ISRY, op8(op0)));
                    break;
                case 0x54:
                    t = string.Format("mvn $%.2x,$%.2x           ", op1, op8(op0));
                    break;
                case 0x55:
                    t = string.Format("eor $%.2x,x     [%.6x]", op8(op0), decode((byte)OPTYPE.DPX, op8(op0)));
                    break;
                case 0x56:
                    t = string.Format("lsr $%.2x,x     [%.6x]", op8(op0), decode((byte)OPTYPE.DPX, op8(op0)));
                    break;
                case 0x57:
                    t = string.Format("eor [$%.2x],y   [%.6x]", op8(op0), decode((byte)OPTYPE.ILDPY, op8(op0)));
                    break;
                case 0x58:
                    t = string.Format("cli                   ");
                    break;
                case 0x59:
                    t = string.Format("eor $%.4x,y   [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDRY, op16(op0, op1)));
                    break;
                case 0x5a:
                    t = string.Format("phy                   ");
                    break;
                case 0x5b:
                    t = string.Format("tcd                   ");
                    break;
                case 0x5c:
                    t = string.Format("jml $%.6x   [%.6x]", op24(op0, op1, op2), decode((byte)OPTYPE.LONG, op24(op0, op1, op2)));
                    break;
                case 0x5d:
                    t = string.Format("eor $%.4x,x   [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDRX, op16(op0, op1)));
                    break;
                case 0x5e:
                    t = string.Format("lsr $%.4x,x   [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDRX, op16(op0, op1)));
                    break;
                case 0x5f:
                    t = string.Format("eor $%.6x,x [%.6x]", op24(op0, op1, op2), decode((byte)OPTYPE.LONGX, op24(op0, op1, op2)));
                    break;
                case 0x60:
                    t = string.Format("rts                   ");
                    break;
                case 0x61:
                    t = string.Format("adc ($%.2x,x)   [%.6x]", op8(op0), decode((byte)OPTYPE.IDPX, op8(op0)));
                    break;
                case 0x62:
                    t = string.Format("per $%.4x     [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDR, op16(op0, op1)));
                    break;
                case 0x63:
                    t = string.Format("adc $%.2x,s     [%.6x]", op8(op0), decode((byte)OPTYPE.SR, op8(op0)));
                    break;
                case 0x64:
                    t = string.Format("stz $%.2x       [%.6x]", op8(op0), decode((byte)OPTYPE.DP, op8(op0)));
                    break;
                case 0x65:
                    t = string.Format("adc $%.2x       [%.6x]", op8(op0), decode((byte)OPTYPE.DP, op8(op0)));
                    break;
                case 0x66:
                    t = string.Format("ror $%.2x       [%.6x]", op8(op0), decode((byte)OPTYPE.DP, op8(op0)));
                    break;
                case 0x67:
                    t = string.Format("adc [$%.2x]     [%.6x]", op8(op0), decode((byte)OPTYPE.ILDP, op8(op0)));
                    break;
                case 0x68:
                    t = string.Format("pla                   ");
                    break;
                case 0x69:
                    if (a8(regs))
                    {
                        t = string.Format("adc #$%.2x              ", op8(op0));
                    }
                    else
                    {
                        t = string.Format("adc #$%.4x            ", op16(op0, op1));
                    }
                    break;
                case 0x6a:
                    t = string.Format("ror a                 ");
                    break;
                case 0x6b:
                    t = string.Format("rtl                   ");
                    break;
                case 0x6c:
                    t = string.Format("jmp ($%.4x)   [%.6x]", op16(op0, op1), decode((byte)OPTYPE.IADDR_PC, op16(op0, op1)));
                    break;
                case 0x6d:
                    t = string.Format("adc $%.4x     [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDR, op16(op0, op1)));
                    break;
                case 0x6e:
                    t = string.Format("ror $%.4x     [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDR, op16(op0, op1)));
                    break;
                case 0x6f:
                    t = string.Format("adc $%.6x   [%.6x]", op24(op0, op1, op2), decode((byte)OPTYPE.LONG, op24(op0, op1, op2)));
                    break;
                case 0x70:
                    t = string.Format("bvs $%.4x     [%.6x]", (ushort)(decode((byte)OPTYPE.RELB, op8(op0))), decode((byte)OPTYPE.RELB, op8(op0)));
                    break;
                case 0x71:
                    t = string.Format("adc ($%.2x),y   [%.6x]", op8(op0), decode((byte)OPTYPE.IDPY, op8(op0)));
                    break;
                case 0x72:
                    t = string.Format("adc ($%.2x)     [%.6x]", op8(op0), decode((byte)OPTYPE.IDP, op8(op0)));
                    break;
                case 0x73:
                    t = string.Format("adc ($%.2x,s),y [%.6x]", op8(op0), decode((byte)OPTYPE.ISRY, op8(op0)));
                    break;
                case 0x74:
                    t = string.Format("stz $%.2x,x     [%.6x]", op8(op0), decode((byte)OPTYPE.DPX, op8(op0)));
                    break;
                case 0x75:
                    t = string.Format("adc $%.2x,x     [%.6x]", op8(op0), decode((byte)OPTYPE.DPX, op8(op0)));
                    break;
                case 0x76:
                    t = string.Format("ror $%.2x,x     [%.6x]", op8(op0), decode((byte)OPTYPE.DPX, op8(op0)));
                    break;
                case 0x77:
                    t = string.Format("adc [$%.2x],y   [%.6x]", op8(op0), decode((byte)OPTYPE.ILDPY, op8(op0)));
                    break;
                case 0x78:
                    t = string.Format("sei                   ");
                    break;
                case 0x79:
                    t = string.Format("adc $%.4x,y   [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDRY, op16(op0, op1)));
                    break;
                case 0x7a:
                    t = string.Format("ply                   ");
                    break;
                case 0x7b:
                    t = string.Format("tdc                   ");
                    break;
                case 0x7c:
                    t = string.Format("jmp ($%.4x,x) [%.6x]", op16(op0, op1), decode((byte)OPTYPE.IADDRX, op16(op0, op1)));
                    break;
                case 0x7d:
                    t = string.Format("adc $%.4x,x   [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDRX, op16(op0, op1)));
                    break;
                case 0x7e:
                    t = string.Format("ror $%.4x,x   [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDRX, op16(op0, op1)));
                    break;
                case 0x7f:
                    t = string.Format("adc $%.6x,x [%.6x]", op24(op0, op1, op2), decode((byte)OPTYPE.LONGX, op24(op0, op1, op2)));
                    break;
                case 0x80:
                    t = string.Format("bra $%.4x     [%.6x]", (ushort)(decode((byte)OPTYPE.RELB, op8(op0))), decode((byte)OPTYPE.RELB, op8(op0)));
                    break;
                case 0x81:
                    t = string.Format("sta ($%.2x,x)   [%.6x]", op8(op0), decode((byte)OPTYPE.IDPX, op8(op0)));
                    break;
                case 0x82:
                    t = string.Format("brl $%.4x     [%.6x]", (ushort)(decode((byte)OPTYPE.RELW, op16(op0, op1))), decode((byte)OPTYPE.RELW, op16(op0, op1)));
                    break;
                case 0x83:
                    t = string.Format("sta $%.2x,s     [%.6x]", op8(op0), decode((byte)OPTYPE.SR, op8(op0)));
                    break;
                case 0x84:
                    t = string.Format("sty $%.2x       [%.6x]", op8(op0), decode((byte)OPTYPE.DP, op8(op0)));
                    break;
                case 0x85:
                    t = string.Format("sta $%.2x       [%.6x]", op8(op0), decode((byte)OPTYPE.DP, op8(op0)));
                    break;
                case 0x86:
                    t = string.Format("stx $%.2x       [%.6x]", op8(op0), decode((byte)OPTYPE.DP, op8(op0)));
                    break;
                case 0x87:
                    t = string.Format("sta [$%.2x]     [%.6x]", op8(op0), decode((byte)OPTYPE.ILDP, op8(op0)));
                    break;
                case 0x88:
                    t = string.Format("dey                   ");
                    break;
                case 0x89:
                    if (a8(regs))
                    {
                        t = string.Format("bit #$%.2x              ", op8(op0));
                    }
                    else
                    {
                        t = string.Format("bit #$%.4x            ", op16(op0, op1));
                    }
                    break;
                case 0x8a:
                    t = string.Format("txa                   ");
                    break;
                case 0x8b:
                    t = string.Format("phb                   ");
                    break;
                case 0x8c:
                    t = string.Format("sty $%.4x     [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDR, op16(op0, op1)));
                    break;
                case 0x8d:
                    t = string.Format("sta $%.4x     [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDR, op16(op0, op1)));
                    break;
                case 0x8e:
                    t = string.Format("stx $%.4x     [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDR, op16(op0, op1)));
                    break;
                case 0x8f:
                    t = string.Format("sta $%.6x   [%.6x]", op24(op0, op1, op2), decode((byte)OPTYPE.LONG, op24(op0, op1, op2)));
                    break;
                case 0x90:
                    t = string.Format("bcc $%.4x     [%.6x]", (ushort)(decode((byte)OPTYPE.RELB, op8(op0))), decode((byte)OPTYPE.RELB, op8(op0)));
                    break;
                case 0x91:
                    t = string.Format("sta ($%.2x),y   [%.6x]", op8(op0), decode((byte)OPTYPE.IDPY, op8(op0)));
                    break;
                case 0x92:
                    t = string.Format("sta ($%.2x)     [%.6x]", op8(op0), decode((byte)OPTYPE.IDP, op8(op0)));
                    break;
                case 0x93:
                    t = string.Format("sta ($%.2x,s),y [%.6x]", op8(op0), decode((byte)OPTYPE.ISRY, op8(op0)));
                    break;
                case 0x94:
                    t = string.Format("sty $%.2x,x     [%.6x]", op8(op0), decode((byte)OPTYPE.DPX, op8(op0)));
                    break;
                case 0x95:
                    t = string.Format("sta $%.2x,x     [%.6x]", op8(op0), decode((byte)OPTYPE.DPX, op8(op0)));
                    break;
                case 0x96:
                    t = string.Format("stx $%.2x,y     [%.6x]", op8(op0), decode((byte)OPTYPE.DPY, op8(op0)));
                    break;
                case 0x97:
                    t = string.Format("sta [$%.2x],y   [%.6x]", op8(op0), decode((byte)OPTYPE.ILDPY, op8(op0)));
                    break;
                case 0x98:
                    t = string.Format("tya                   ");
                    break;
                case 0x99:
                    t = string.Format("sta $%.4x,y   [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDRY, op16(op0, op1)));
                    break;
                case 0x9a:
                    t = string.Format("txs                   ");
                    break;
                case 0x9b:
                    t = string.Format("txy                   ");
                    break;
                case 0x9c:
                    t = string.Format("stz $%.4x     [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDR, op16(op0, op1)));
                    break;
                case 0x9d:
                    t = string.Format("sta $%.4x,x   [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDRX, op16(op0, op1)));
                    break;
                case 0x9e:
                    t = string.Format("stz $%.4x,x   [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDRX, op16(op0, op1)));
                    break;
                case 0x9f:
                    t = string.Format("sta $%.6x,x [%.6x]", op24(op0, op1, op2), decode((byte)OPTYPE.LONGX, op24(op0, op1, op2)));
                    break;
                case 0xa0:
                    if (x8(regs))
                    {
                        t = string.Format("ldy #$%.2x              ", op8(op0));
                    }
                    else
                    {
                        t = string.Format("ldy #$%.4x            ", op16(op0, op1));
                    }
                    break;
                case 0xa1:
                    t = string.Format("lda ($%.2x,x)   [%.6x]", op8(op0), decode((byte)OPTYPE.IDPX, op8(op0)));
                    break;
                case 0xa2:
                    if (x8(regs))
                    {
                        t = string.Format("ldx #$%.2x              ", op8(op0));
                    }
                    else
                    {
                        t = string.Format("ldx #$%.4x            ", op16(op0, op1));
                    }
                    break;
                case 0xa3:
                    t = string.Format("lda $%.2x,s     [%.6x]", op8(op0), decode((byte)OPTYPE.SR, op8(op0)));
                    break;
                case 0xa4:
                    t = string.Format("ldy $%.2x       [%.6x]", op8(op0), decode((byte)OPTYPE.DP, op8(op0)));
                    break;
                case 0xa5:
                    t = string.Format("lda $%.2x       [%.6x]", op8(op0), decode((byte)OPTYPE.DP, op8(op0)));
                    break;
                case 0xa6:
                    t = string.Format("ldx $%.2x       [%.6x]", op8(op0), decode((byte)OPTYPE.DP, op8(op0)));
                    break;
                case 0xa7:
                    t = string.Format("lda [$%.2x]     [%.6x]", op8(op0), decode((byte)OPTYPE.ILDP, op8(op0)));
                    break;
                case 0xa8:
                    t = string.Format("tay                   ");
                    break;
                case 0xa9:
                    if (a8(regs))
                    {
                        t = string.Format("lda #$%.2x              ", op8(op0));
                    }
                    else
                    {
                        t = string.Format("lda #$%.4x            ", op16(op0, op1));
                    }
                    break;
                case 0xaa:
                    t = string.Format("tax                   ");
                    break;
                case 0xab:
                    t = string.Format("plb                   ");
                    break;
                case 0xac:
                    t = string.Format("ldy $%.4x     [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDR, op16(op0, op1)));
                    break;
                case 0xad:
                    t = string.Format("lda $%.4x     [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDR, op16(op0, op1)));
                    break;
                case 0xae:
                    t = string.Format("ldx $%.4x     [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDR, op16(op0, op1)));
                    break;
                case 0xaf:
                    t = string.Format("lda $%.6x   [%.6x]", op24(op0, op1, op2), decode((byte)OPTYPE.LONG, op24(op0, op1, op2)));
                    break;
                case 0xb0:
                    t = string.Format("bcs $%.4x     [%.6x]", (ushort)(decode((byte)OPTYPE.RELB, op8(op0))), decode((byte)OPTYPE.RELB, op8(op0)));
                    break;
                case 0xb1:
                    t = string.Format("lda ($%.2x),y   [%.6x]", op8(op0), decode((byte)OPTYPE.IDPY, op8(op0)));
                    break;
                case 0xb2:
                    t = string.Format("lda ($%.2x)     [%.6x]", op8(op0), decode((byte)OPTYPE.IDP, op8(op0)));
                    break;
                case 0xb3:
                    t = string.Format("lda ($%.2x,s),y [%.6x]", op8(op0), decode((byte)OPTYPE.ISRY, op8(op0)));
                    break;
                case 0xb4:
                    t = string.Format("ldy $%.2x,x     [%.6x]", op8(op0), decode((byte)OPTYPE.DPX, op8(op0)));
                    break;
                case 0xb5:
                    t = string.Format("lda $%.2x,x     [%.6x]", op8(op0), decode((byte)OPTYPE.DPX, op8(op0)));
                    break;
                case 0xb6:
                    t = string.Format("ldx $%.2x,y     [%.6x]", op8(op0), decode((byte)OPTYPE.DPY, op8(op0)));
                    break;
                case 0xb7:
                    t = string.Format("lda [$%.2x],y   [%.6x]", op8(op0), decode((byte)OPTYPE.ILDPY, op8(op0)));
                    break;
                case 0xb8:
                    t = string.Format("clv                   ");
                    break;
                case 0xb9:
                    t = string.Format("lda $%.4x,y   [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDRY, op16(op0, op1)));
                    break;
                case 0xba:
                    t = string.Format("tsx                   ");
                    break;
                case 0xbb:
                    t = string.Format("tyx                   ");
                    break;
                case 0xbc:
                    t = string.Format("ldy $%.4x,x   [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDRX, op16(op0, op1)));
                    break;
                case 0xbd:
                    t = string.Format("lda $%.4x,x   [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDRX, op16(op0, op1)));
                    break;
                case 0xbe:
                    t = string.Format("ldx $%.4x,y   [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDRY, op16(op0, op1)));
                    break;
                case 0xbf:
                    t = string.Format("lda $%.6x,x [%.6x]", op24(op0, op1, op2), decode((byte)OPTYPE.LONGX, op24(op0, op1, op2)));
                    break;
                case 0xc0:
                    if (x8(regs))
                    {
                        t = string.Format("cpy #$%.2x              ", op8(op0));
                    }
                    else
                    {
                        t = string.Format("cpy #$%.4x            ", op16(op0, op1));
                    }
                    break;
                case 0xc1:
                    t = string.Format("cmp ($%.2x,x)   [%.6x]", op8(op0), decode((byte)OPTYPE.IDPX, op8(op0)));
                    break;
                case 0xc2:
                    t = string.Format("rep #$%.2x              ", op8(op0));
                    break;
                case 0xc3:
                    t = string.Format("cmp $%.2x,s     [%.6x]", op8(op0), decode((byte)OPTYPE.SR, op8(op0)));
                    break;
                case 0xc4:
                    t = string.Format("cpy $%.2x       [%.6x]", op8(op0), decode((byte)OPTYPE.DP, op8(op0)));
                    break;
                case 0xc5:
                    t = string.Format("cmp $%.2x       [%.6x]", op8(op0), decode((byte)OPTYPE.DP, op8(op0)));
                    break;
                case 0xc6:
                    t = string.Format("dec $%.2x       [%.6x]", op8(op0), decode((byte)OPTYPE.DP, op8(op0)));
                    break;
                case 0xc7:
                    t = string.Format("cmp [$%.2x]     [%.6x]", op8(op0), decode((byte)OPTYPE.ILDP, op8(op0)));
                    break;
                case 0xc8:
                    t = string.Format("iny                   ");
                    break;
                case 0xc9:
                    if (a8(regs))
                    {
                        t = string.Format("cmp #$%.2x              ", op8(op0));
                    }
                    else
                    {
                        t = string.Format("cmp #$%.4x            ", op16(op0, op1));
                    }
                    break;
                case 0xca:
                    t = string.Format("dex                   ");
                    break;
                case 0xcb:
                    t = string.Format("wai                   ");
                    break;
                case 0xcc:
                    t = string.Format("cpy $%.4x     [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDR, op16(op0, op1)));
                    break;
                case 0xcd:
                    t = string.Format("cmp $%.4x     [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDR, op16(op0, op1)));
                    break;
                case 0xce:
                    t = string.Format("dec $%.4x     [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDR, op16(op0, op1)));
                    break;
                case 0xcf:
                    t = string.Format("cmp $%.6x   [%.6x]", op24(op0, op1, op2), decode((byte)OPTYPE.LONG, op24(op0, op1, op2)));
                    break;
                case 0xd0:
                    t = string.Format("bne $%.4x     [%.6x]", (ushort)(decode((byte)OPTYPE.RELB, op8(op0))), decode((byte)OPTYPE.RELB, op8(op0)));
                    break;
                case 0xd1:
                    t = string.Format("cmp ($%.2x),y   [%.6x]", op8(op0), decode((byte)OPTYPE.IDPY, op8(op0)));
                    break;
                case 0xd2:
                    t = string.Format("cmp ($%.2x)     [%.6x]", op8(op0), decode((byte)OPTYPE.IDP, op8(op0)));
                    break;
                case 0xd3:
                    t = string.Format("cmp ($%.2x,s),y [%.6x]", op8(op0), decode((byte)OPTYPE.ISRY, op8(op0)));
                    break;
                case 0xd4:
                    t = string.Format("pei ($%.2x)     [%.6x]", op8(op0), decode((byte)OPTYPE.IDP, op8(op0)));
                    break;
                case 0xd5:
                    t = string.Format("cmp $%.2x,x     [%.6x]", op8(op0), decode((byte)OPTYPE.DPX, op8(op0)));
                    break;
                case 0xd6:
                    t = string.Format("dec $%.2x,x     [%.6x]", op8(op0), decode((byte)OPTYPE.DPX, op8(op0)));
                    break;
                case 0xd7:
                    t = string.Format("cmp [$%.2x],y   [%.6x]", op8(op0), decode((byte)OPTYPE.ILDPY, op8(op0)));
                    break;
                case 0xd8:
                    t = string.Format("cld                   ");
                    break;
                case 0xd9:
                    t = string.Format("cmp $%.4x,y   [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDRY, op16(op0, op1)));
                    break;
                case 0xda:
                    t = string.Format("phx                   ");
                    break;
                case 0xdb:
                    t = string.Format("stp                   ");
                    break;
                case 0xdc:
                    t = string.Format("jmp [$%.4x]   [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ILADDR, op16(op0, op1)));
                    break;
                case 0xdd:
                    t = string.Format("cmp $%.4x,x   [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDRX, op16(op0, op1)));
                    break;
                case 0xde:
                    t = string.Format("dec $%.4x,x   [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDRX, op16(op0, op1)));
                    break;
                case 0xdf:
                    t = string.Format("cmp $%.6x,x [%.6x]", op24(op0, op1, op2), decode((byte)OPTYPE.LONGX, op24(op0, op1, op2)));
                    break;
                case 0xe0:
                    if (x8(regs))
                    {
                        t = string.Format("cpx #$%.2x              ", op8(op0));
                    }
                    else
                    {
                        t = string.Format("cpx #$%.4x            ", op16(op0, op1));
                    }
                    break;
                case 0xe1:
                    t = string.Format("sbc ($%.2x,x)   [%.6x]", op8(op0), decode((byte)OPTYPE.IDPX, op8(op0)));
                    break;
                case 0xe2:
                    t = string.Format("sep #$%.2x              ", op8(op0));
                    break;
                case 0xe3:
                    t = string.Format("sbc $%.2x,s     [%.6x]", op8(op0), decode((byte)OPTYPE.SR, op8(op0)));
                    break;
                case 0xe4:
                    t = string.Format("cpx $%.2x       [%.6x]", op8(op0), decode((byte)OPTYPE.DP, op8(op0)));
                    break;
                case 0xe5:
                    t = string.Format("sbc $%.2x       [%.6x]", op8(op0), decode((byte)OPTYPE.DP, op8(op0)));
                    break;
                case 0xe6:
                    t = string.Format("inc $%.2x       [%.6x]", op8(op0), decode((byte)OPTYPE.DP, op8(op0)));
                    break;
                case 0xe7:
                    t = string.Format("sbc [$%.2x]     [%.6x]", op8(op0), decode((byte)OPTYPE.ILDP, op8(op0)));
                    break;
                case 0xe8:
                    t = string.Format("inx                   ");
                    break;
                case 0xe9:
                    if (a8(regs))
                    {
                        t = string.Format("sbc #$%.2x              ", op8(op0));
                    }
                    else
                    {
                        t = string.Format("sbc #$%.4x            ", op16(op0, op1));
                    }
                    break;
                case 0xea:
                    t = string.Format("nop                   ");
                    break;
                case 0xeb:
                    t = string.Format("xba                   ");
                    break;
                case 0xec:
                    t = string.Format("cpx $%.4x     [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDR, op16(op0, op1)));
                    break;
                case 0xed:
                    t = string.Format("sbc $%.4x     [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDR, op16(op0, op1)));
                    break;
                case 0xee:
                    t = string.Format("inc $%.4x     [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDR, op16(op0, op1)));
                    break;
                case 0xef:
                    t = string.Format("sbc $%.6x   [%.6x]", op24(op0, op1, op2), decode((byte)OPTYPE.LONG, op24(op0, op1, op2)));
                    break;
                case 0xf0:
                    t = string.Format("beq $%.4x     [%.6x]", (ushort)(decode((byte)OPTYPE.RELB, op8(op0))), decode((byte)OPTYPE.RELB, op8(op0)));
                    break;
                case 0xf1:
                    t = string.Format("sbc ($%.2x),y   [%.6x]", op8(op0), decode((byte)OPTYPE.IDPY, op8(op0)));
                    break;
                case 0xf2:
                    t = string.Format("sbc ($%.2x)     [%.6x]", op8(op0), decode((byte)OPTYPE.IDP, op8(op0)));
                    break;
                case 0xf3:
                    t = string.Format("sbc ($%.2x,s),y [%.6x]", op8(op0), decode((byte)OPTYPE.ISRY, op8(op0)));
                    break;
                case 0xf4:
                    t = string.Format("pea $%.4x     [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDR, op16(op0, op1)));
                    break;
                case 0xf5:
                    t = string.Format("sbc $%.2x,x     [%.6x]", op8(op0), decode((byte)OPTYPE.DPX, op8(op0)));
                    break;
                case 0xf6:
                    t = string.Format("inc $%.2x,x     [%.6x]", op8(op0), decode((byte)OPTYPE.DPX, op8(op0)));
                    break;
                case 0xf7:
                    t = string.Format("sbc [$%.2x],y   [%.6x]", op8(op0), decode((byte)OPTYPE.ILDPY, op8(op0)));
                    break;
                case 0xf8:
                    t = string.Format("sed                   ");
                    break;
                case 0xf9:
                    t = string.Format("sbc $%.4x,y   [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDRY, op16(op0, op1)));
                    break;
                case 0xfa:
                    t = string.Format("plx                   ");
                    break;
                case 0xfb:
                    t = string.Format("xce                   ");
                    break;
                case 0xfc:
                    t = string.Format("jsr ($%.4x,x) [%.6x]", op16(op0, op1), decode((byte)OPTYPE.IADDRX, op16(op0, op1)));
                    break;
                case 0xfd:
                    t = string.Format("sbc $%.4x,x   [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDRX, op16(op0, op1)));
                    break;
                case 0xfe:
                    t = string.Format("inc $%.4x,x   [%.6x]", op16(op0, op1), decode((byte)OPTYPE.ADDRX, op16(op0, op1)));
                    break;
                case 0xff:
                    t = string.Format("sbc $%.6x,x [%.6x]", op24(op0, op1, op2), decode((byte)OPTYPE.LONGX, op24(op0, op1, op2)));
                    break;
            }

            s += t;
            s += " ";

            t = string.Format("A:%.4x X:%.4x Y:%.4x S:%.4x D:%.4x DB:%.2x ",
              regs.a.w, regs.x.w, regs.y.w, regs.s.w, regs.d.w, regs.db);
            s += t;

            if (regs.e)
            {
                t = string.Format("%c%c%c%c%c%c%c%c",
                  regs.p.n ? 'N' : 'n', regs.p.v ? 'V' : 'v',
                  regs.p.m ? '1' : '0', regs.p.x ? 'B' : 'b',
                  regs.p.d ? 'D' : 'd', regs.p.i ? 'I' : 'i',
                  regs.p.z ? 'Z' : 'z', regs.p.c ? 'C' : 'c');
            }
            else
            {
                t = string.Format("%c%c%c%c%c%c%c%c",
                  regs.p.n ? 'N' : 'n', regs.p.v ? 'V' : 'v',
                  regs.p.m ? 'M' : 'm', regs.p.x ? 'X' : 'x',
                  regs.p.d ? 'D' : 'd', regs.p.i ? 'I' : 'i',
                  regs.p.z ? 'Z' : 'z', regs.p.c ? 'C' : 'c');
            }

            s += t;
            s += " ";

            t = string.Format("V:%3d H:%4d", CPU.cpu.PPUCounter.vcounter(), CPU.cpu.PPUCounter.hcounter());
            s += t;
        }

        public byte dreadb(uint addr)
        {
            if ((addr & 0x40ffff) >= 0x2000 && (addr & 0x40ffff) <= 0x5fff)
            {
                //$[00-3f|80-bf]:[2000-5fff]
                //do not read MMIO registers within debugger
                return 0x00;
            }
            return Bus.bus.read(new uint24(addr));
        }

        public ushort dreadw(uint addr)
        {
            ushort r;
            r = (ushort)(dreadb((addr + 0) & 0xffffff) << 0);
            r |= (ushort)(dreadb((addr + 1) & 0xffffff) << 8);
            return r;
        }

        public uint dreadl(uint addr)
        {
            uint r;
            r = (uint)(dreadb((addr + 0) & 0xffffff) << 0);
            r |= (uint)(dreadb((addr + 1) & 0xffffff) << 8);
            r |= (uint)(dreadb((addr + 2) & 0xffffff) << 16);
            return r;
        }

        public uint decode(byte offset_type, uint addr)
        {
            uint r = 0;

            switch ((OPTYPE)offset_type)
            {
                case OPTYPE.DP:
                    r = (regs.d + (addr & 0xffff)) & 0xffff;
                    break;
                case OPTYPE.DPX:
                    r = ((uint)regs.d + (uint)regs.x + (addr & 0xffff)) & 0xffff;
                    break;
                case OPTYPE.DPY:
                    r = ((uint)regs.d + (uint)regs.y + (addr & 0xffff)) & 0xffff;
                    break;
                case OPTYPE.IDP:
                    addr = (regs.d + (addr & 0xffff)) & 0xffff;
                    r = (uint)((regs.db << 16) + dreadw(addr));
                    break;
                case OPTYPE.IDPX:
                    addr = ((uint)regs.d + (uint)regs.x + (addr & 0xffff)) & 0xffff;
                    r = (uint)((regs.db << 16) + dreadw(addr));
                    break;
                case OPTYPE.IDPY:
                    addr = (regs.d + (addr & 0xffff)) & 0xffff;
                    r = (uint)((regs.db << 16) + dreadw(addr) + (uint)regs.y);
                    break;
                case OPTYPE.ILDP:
                    addr = (regs.d + (addr & 0xffff)) & 0xffff;
                    r = dreadl(addr);
                    break;
                case OPTYPE.ILDPY:
                    addr = (regs.d + (addr & 0xffff)) & 0xffff;
                    r = dreadl(addr) + (uint)regs.y;
                    break;
                case OPTYPE.ADDR:
                    r = (uint)((regs.db << 16) + (addr & 0xffff));
                    break;
                case OPTYPE.ADDR_PC:
                    r = (uint)((regs.pc.b << 16) + (addr & 0xffff));
                    break;
                case OPTYPE.ADDRX:
                    r = (uint)((regs.db << 16) + (addr & 0xffff) + (uint)regs.x);
                    break;
                case OPTYPE.ADDRY:
                    r = (uint)((regs.db << 16) + (addr & 0xffff) + (uint)regs.y);
                    break;
                case OPTYPE.IADDR_PC:
                    r = (uint)((regs.pc.b << 16) + (addr & 0xffff));
                    break;
                case OPTYPE.IADDRX:
                    r = (uint)((regs.pc.b << 16) + ((addr + (uint)regs.x) & 0xffff));
                    break;
                case OPTYPE.ILADDR:
                    r = addr;
                    break;
                case OPTYPE.LONG:
                    r = addr;
                    break;
                case OPTYPE.LONGX:
                    r = (addr + (uint)regs.x);
                    break;
                case OPTYPE.SR:
                    r = (regs.s + (addr & 0xff)) & 0xffff;
                    break;
                case OPTYPE.ISRY:
                    addr = (regs.s + (addr & 0xff)) & 0xffff;
                    r = (uint)((regs.db << 16) + dreadw(addr) + (uint)regs.y);
                    break;
                case OPTYPE.RELB:
                    r = (uint)((regs.pc.b << 16) + ((regs.pc.w + 2) & 0xffff));
                    r += (uint)((sbyte)(addr));
                    break;
                case OPTYPE.RELW:
                    r = (uint)((regs.pc.b << 16) + ((regs.pc.w + 3) & 0xffff));
                    r += (uint)((short)(addr));
                    break;
            }

            return (r & 0xffffff);
        }

        private static readonly byte[] op_len_tbl = new byte[256] 
		{
			//0,1,2,3,  4,5,6,7,  8,9,a,b,  c,d,e,f

			2,2,2,2,  2,2,2,2,  1,5,1,1,  3,3,3,4, //0x0n
			2,2,2,2,  2,2,2,2,  1,3,1,1,  3,3,3,4, //0x1n
			3,2,4,2,  2,2,2,2,  1,5,1,1,  3,3,3,4, //0x2n
			2,2,2,2,  2,2,2,2,  1,3,1,1,  3,3,3,4, //0x3n

			1,2,2,2,  3,2,2,2,  1,5,1,1,  3,3,3,4, //0x4n
			2,2,2,2,  3,2,2,2,  1,3,1,1,  4,3,3,4, //0x5n
			1,2,3,2,  2,2,2,2,  1,5,1,1,  3,3,3,4, //0x6n
			2,2,2,2,  2,2,2,2,  1,3,1,1,  3,3,3,4, //0x7n

			2,2,3,2,  2,2,2,2,  1,5,1,1,  3,3,3,4, //0x8n
			2,2,2,2,  2,2,2,2,  1,3,1,1,  3,3,3,4, //0x9n
			6,2,6,2,  2,2,2,2,  1,5,1,1,  3,3,3,4, //0xan
			2,2,2,2,  2,2,2,2,  1,3,1,1,  3,3,3,4, //0xbn

			6,2,2,2,  2,2,2,2,  1,5,1,1,  3,3,3,4, //0xcn
			2,2,2,2,  2,2,2,2,  1,3,1,1,  3,3,3,4, //0xdn
			6,2,2,2,  2,2,2,2,  1,5,1,1,  3,3,3,4, //0xen
			2,2,2,2,  3,2,2,2,  1,3,1,1,  3,3,3,4  //0xfn
		};

        public byte opcode_length()
        {
            byte op, len;

            op = dreadb(regs.pc.d);
            len = op_len_tbl[op];
            if (len == 5)
            {
                return (byte)((regs.e || regs.p.m) ? 2 : 3);
            }
            if (len == 6)
            {
                return (byte)((regs.e || regs.p.x) ? 2 : 3);
            }
            return len;
        }

        public Regs regs = new Regs();
        public Reg24 aa = new Reg24();
        public Reg24 rd = new Reg24();
        public byte sp, dp;

        public abstract void op_io();
        public abstract byte op_read(uint addr);
        public abstract void op_write(uint addr, byte data);
        public abstract void last_cycle();
        public abstract bool interrupt_pending();

        public void op_io_irq()
        {
            if (interrupt_pending())
            {
                //modify I/O cycle to bus read cycle, do not increment PC
                op_read(regs.pc.d);
            }
            else
            {
                op_io();
            }
        }

        public void op_io_cond2()
        {
            if (regs.d.l != 0x00)
            {
                op_io();
            }
        }

        public void op_io_cond4(ushort x, ushort y)
        {
            if (!regs.p.x || (x & 0xff00) != (y & 0xff00))
            {
                op_io();
            }
        }

        public void op_io_cond6(ushort addr)
        {
            if (regs.e && (regs.pc.w & 0xff00) != (addr & 0xff00))
            {
                op_io();
            }
        }

        public void op_adc_b()
        {
            int result;

            if (!regs.p.d)
            {
                result = regs.a.l + rd.l + Convert.ToInt32(regs.p.c);
            }
            else
            {
                result = (regs.a.l & 0x0f) + (rd.l & 0x0f) + (Convert.ToInt32(regs.p.c) << 0);
                if (result > 0x09)
                {
                    result += 0x06;
                }
                regs.p.c = result > 0x0f;
                result = (regs.a.l & 0xf0) + (rd.l & 0xf0) + (Convert.ToInt32(regs.p.c) << 4) + (result & 0x0f);
            }

            regs.p.v = Convert.ToBoolean(~(regs.a.l ^ rd.l) & (regs.a.l ^ result) & 0x80);
            if (regs.p.d && result > 0x9f)
            {
                result += 0x60;
            }
            regs.p.c = result > 0xff;
            regs.p.n = Convert.ToBoolean(result & 0x80);
            regs.p.z = (byte)result == 0;

            regs.a.l = (byte)result;
        }

        public void op_adc_w()
        {
            int result;

            if (!regs.p.d)
            {
                result = regs.a.w + rd.w + Convert.ToInt32(regs.p.c);
            }
            else
            {
                result = (regs.a.w & 0x000f) + (rd.w & 0x000f) + (Convert.ToInt32(regs.p.c) << 0);
                if (result > 0x0009)
                {
                    result += 0x0006;
                }
                regs.p.c = result > 0x000f;
                result = (regs.a.w & 0x00f0) + (rd.w & 0x00f0) + (Convert.ToInt32(regs.p.c) << 4) + (result & 0x000f);
                if (result > 0x009f)
                {
                    result += 0x0060;
                }
                regs.p.c = result > 0x00ff;
                result = (regs.a.w & 0x0f00) + (rd.w & 0x0f00) + (Convert.ToInt32(regs.p.c) << 8) + (result & 0x00ff);
                if (result > 0x09ff)
                {
                    result += 0x0600;
                }
                regs.p.c = result > 0x0fff;
                result = (regs.a.w & 0xf000) + (rd.w & 0xf000) + (Convert.ToInt32(regs.p.c) << 12) + (result & 0x0fff);
            }

            regs.p.v = Convert.ToBoolean(~(regs.a.w ^ rd.w) & (regs.a.w ^ result) & 0x8000);
            if (regs.p.d && result > 0x9fff) result += 0x6000;
            regs.p.c = result > 0xffff;
            regs.p.n = Convert.ToBoolean(result & 0x8000);
            regs.p.z = (ushort)result == 0;

            regs.a.w = (ushort)result;
        }

        public void op_and_b()
        {
            regs.a.l &= rd.l;
            regs.p.n = Convert.ToBoolean(regs.a.l & 0x80);
            regs.p.z = regs.a.l == 0;
        }

        public void op_and_w()
        {
            regs.a.w &= rd.w;
            regs.p.n = Convert.ToBoolean(regs.a.w & 0x8000);
            regs.p.z = regs.a.w == 0;
        }

        public void op_bit_b()
        {
            regs.p.n = Convert.ToBoolean(rd.l & 0x80);
            regs.p.v = Convert.ToBoolean(rd.l & 0x40);
            regs.p.z = (rd.l & regs.a.l) == 0;
        }

        public void op_bit_w()
        {
            regs.p.n = Convert.ToBoolean(rd.w & 0x8000);
            regs.p.v = Convert.ToBoolean(rd.w & 0x4000);
            regs.p.z = (rd.w & regs.a.w) == 0;
        }

        public void op_cmp_b()
        {
            int r = regs.a.l - rd.l;
            regs.p.n = Convert.ToBoolean(r & 0x80);
            regs.p.z = (byte)r == 0;
            regs.p.c = r >= 0;
        }

        public void op_cmp_w()
        {
            int r = regs.a.w - rd.w;
            regs.p.n = Convert.ToBoolean(r & 0x8000);
            regs.p.z = (ushort)r == 0;
            regs.p.c = r >= 0;
        }

        public void op_cpx_b()
        {
            int r = regs.x.l - rd.l;
            regs.p.n = Convert.ToBoolean(r & 0x80);
            regs.p.z = (byte)r == 0;
            regs.p.c = r >= 0;
        }

        public void op_cpx_w()
        {
            int r = regs.x.w - rd.w;
            regs.p.n = Convert.ToBoolean(r & 0x8000);
            regs.p.z = (ushort)r == 0;
            regs.p.c = r >= 0;
        }

        public void op_cpy_b()
        {
            int r = regs.y.l - rd.l;
            regs.p.n = Convert.ToBoolean(r & 0x80);
            regs.p.z = (byte)r == 0;
            regs.p.c = r >= 0;
        }

        public void op_cpy_w()
        {
            int r = regs.y.w - rd.w;
            regs.p.n = Convert.ToBoolean(r & 0x8000);
            regs.p.z = (ushort)r == 0;
            regs.p.c = r >= 0;
        }

        public void op_eor_b()
        {
            regs.a.l ^= rd.l;
            regs.p.n = Convert.ToBoolean(regs.a.l & 0x80);
            regs.p.z = regs.a.l == 0;
        }

        public void op_eor_w()
        {
            regs.a.w ^= rd.w;
            regs.p.n = Convert.ToBoolean(regs.a.w & 0x8000);
            regs.p.z = regs.a.w == 0;
        }

        public void op_lda_b()
        {
            regs.a.l = rd.l;
            regs.p.n = Convert.ToBoolean(regs.a.l & 0x80);
            regs.p.z = regs.a.l == 0;
        }

        public void op_lda_w()
        {
            regs.a.w = rd.w;
            regs.p.n = Convert.ToBoolean(regs.a.w & 0x8000);
            regs.p.z = regs.a.w == 0;
        }

        public void op_ldx_b()
        {
            regs.x.l = rd.l;
            regs.p.n = Convert.ToBoolean(regs.x.l & 0x80);
            regs.p.z = regs.x.l == 0;
        }

        public void op_ldx_w()
        {
            regs.x.w = rd.w;
            regs.p.n = Convert.ToBoolean(regs.x.w & 0x8000);
            regs.p.z = regs.x.w == 0;
        }

        public void op_ldy_b()
        {
            regs.y.l = rd.l;
            regs.p.n = Convert.ToBoolean(regs.y.l & 0x80);
            regs.p.z = regs.y.l == 0;
        }

        public void op_ldy_w()
        {
            regs.y.w = rd.w;
            regs.p.n = Convert.ToBoolean(regs.y.w & 0x8000);
            regs.p.z = regs.y.w == 0;
        }

        public void op_ora_b()
        {
            regs.a.l |= rd.l;
            regs.p.n = Convert.ToBoolean(regs.a.l & 0x80);
            regs.p.z = regs.a.l == 0;
        }

        public void op_ora_w()
        {
            regs.a.w |= rd.w;
            regs.p.n = Convert.ToBoolean(regs.a.w & 0x8000);
            regs.p.z = regs.a.w == 0;
        }

        public void op_sbc_b()
        {
            int result;
            rd.l ^= 0xff;

            if (!regs.p.d)
            {
                result = regs.a.l + rd.l + Convert.ToInt32(regs.p.c);
            }
            else
            {
                result = (regs.a.l & 0x0f) + (rd.l & 0x0f) + (Convert.ToInt32(regs.p.c) << 0);
                if (result <= 0x0f) result -= 0x06;
                regs.p.c = result > 0x0f;
                result = (regs.a.l & 0xf0) + (rd.l & 0xf0) + (Convert.ToInt32(regs.p.c) << 4) + (result & 0x0f);
            }

            regs.p.v = Convert.ToBoolean(~(regs.a.l ^ rd.l) & (regs.a.l ^ result) & 0x80);
            if (regs.p.d && result <= 0xff) result -= 0x60;
            regs.p.c = result > 0xff;
            regs.p.n = Convert.ToBoolean(result & 0x80);
            regs.p.z = (byte)result == 0;

            regs.a.l = (byte)result;
        }

        public void op_sbc_w()
        {
            int result;
            rd.w ^= 0xffff;

            if (!regs.p.d)
            {
                result = regs.a.w + rd.w + Convert.ToInt32(regs.p.c);
            }
            else
            {
                result = (regs.a.w & 0x000f) + (rd.w & 0x000f) + (Convert.ToInt32(regs.p.c) << 0);
                if (result <= 0x000f) result -= 0x0006;
                regs.p.c = result > 0x000f;
                result = (regs.a.w & 0x00f0) + (rd.w & 0x00f0) + (Convert.ToInt32(regs.p.c) << 4) + (result & 0x000f);
                if (result <= 0x00ff) result -= 0x0060;
                regs.p.c = result > 0x00ff;
                result = (regs.a.w & 0x0f00) + (rd.w & 0x0f00) + (Convert.ToInt32(regs.p.c) << 8) + (result & 0x00ff);
                if (result <= 0x0fff) result -= 0x0600;
                regs.p.c = result > 0x0fff;
                result = (regs.a.w & 0xf000) + (rd.w & 0xf000) + (Convert.ToInt32(regs.p.c) << 12) + (result & 0x0fff);
            }

            regs.p.v = Convert.ToBoolean(~(regs.a.w ^ rd.w) & (regs.a.w ^ result) & 0x8000);
            if (regs.p.d && result <= 0xffff) result -= 0x6000;
            regs.p.c = result > 0xffff;
            regs.p.n = Convert.ToBoolean(result & 0x8000);
            regs.p.z = (ushort)result == 0;

            regs.a.w = (ushort)result;
        }

        public void op_inc_b()
        {
            rd.l++;
            regs.p.n = Convert.ToBoolean(rd.l & 0x80);
            regs.p.z = rd.l == 0;
        }

        public void op_inc_w()
        {
            rd.w++;
            regs.p.n = Convert.ToBoolean(rd.w & 0x8000);
            regs.p.z = rd.w == 0;
        }

        public void op_dec_b()
        {
            rd.l--;
            regs.p.n = Convert.ToBoolean(rd.l & 0x80);
            regs.p.z = rd.l == 0;
        }

        public void op_dec_w()
        {
            rd.w--;
            regs.p.n = Convert.ToBoolean(rd.w & 0x8000);
            regs.p.z = rd.w == 0;
        }

        public void op_asl_b()
        {
            regs.p.c = Convert.ToBoolean(rd.l & 0x80);
            rd.l <<= 1;
            regs.p.n = Convert.ToBoolean(rd.l & 0x80);
            regs.p.z = rd.l == 0;
        }

        public void op_asl_w()
        {
            regs.p.c = Convert.ToBoolean(rd.w & 0x8000);
            rd.w <<= 1;
            regs.p.n = Convert.ToBoolean(rd.w & 0x8000);
            regs.p.z = rd.w == 0;
        }

        public void op_lsr_b()
        {
            regs.p.c = Convert.ToBoolean(rd.l & 1);
            rd.l >>= 1;
            regs.p.n = Convert.ToBoolean(rd.l & 0x80);
            regs.p.z = rd.l == 0;
        }

        public void op_lsr_w()
        {
            regs.p.c = Convert.ToBoolean(rd.w & 1);
            rd.w >>= 1;
            regs.p.n = Convert.ToBoolean(rd.w & 0x8000);
            regs.p.z = rd.w == 0;
        }

        public void op_rol_b()
        {
            uint carry = Convert.ToUInt32(regs.p.c);
            regs.p.c = Convert.ToBoolean(rd.l & 0x80);
            rd.l = (byte)((uint)(rd.l << 1) | carry);
            regs.p.n = Convert.ToBoolean(rd.l & 0x80);
            regs.p.z = rd.l == 0;
        }

        public void op_rol_w()
        {
            uint carry = Convert.ToUInt32(regs.p.c);
            regs.p.c = Convert.ToBoolean(rd.w & 0x8000);
            rd.w = (ushort)((uint)(rd.w << 1) | carry);
            regs.p.n = Convert.ToBoolean(rd.w & 0x8000);
            regs.p.z = rd.w == 0;
        }

        public void op_ror_b()
        {
            uint carry = Convert.ToUInt32(regs.p.c) << 7;
            regs.p.c = Convert.ToBoolean(rd.l & 1);
            rd.l = (byte)(carry | (uint)(rd.l >> 1));
            regs.p.n = Convert.ToBoolean(rd.l & 0x80);
            regs.p.z = rd.l == 0;
        }

        public void op_ror_w()
        {
            uint carry = Convert.ToUInt32(regs.p.c) << 15;
            regs.p.c = Convert.ToBoolean(rd.w & 1);
            rd.w = (ushort)(carry | (uint)(rd.w >> 1));
            regs.p.n = Convert.ToBoolean(rd.w & 0x8000);
            regs.p.z = rd.w == 0;
        }

        public void op_trb_b()
        {
            regs.p.z = (rd.l & regs.a.l) == 0;
            rd.l &= (byte)(~regs.a.l);
        }

        public void op_trb_w()
        {
            regs.p.z = (rd.w & regs.a.w) == 0;
            rd.w &= (ushort)(~regs.a.w);
        }

        public void op_tsb_b()
        {
            regs.p.z = (rd.l & regs.a.l) == 0;
            rd.l |= regs.a.l;
        }

        public void op_tsb_w()
        {
            regs.p.z = (rd.w & regs.a.w) == 0;
            rd.w |= regs.a.w;
        }

        public void op_read_const_b(CPUCoreOperation op)
        {
            rd.l = op_readpc();
            op.Invoke();
        }

        public void op_read_const_w(CPUCoreOperation op)
        {
            rd.l = op_readpc();
            rd.h = op_readpc();
            op.Invoke();
        }

        public void op_read_bit_const_b()
        {
            rd.l = op_readpc();
            regs.p.z = ((rd.l & regs.a.l) == 0);
        }

        public void op_read_bit_const_w()
        {
            rd.l = op_readpc();
            rd.h = op_readpc();
            regs.p.z = ((rd.w & regs.a.w) == 0);
        }

        public void op_read_addr_b(CPUCoreOperation op)
        {
            aa.l = op_readpc();
            aa.h = op_readpc();
            rd.l = op_readdbr(aa.w);
            op.Invoke();
        }

        public void op_read_addr_w(CPUCoreOperation op)
        {
            aa.l = op_readpc();
            aa.h = op_readpc();
            rd.l = op_readdbr(aa.w + 0U);
            rd.h = op_readdbr(aa.w + 1U);
            op.Invoke();
        }

        public void op_read_addrx_b(CPUCoreOperation op)
        {
            aa.l = op_readpc();
            aa.h = op_readpc();
            op_io_cond4(aa.w, (ushort)(aa.w + regs.x.w));
            rd.l = op_readdbr((uint)(aa.w + regs.x.w));
            op.Invoke();
        }

        public void op_read_addrx_w(CPUCoreOperation op)
        {
            aa.l = op_readpc();
            aa.h = op_readpc();
            op_io_cond4(aa.w, (ushort)(aa.w + regs.x.w));
            rd.l = op_readdbr((uint)(aa.w + regs.x.w + 0));
            rd.h = op_readdbr((uint)(aa.w + regs.x.w + 1));
            op.Invoke();
        }

        public void op_read_addry_b(CPUCoreOperation op)
        {
            aa.l = op_readpc();
            aa.h = op_readpc();
            op_io_cond4(aa.w, (ushort)(aa.w + regs.y.w));
            rd.l = op_readdbr((uint)(aa.w + regs.y.w));
            op.Invoke();
        }

        public void op_read_addry_w(CPUCoreOperation op)
        {
            aa.l = op_readpc();
            aa.h = op_readpc();
            op_io_cond4(aa.w, (ushort)(aa.w + regs.y.w));
            rd.l = op_readdbr((uint)(aa.w + regs.y.w + 0));
            rd.h = op_readdbr((uint)(aa.w + regs.y.w + 1));
            op.Invoke();
        }

        public void op_read_long_b(CPUCoreOperation op)
        {
            aa.l = op_readpc();
            aa.h = op_readpc();
            aa.b = op_readpc();
            rd.l = op_readlong(aa.d);
            op.Invoke();
        }

        public void op_read_long_w(CPUCoreOperation op)
        {
            aa.l = op_readpc();
            aa.h = op_readpc();
            aa.b = op_readpc();
            rd.l = op_readlong(aa.d + 0);
            rd.h = op_readlong(aa.d + 1);
            op.Invoke();
        }

        public void op_read_longx_b(CPUCoreOperation op)
        {
            aa.l = op_readpc();
            aa.h = op_readpc();
            aa.b = op_readpc();
            rd.l = op_readlong(aa.d + regs.x.w);
            op.Invoke();
        }

        public void op_read_longx_w(CPUCoreOperation op)
        {
            aa.l = op_readpc();
            aa.h = op_readpc();
            aa.b = op_readpc();
            rd.l = op_readlong(aa.d + regs.x.w + 0);
            rd.h = op_readlong(aa.d + regs.x.w + 1);
            op.Invoke();
        }

        public void op_read_dp_b(CPUCoreOperation op)
        {
            dp = op_readpc();
            op_io_cond2();
            rd.l = op_readdp(dp);
            op.Invoke();
        }

        public void op_read_dp_w(CPUCoreOperation op)
        {
            dp = op_readpc();
            op_io_cond2();
            rd.l = op_readdp(dp + 0U);
            rd.h = op_readdp(dp + 1U);
            op.Invoke();
        }

        public void op_read_dpr_b(CPUCoreOperation op, int n)
        {
            dp = op_readpc();
            op_io_cond2();
            op_io();
            rd.l = op_readdp((uint)(dp + regs.r[n].w));
            op.Invoke();
        }

        public void op_read_dpr_w(CPUCoreOperation op, int n)
        {
            dp = op_readpc();
            op_io_cond2();
            op_io();
            rd.l = op_readdp((uint)(dp + regs.r[n].w + 0));
            rd.h = op_readdp((uint)(dp + regs.r[n].w + 1));
            op.Invoke();
        }

        public void op_read_idp_b(CPUCoreOperation op)
        {
            dp = op_readpc();
            op_io_cond2();
            aa.l = op_readdp(dp + 0U);
            aa.h = op_readdp(dp + 1U);
            rd.l = op_readdbr(aa.w);
            op.Invoke();
        }

        public void op_read_idp_w(CPUCoreOperation op)
        {
            dp = op_readpc();
            op_io_cond2();
            aa.l = op_readdp(dp + 0U);
            aa.h = op_readdp(dp + 1U);
            rd.l = op_readdbr(aa.w + 0U);
            rd.h = op_readdbr(aa.w + 1U);
            op.Invoke();
        }

        public void op_read_idpx_b(CPUCoreOperation op)
        {
            dp = op_readpc();
            op_io_cond2();
            op_io();
            aa.l = op_readdp((uint)(dp + regs.x.w + 0));
            aa.h = op_readdp((uint)(dp + regs.x.w + 1));
            rd.l = op_readdbr(aa.w);
            op.Invoke();
        }

        public void op_read_idpx_w(CPUCoreOperation op)
        {
            dp = op_readpc();
            op_io_cond2();
            op_io();
            aa.l = op_readdp((uint)(dp + regs.x.w + 0));
            aa.h = op_readdp((uint)(dp + regs.x.w + 1));
            rd.l = op_readdbr(aa.w + 0U);
            rd.h = op_readdbr(aa.w + 1U);
            op.Invoke();
        }

        public void op_read_idpy_b(CPUCoreOperation op)
        {
            dp = op_readpc();
            op_io_cond2();
            aa.l = op_readdp(dp + 0U);
            aa.h = op_readdp(dp + 1U);
            op_io_cond4(aa.w, (ushort)(aa.w + regs.y.w));
            rd.l = op_readdbr((uint)(aa.w + regs.y.w));
            op.Invoke();
        }

        public void op_read_idpy_w(CPUCoreOperation op)
        {
            dp = op_readpc();
            op_io_cond2();
            aa.l = op_readdp(dp + 0U);
            aa.h = op_readdp(dp + 1U);
            op_io_cond4(aa.w, (ushort)(aa.w + regs.y.w));
            rd.l = op_readdbr((uint)(aa.w + regs.y.w + 0));
            rd.h = op_readdbr((uint)(aa.w + regs.y.w + 1));
            op.Invoke();
        }

        public void op_read_ildp_b(CPUCoreOperation op)
        {
            dp = op_readpc();
            op_io_cond2();
            aa.l = op_readdp(dp + 0U);
            aa.h = op_readdp(dp + 1U);
            aa.b = op_readdp(dp + 2U);
            rd.l = op_readlong(aa.d);
            op.Invoke();
        }

        public void op_read_ildp_w(CPUCoreOperation op)
        {
            dp = op_readpc();
            op_io_cond2();
            aa.l = op_readdp(dp + 0U);
            aa.h = op_readdp(dp + 1U);
            aa.b = op_readdp(dp + 2U);
            rd.l = op_readlong(aa.d + 0);
            rd.h = op_readlong(aa.d + 1);
            op.Invoke();
        }

        public void op_read_ildpy_b(CPUCoreOperation op)
        {
            dp = op_readpc();
            op_io_cond2();
            aa.l = op_readdp(dp + 0U);
            aa.h = op_readdp(dp + 1U);
            aa.b = op_readdp(dp + 2U);
            rd.l = op_readlong(aa.d + regs.y.w);
            op.Invoke();
        }

        public void op_read_ildpy_w(CPUCoreOperation op)
        {
            dp = op_readpc();
            op_io_cond2();
            aa.l = op_readdp(dp + 0U);
            aa.h = op_readdp(dp + 1U);
            aa.b = op_readdp(dp + 2U);
            rd.l = op_readlong(aa.d + regs.y.w + 0);
            rd.h = op_readlong(aa.d + regs.y.w + 1);
            op.Invoke();
        }

        public void op_read_sr_b(CPUCoreOperation op)
        {
            sp = op_readpc();
            op_io();
            rd.l = op_readsp(sp);
            op.Invoke();
        }

        public void op_read_sr_w(CPUCoreOperation op)
        {
            sp = op_readpc();
            op_io();
            rd.l = op_readsp(sp + 0U);
            rd.h = op_readsp(sp + 1U);
            op.Invoke();
        }

        public void op_read_isry_b(CPUCoreOperation op)
        {
            sp = op_readpc();
            op_io();
            aa.l = op_readsp(sp + 0U);
            aa.h = op_readsp(sp + 1U);
            op_io();
            rd.l = op_readdbr((uint)(aa.w + regs.y.w));
            op.Invoke();
        }

        public void op_read_isry_w(CPUCoreOperation op)
        {
            sp = op_readpc();
            op_io();
            aa.l = op_readsp(sp + 0U);
            aa.h = op_readsp(sp + 1U);
            op_io();
            rd.l = op_readdbr((uint)(aa.w + regs.y.w + 0));
            rd.h = op_readdbr((uint)(aa.w + regs.y.w + 1));
            op.Invoke();
        }

        public void op_write_addr_b(int n)
        {
            aa.l = op_readpc();
            aa.h = op_readpc();
            op_writedbr(aa.w, (byte)regs.r[n]);
        }

        public void op_write_addr_w(int n)
        {
            aa.l = op_readpc();
            aa.h = op_readpc();
            op_writedbr(aa.w + 0U, (byte)(regs.r[n] >> 0));
            op_writedbr(aa.w + 1U, (byte)(regs.r[n] >> 8));
        }

        public void op_write_addrr_b(int n, int i)
        {
            aa.l = op_readpc();
            aa.h = op_readpc();
            op_io();
            op_writedbr(aa.w + (uint)regs.r[i], (byte)regs.r[n]);
        }

        public void op_write_addrr_w(int n, int i)
        {
            aa.l = op_readpc();
            aa.h = op_readpc();
            op_io();
            op_writedbr(aa.w + (uint)regs.r[i] + 0, (byte)(regs.r[n] >> 0));
            op_writedbr(aa.w + (uint)regs.r[i] + 1, (byte)(regs.r[n] >> 8));
        }

        public void op_write_longr_b(int i)
        {
            aa.l = op_readpc();
            aa.h = op_readpc();
            aa.b = op_readpc();
            op_writelong(aa.d + (uint)regs.r[i], regs.a.l);
        }

        public void op_write_longr_w(int i)
        {
            aa.l = op_readpc();
            aa.h = op_readpc();
            aa.b = op_readpc();
            op_writelong(aa.d + (uint)regs.r[i] + 0, regs.a.l);
            op_writelong(aa.d + (uint)regs.r[i] + 1, regs.a.h);
        }

        public void op_write_dp_b(int n)
        {
            dp = op_readpc();
            op_io_cond2();
            op_writedp(dp, (byte)regs.r[n]);
        }

        public void op_write_dp_w(int n)
        {
            dp = op_readpc();
            op_io_cond2();
            op_writedp(dp + 0U, (byte)(regs.r[n] >> 0));
            op_writedp(dp + 1U, (byte)(regs.r[n] >> 8));
        }

        public void op_write_dpr_b(int n, int i)
        {
            dp = op_readpc();
            op_io_cond2();
            op_io();
            op_writedp(dp + (uint)regs.r[i], (byte)regs.r[n]);
        }

        public void op_write_dpr_w(int n, int i)
        {
            dp = op_readpc();
            op_io_cond2();
            op_io();
            op_writedp(dp + (uint)regs.r[i] + 0, (byte)(regs.r[n] >> 0));
            op_writedp(dp + (uint)regs.r[i] + 1, (byte)(regs.r[n] >> 8));
        }

        public void op_sta_idp_b()
        {
            dp = op_readpc();
            op_io_cond2();
            aa.l = op_readdp(dp + 0U);
            aa.h = op_readdp(dp + 1U);
            op_writedbr(aa.w, regs.a.l);
        }

        public void op_sta_idp_w()
        {
            dp = op_readpc();
            op_io_cond2();
            aa.l = op_readdp(dp + 0U);
            aa.h = op_readdp(dp + 1U);
            op_writedbr(aa.w + 0U, regs.a.l);
            op_writedbr(aa.w + 1U, regs.a.h);
        }

        public void op_sta_ildp_b()
        {
            dp = op_readpc();
            op_io_cond2();
            aa.l = op_readdp(dp + 0U);
            aa.h = op_readdp(dp + 1U);
            aa.b = op_readdp(dp + 2U);
            op_writelong(aa.d, regs.a.l);
        }

        public void op_sta_ildp_w()
        {
            dp = op_readpc();
            op_io_cond2();
            aa.l = op_readdp(dp + 0U);
            aa.h = op_readdp(dp + 1U);
            aa.b = op_readdp(dp + 2U);
            op_writelong(aa.d + 0, regs.a.l);
            op_writelong(aa.d + 1, regs.a.h);
        }

        public void op_sta_idpx_b()
        {
            dp = op_readpc();
            op_io_cond2();
            op_io();
            aa.l = op_readdp((uint)(dp + regs.x.w + 0));
            aa.h = op_readdp((uint)(dp + regs.x.w + 1));
            op_writedbr(aa.w, regs.a.l);
        }

        public void op_sta_idpx_w()
        {
            dp = op_readpc();
            op_io_cond2();
            op_io();
            aa.l = op_readdp((uint)(dp + regs.x.w + 0));
            aa.h = op_readdp((uint)(dp + regs.x.w + 1));
            op_writedbr(aa.w + 0U, regs.a.l);
            op_writedbr(aa.w + 1U, regs.a.h);
        }

        public void op_sta_idpy_b()
        {
            dp = op_readpc();
            op_io_cond2();
            aa.l = op_readdp(dp + 0U);
            aa.h = op_readdp(dp + 1U);
            op_io();
            op_writedbr((uint)(aa.w + regs.y.w), regs.a.l);
        }

        public void op_sta_idpy_w()
        {
            dp = op_readpc();
            op_io_cond2();
            aa.l = op_readdp(dp + 0U);
            aa.h = op_readdp(dp + 1U);
            op_io();
            op_writedbr((uint)(aa.w + regs.y.w + 0), regs.a.l);
            op_writedbr((uint)(aa.w + regs.y.w + 1), regs.a.h);
        }

        public void op_sta_ildpy_b()
        {
            dp = op_readpc();
            op_io_cond2();
            aa.l = op_readdp(dp + 0U);
            aa.h = op_readdp(dp + 1U);
            aa.b = op_readdp(dp + 2U);
            op_writelong(aa.d + regs.y.w, regs.a.l);
        }

        public void op_sta_ildpy_w()
        {
            dp = op_readpc();
            op_io_cond2();
            aa.l = op_readdp(dp + 0U);
            aa.h = op_readdp(dp + 1U);
            aa.b = op_readdp(dp + 2U);
            op_writelong(aa.d + regs.y.w + 0, regs.a.l);
            op_writelong(aa.d + regs.y.w + 1, regs.a.h);
        }

        public void op_sta_sr_b()
        {
            sp = op_readpc();
            op_io();
            op_writesp(sp, regs.a.l);
        }

        public void op_sta_sr_w()
        {
            sp = op_readpc();
            op_io();
            op_writesp(sp + 0U, regs.a.l);
            op_writesp(sp + 1U, regs.a.h);
        }

        public void op_sta_isry_b()
        {
            sp = op_readpc();
            op_io();
            aa.l = op_readsp(sp + 0U);
            aa.h = op_readsp(sp + 1U);
            op_io();
            op_writedbr((uint)(aa.w + regs.y.w), regs.a.l);
        }

        public void op_sta_isry_w()
        {
            sp = op_readpc();
            op_io();
            aa.l = op_readsp(sp + 0U);
            aa.h = op_readsp(sp + 1U);
            op_io();
            op_writedbr((uint)(aa.w + regs.y.w + 0), regs.a.l);
            op_writedbr((uint)(aa.w + regs.y.w + 1), regs.a.h);
        }

        public void op_adjust_imm_b(int n, int adjust)
        {
            op_io_irq();
            regs.r[n].l += (byte)adjust;
            regs.p.n = Convert.ToBoolean(regs.r[n].l & 0x80);
            regs.p.z = (regs.r[n].l == 0);
        }

        public void op_adjust_imm_w(int n, int adjust)
        {
            op_io_irq();
            regs.r[n].w += (byte)adjust;
            regs.p.n = Convert.ToBoolean(regs.r[n].w & 0x8000);
            regs.p.z = (regs.r[n].w == 0);
        }

        public void op_asl_imm_b()
        {
            op_io_irq();
            regs.p.c = Convert.ToBoolean(regs.a.l & 0x80);
            regs.a.l <<= 1;
            regs.p.n = Convert.ToBoolean(regs.a.l & 0x80);
            regs.p.z = (regs.a.l == 0);
        }

        public void op_asl_imm_w()
        {
            op_io_irq();
            regs.p.c = Convert.ToBoolean(regs.a.w & 0x8000);
            regs.a.w <<= 1;
            regs.p.n = Convert.ToBoolean(regs.a.w & 0x8000);
            regs.p.z = (regs.a.w == 0);
        }

        public void op_lsr_imm_b()
        {
            op_io_irq();
            regs.p.c = Convert.ToBoolean(regs.a.l & 0x01);
            regs.a.l >>= 1;
            regs.p.n = Convert.ToBoolean(regs.a.l & 0x80);
            regs.p.z = (regs.a.l == 0);
        }

        public void op_lsr_imm_w()
        {
            op_io_irq();
            regs.p.c = Convert.ToBoolean(regs.a.w & 0x0001);
            regs.a.w >>= 1;
            regs.p.n = Convert.ToBoolean(regs.a.w & 0x8000);
            regs.p.z = (regs.a.w == 0);
        }

        public void op_rol_imm_b()
        {
            op_io_irq();
            bool carry = regs.p.c;
            regs.p.c = Convert.ToBoolean(regs.a.l & 0x80);
            regs.a.l = (byte)((regs.a.l << 1) | Convert.ToInt32(carry));
            regs.p.n = Convert.ToBoolean(regs.a.l & 0x80);
            regs.p.z = (regs.a.l == 0);
        }

        public void op_rol_imm_w()
        {
            op_io_irq();
            bool carry = regs.p.c;
            regs.p.c = Convert.ToBoolean(regs.a.w & 0x8000);
            regs.a.w = (byte)((regs.a.w << 1) | Convert.ToInt32(carry));
            regs.p.n = Convert.ToBoolean(regs.a.w & 0x8000);
            regs.p.z = (regs.a.w == 0);
        }

        public void op_ror_imm_b()
        {
            op_io_irq();
            bool carry = regs.p.c;
            regs.p.c = Convert.ToBoolean(regs.a.l & 0x01);
            regs.a.l = (byte)((Convert.ToInt32(carry) << 7) | (regs.a.l >> 1));
            regs.p.n = Convert.ToBoolean(regs.a.l & 0x80);
            regs.p.z = (regs.a.l == 0);
        }

        public void op_ror_imm_w()
        {
            op_io_irq();
            bool carry = regs.p.c;
            regs.p.c = Convert.ToBoolean(regs.a.w & 0x0001);
            regs.a.w = (byte)((Convert.ToInt32(carry) << 15) | (regs.a.w >> 1));
            regs.p.n = Convert.ToBoolean(regs.a.w & 0x8000);
            regs.p.z = (regs.a.w == 0);
        }

        public void op_adjust_addr_b(CPUCoreOperation op)
        {
            aa.l = op_readpc();
            aa.h = op_readpc();
            rd.l = op_readdbr(aa.w);
            op_io();
            op.Invoke();
            op_writedbr(aa.w, rd.l);
        }

        public void op_adjust_addr_w(CPUCoreOperation op)
        {
            aa.l = op_readpc();
            aa.h = op_readpc();
            rd.l = op_readdbr(aa.w + 0U);
            rd.h = op_readdbr(aa.w + 1U);
            op_io();
            op.Invoke();
            op_writedbr(aa.w + 1U, rd.h);
            op_writedbr(aa.w + 0U, rd.l);
        }

        public void op_adjust_addrx_b(CPUCoreOperation op)
        {
            aa.l = op_readpc();
            aa.h = op_readpc();
            op_io();
            rd.l = op_readdbr((uint)(aa.w + regs.x.w));
            op_io();
            op.Invoke();
            op_writedbr((uint)(aa.w + regs.x.w), rd.l);
        }

        public void op_adjust_addrx_w(CPUCoreOperation op)
        {
            aa.l = op_readpc();
            aa.h = op_readpc();
            op_io();
            rd.l = op_readdbr((uint)(aa.w + regs.x.w + 0));
            rd.h = op_readdbr((uint)(aa.w + regs.x.w + 1));
            op_io();
            op.Invoke();
            op_writedbr((uint)(aa.w + regs.x.w + 1), rd.h);
            op_writedbr((uint)(aa.w + regs.x.w + 0), rd.l);
        }

        public void op_adjust_dp_b(CPUCoreOperation op)
        {
            dp = op_readpc();
            op_io_cond2();
            rd.l = op_readdp(dp);
            op_io();
            op.Invoke();
            op_writedp(dp, rd.l);
        }

        public void op_adjust_dp_w(CPUCoreOperation op)
        {
            dp = op_readpc();
            op_io_cond2();
            rd.l = op_readdp(dp + 0U);
            rd.h = op_readdp(dp + 1U);
            op_io();
            op.Invoke();
            op_writedp(dp + 1U, rd.h);
            op_writedp(dp + 0U, rd.l);
        }

        public void op_adjust_dpx_b(CPUCoreOperation op)
        {
            dp = op_readpc();
            op_io_cond2();
            op_io();
            rd.l = op_readdp((uint)(dp + regs.x.w));
            op_io();
            op.Invoke();
            op_writedp((uint)(dp + regs.x.w), rd.l);
        }

        public void op_adjust_dpx_w(CPUCoreOperation op)
        {
            dp = op_readpc();
            op_io_cond2();
            op_io();
            rd.l = op_readdp((uint)(dp + regs.x.w + 0));
            rd.h = op_readdp((uint)(dp + regs.x.w + 1));
            op_io();
            op.Invoke();
            op_writedp((uint)(dp + regs.x.w + 1), rd.h);
            op_writedp((uint)(dp + regs.x.w + 0), rd.l);
        }

        public void op_branch(int bit, int val)
        {
            if (Bit.bit(regs.p & (uint)bit) != val)
            {
                rd.l = op_readpc();
            }
            else
            {
                rd.l = op_readpc();
                aa.w = (ushort)(regs.pc.d + (sbyte)rd.l);
                op_io_cond6(aa.w);
                op_io();
                regs.pc.w = aa.w;
            }
        }

        public void op_bra()
        {
            rd.l = op_readpc();
            aa.w = (ushort)(regs.pc.d + (sbyte)rd.l);
            op_io_cond6(aa.w);
            op_io();
            regs.pc.w = aa.w;
        }

        public void op_brl()
        {
            rd.l = op_readpc();
            rd.h = op_readpc();
            op_io();
            regs.pc.w = (ushort)(regs.pc.d + (short)rd.w);
        }

        public void op_jmp_addr()
        {
            rd.l = op_readpc();
            rd.h = op_readpc();
            regs.pc.w = rd.w;
        }

        public void op_jmp_long()
        {
            rd.l = op_readpc();
            rd.h = op_readpc();
            rd.b = op_readpc();
            regs.pc.d = rd.d & 0xffffff;
        }

        public void op_jmp_iaddr()
        {
            aa.l = op_readpc();
            aa.h = op_readpc();
            rd.l = op_readaddr(aa.w + 0U);
            rd.h = op_readaddr(aa.w + 1U);
            regs.pc.w = rd.w;
        }

        public void op_jmp_iaddrx()
        {
            aa.l = op_readpc();
            aa.h = op_readpc();
            op_io();
            rd.l = op_readpbr((uint)(aa.w + regs.x.w + 0));
            rd.h = op_readpbr((uint)(aa.w + regs.x.w + 1));
            regs.pc.w = rd.w;
        }

        public void op_jmp_iladdr()
        {
            aa.l = op_readpc();
            aa.h = op_readpc();
            rd.l = op_readaddr(aa.w + 0U);
            rd.h = op_readaddr(aa.w + 1U);
            rd.b = op_readaddr(aa.w + 2U);
            regs.pc.d = rd.d & 0xffffff;
        }

        public void op_jsr_addr()
        {
            aa.l = op_readpc();
            aa.h = op_readpc();
            op_io();
            regs.pc.w--;
            op_writestack(regs.pc.h);
            op_writestack(regs.pc.l);
            regs.pc.w = aa.w;
        }

        public void op_jsr_long_e()
        {
            aa.l = op_readpc();
            aa.h = op_readpc();
            op_writestackn(regs.pc.b);
            op_io();
            aa.b = op_readpc();
            regs.pc.w--;
            op_writestackn(regs.pc.h);
            op_writestackn(regs.pc.l);
            regs.pc.d = aa.d & 0xffffff;
            regs.s.h = 0x01;
        }

        public void op_jsr_long_n()
        {
            aa.l = op_readpc();
            aa.h = op_readpc();
            op_writestackn(regs.pc.b);
            op_io();
            aa.b = op_readpc();
            regs.pc.w--;
            op_writestackn(regs.pc.h);
            op_writestackn(regs.pc.l);
            regs.pc.d = aa.d & 0xffffff;
        }

        public void op_jsr_iaddrx_e()
        {
            aa.l = op_readpc();
            op_writestackn(regs.pc.h);
            op_writestackn(regs.pc.l);
            aa.h = op_readpc();
            op_io();
            rd.l = op_readpbr((uint)(aa.w + regs.x.w + 0));
            rd.h = op_readpbr((uint)(aa.w + regs.x.w + 1));
            regs.pc.w = rd.w;
            regs.s.h = 0x01;
        }

        public void op_jsr_iaddrx_n()
        {
            aa.l = op_readpc();
            op_writestackn(regs.pc.h);
            op_writestackn(regs.pc.l);
            aa.h = op_readpc();
            op_io();
            rd.l = op_readpbr((uint)(aa.w + regs.x.w + 0));
            rd.h = op_readpbr((uint)(aa.w + regs.x.w + 1));
            regs.pc.w = rd.w;
        }

        public void op_rti_e()
        {
            op_io();
            op_io();
            regs.p.Assign((byte)(op_readstack() | 0x30));
            rd.l = op_readstack();
            rd.h = op_readstack();
            regs.pc.w = rd.w;
        }

        public void op_rti_n()
        {
            op_io();
            op_io();
            regs.p.Assign(op_readstack());
            if (regs.p.x)
            {
                regs.x.h = 0x00;
                regs.y.h = 0x00;
            }
            rd.l = op_readstack();
            rd.h = op_readstack();
            rd.b = op_readstack();
            regs.pc.d = rd.d & 0xffffff;
            update_table();
        }

        public void op_rts()
        {
            op_io();
            op_io();
            rd.l = op_readstack();
            rd.h = op_readstack();
            op_io();
            regs.pc.w = ++rd.w;
        }

        public void op_rtl_e()
        {
            op_io();
            op_io();
            rd.l = op_readstackn();
            rd.h = op_readstackn();
            rd.b = op_readstackn();
            regs.pc.b = rd.b;
            regs.pc.w = ++rd.w;
            regs.s.h = 0x01;
        }

        public void op_rtl_n()
        {
            op_io();
            op_io();
            rd.l = op_readstackn();
            rd.h = op_readstackn();
            rd.b = op_readstackn();
            regs.pc.b = rd.b;
            regs.pc.w = ++rd.w;
        }

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

        public CPUCoreOperation[] opcode_table;
        public CPUCoreOperation[] op_table = new CPUCoreOperation[256 * 5];

        private void opA(byte id, CPUCoreOp op_name)
        {
            op_table[(int)Table.EM + id] = op_table[(int)Table.MX + id] = op_table[(int)Table.Mx + id] = op_table[(int)Table.mX + id] = op_table[(int)Table.mx + id] = new CPUCoreOperation(op_name, null);
        }

        private void opAII(byte id, CPUCoreOp op_name, int x, int y)
        {
            op_table[(int)Table.EM + id] = op_table[(int)Table.MX + id] = op_table[(int)Table.Mx + id] = op_table[(int)Table.mX + id] = op_table[(int)Table.mx + id] = new CPUCoreOperation(op_name, null);
        }

        private void opE(byte id, CPUCoreOp op_name_e, CPUCoreOp op_name_n)
        {
            op_table[(int)Table.EM + id] = new CPUCoreOperation(op_name_e, null);
            op_table[(int)Table.MX + id] = op_table[(int)Table.Mx + id] = op_table[(int)Table.mX + id] = op_table[(int)Table.mx + id] = new CPUCoreOperation(op_name_n, null);
        }

        private void opEI(byte id, CPUCoreOp op_name_e, CPUCoreOp op_name_n, int x)
        {
            op_table[(int)Table.EM + id] = new CPUCoreOperation(op_name_e, null);
            op_table[(int)Table.MX + id] = op_table[(int)Table.Mx + id] = op_table[(int)Table.mX + id] = op_table[(int)Table.mx + id] = new CPUCoreOperation(op_name_n, null);
        }

        private void opEII(byte id, CPUCoreOp op_name_e, CPUCoreOp op_name_n, int x, int y)
        {
            op_table[(int)Table.EM + id] = new CPUCoreOperation(op_name_e, null);
            op_table[(int)Table.MX + id] = op_table[(int)Table.Mx + id] = op_table[(int)Table.mX + id] = op_table[(int)Table.mx + id] = new CPUCoreOperation(op_name_n, null);
        }

        private void opM(byte id, CPUCoreOp op_name_b, CPUCoreOp op_name_w)
        {
            op_table[(int)Table.EM + id] = op_table[(int)Table.MX + id] = op_table[(int)Table.Mx + id] = new CPUCoreOperation(op_name_b, null);
            op_table[(int)Table.mX + id] = op_table[(int)Table.mx + id] = new CPUCoreOperation(op_name_w, null);
        }

        private void opMI(byte id, CPUCoreOp op_name_b, CPUCoreOp op_name_w, int x)
        {
            op_table[(int)Table.EM + id] = op_table[(int)Table.MX + id] = op_table[(int)Table.Mx + id] = new CPUCoreOperation(op_name_b, null);
            op_table[(int)Table.mX + id] = op_table[(int)Table.mx + id] = new CPUCoreOperation(op_name_w, null);
        }

        private void opMII(byte id, CPUCoreOp op_name_b, CPUCoreOp op_name_w, int x, int y)
        {
            op_table[(int)Table.EM + id] = op_table[(int)Table.MX + id] = op_table[(int)Table.Mx + id] = new CPUCoreOperation(op_name_b, null);
            op_table[(int)Table.mX + id] = op_table[(int)Table.mx + id] = new CPUCoreOperation(op_name_w, null);
        }

        private void opMF(byte id, CPUCoreOp op_name_b, CPUCoreOp op_fn_b, CPUCoreOp op_name_w, CPUCoreOp op_fn_w)
        {
            op_table[(int)Table.EM + id] = op_table[(int)Table.MX + id] = op_table[(int)Table.Mx + id] = new CPUCoreOperation(op_name_b, null);
            op_table[(int)Table.mX + id] = op_table[(int)Table.mx + id] = new CPUCoreOperation(op_name_w, null);
        }

        private void opMFI(byte id, CPUCoreOp op_name_b, CPUCoreOp op_fn_b, CPUCoreOp op_name_w, CPUCoreOp op_fn_w, int x, int y)
        {
            op_table[(int)Table.EM + id] = op_table[(int)Table.MX + id] = op_table[(int)Table.Mx + id] = new CPUCoreOperation(op_name_b, null);
            op_table[(int)Table.mX + id] = op_table[(int)Table.mx + id] = new CPUCoreOperation(op_name_w, null);
        }

        private void opX(byte id, CPUCoreOp op_name_b, CPUCoreOp op_name_w)
        {
            op_table[(int)Table.EM + id] = op_table[(int)Table.MX + id] = op_table[(int)Table.mX + id] = new CPUCoreOperation(op_name_b, null);
            op_table[(int)Table.Mx + id] = op_table[(int)Table.mx + id] = new CPUCoreOperation(op_name_w, null);
        }

        private void opXI(byte id, CPUCoreOp op_name_b, CPUCoreOp op_name_w, int x)
        {
            op_table[(int)Table.EM + id] = op_table[(int)Table.MX + id] = op_table[(int)Table.mX + id] = new CPUCoreOperation(op_name_b, null);
            op_table[(int)Table.Mx + id] = op_table[(int)Table.mx + id] = new CPUCoreOperation(op_name_w, null);
        }

        private void opXII(byte id, CPUCoreOp op_name_b, CPUCoreOp op_name_w, int x, int y)
        {
            op_table[(int)Table.EM + id] = op_table[(int)Table.MX + id] = op_table[(int)Table.mX + id] = new CPUCoreOperation(op_name_b, null);
            op_table[(int)Table.Mx + id] = op_table[(int)Table.mx + id] = new CPUCoreOperation(op_name_w, null);
        }

        private void opXF(byte id, CPUCoreOp op_name_b, CPUCoreOp op_fn_b, CPUCoreOp op_name_w, CPUCoreOp op_fn_w)
        {
            op_table[(int)Table.EM + id] = op_table[(int)Table.MX + id] = op_table[(int)Table.mX + id] = new CPUCoreOperation(op_name_b, null);
            op_table[(int)Table.Mx + id] = op_table[(int)Table.mx + id] = new CPUCoreOperation(op_name_w, null);
        }

        private void opXFI(byte id, CPUCoreOp op_name_b, CPUCoreOp op_fn_b, CPUCoreOp op_name_w, CPUCoreOp op_fn_w, int x)
        {
            op_table[(int)Table.EM + id] = op_table[(int)Table.MX + id] = op_table[(int)Table.mX + id] = new CPUCoreOperation(op_name_b, null);
            op_table[(int)Table.Mx + id] = op_table[(int)Table.mx + id] = new CPUCoreOperation(op_name_w, null);
        }

        public void initialize_opcode_table()
        {
            { throw new NotImplementedException(); }
        }

        public void update_table()
        {
            if (regs.e)
            {
                opcode_table = new ArraySegment<CPUCoreOperation>(op_table, (int)Table.EM, op_table.Length - (int)Table.EM).Array;
            }
            else if (regs.p.m)
            {
                if (regs.p.x)
                {
                    opcode_table = new ArraySegment<CPUCoreOperation>(op_table, (int)Table.MX, op_table.Length - (int)Table.MX).Array;
                }
                else
                {
                    opcode_table = new ArraySegment<CPUCoreOperation>(op_table, (int)Table.Mx, op_table.Length - (int)Table.Mx).Array;
                }
            }
            else
            {
                if (regs.p.x)
                {
                    opcode_table = new ArraySegment<CPUCoreOperation>(op_table, (int)Table.mX, op_table.Length - (int)Table.mX).Array;
                }
                else
                {
                    opcode_table = new ArraySegment<CPUCoreOperation>(op_table, (int)Table.mx, op_table.Length - (int)Table.mx).Array;
                }
            }
        }

        public enum Table { EM = 0, MX = 256, Mx = 512, mX = 768, mx = 1024 }

        public CPUCore()
        {
            initialize_opcode_table();
        }
    }
}
