using System;
using System.Text;

namespace Snes
{
    abstract partial class SMPCore
    {
        public byte op_readpc()
        {
            return op_read(regs.pc++);
        }

        public byte op_readstack()
        {
            return op_read((ushort)(0x0100 | ++regs.sp[0]));
        }

        public void op_writestack(byte data)
        {
            op_write((ushort)(0x0100 | regs.sp[0]--), data);
        }

        public byte op_readaddr(ushort addr)
        {
            return op_read(addr);
        }

        public void op_writeaddr(ushort addr, byte data)
        {
            op_write(addr, data);
        }

        public byte op_readdp(byte addr)
        {
            return op_read((ushort)((Convert.ToUInt32(regs.p.p) << 8) + addr));
        }

        public void op_writedp(byte addr, byte data)
        {
            op_write((ushort)((Convert.ToUInt32(regs.p.p) << 8) + addr), data);
        }

        public void disassemble_opcode(byte[] output, ushort addr)
        {
            string s, t;
            byte opcode_table, op0, op1;
            ushort opw, opdp0, opdp1;
            s = ASCIIEncoding.ASCII.GetString(output);

            s = string.Format("..%.4x ", addr);

            opcode_table = disassemble_read((ushort)(addr + 0));
            op0 = disassemble_read((ushort)(addr + 1));
            op1 = disassemble_read((ushort)(addr + 2));
            opw = (ushort)((op0) | (op1 << 8));
            opdp0 = (ushort)((Convert.ToUInt32(regs.p.p) << 8) + op0);
            opdp1 = (ushort)((Convert.ToUInt32(regs.p.p) << 8) + op1);

            t = "                       ";

            switch (opcode_table)
            {
                case 0x00: t = string.Format("nop"); break;
                case 0x01: t = string.Format("tcall 0"); break;
                case 0x02: t = string.Format("set0  $%.3x", opdp0); break;
                case 0x03: t = string.Format("bbs0  $%.3x,$%.4x", opdp0, relb(op1, 3)); break;
                case 0x04: t = string.Format("or    a,$%.3x", opdp0); break;
                case 0x05: t = string.Format("or    a,$%.4x", opw); break;
                case 0x06: t = string.Format("or    a,(x)"); break;
                case 0x07: t = string.Format("or    a,($%.3x+x)", opdp0); break;
                case 0x08: t = string.Format("or    a,#$%.2x", op0); break;
                case 0x09: t = string.Format("or    $%.3x,$%.3x", opdp1, opdp0); break;
                case 0x0a: t = string.Format("or1   c,$%.4x:%d", opw & 0x1fff, opw >> 13); break;
                case 0x0b: t = string.Format("asl   $%.3x", opdp0); break;
                case 0x0c: t = string.Format("asl   $%.4x", opw); break;
                case 0x0d: t = string.Format("push  p"); break;
                case 0x0e: t = string.Format("tset  $%.4x,a", opw); break;
                case 0x0f: t = string.Format("brk"); break;
                case 0x10: t = string.Format("bpl   $%.4x", relb(op0, 2)); break;
                case 0x11: t = string.Format("tcall 1"); break;
                case 0x12: t = string.Format("clr0  $%.3x", opdp0); break;
                case 0x13: t = string.Format("bbc0  $%.3x,$%.4x", opdp0, relb(op1, 3)); break;
                case 0x14: t = string.Format("or    a,$%.3x+x", opdp0); break;
                case 0x15: t = string.Format("or    a,$%.4x+x", opw); break;
                case 0x16: t = string.Format("or    a,$%.4x+y", opw); break;
                case 0x17: t = string.Format("or    a,($%.3x)+y", opdp0); break;
                case 0x18: t = string.Format("or    $%.3x,#$%.2x", opdp1, op0); break;
                case 0x19: t = string.Format("or    (x),(y)"); break;
                case 0x1a: t = string.Format("decw  $%.3x", opdp0); break;
                case 0x1b: t = string.Format("asl   $%.3x+x", opdp0); break;
                case 0x1c: t = string.Format("asl   a"); break;
                case 0x1d: t = string.Format("dec   x"); break;
                case 0x1e: t = string.Format("cmp   x,$%.4x", opw); break;
                case 0x1f: t = string.Format("jmp   ($%.4x+x)", opw); break;
                case 0x20: t = string.Format("clrp"); break;
                case 0x21: t = string.Format("tcall 2"); break;
                case 0x22: t = string.Format("set1  $%.3x", opdp0); break;
                case 0x23: t = string.Format("bbs1  $%.3x,$%.4x", opdp0, relb(op1, 3)); break;
                case 0x24: t = string.Format("and   a,$%.3x", opdp0); break;
                case 0x25: t = string.Format("and   a,$%.4x", opw); break;
                case 0x26: t = string.Format("and   a,(x)"); break;
                case 0x27: t = string.Format("and   a,($%.3x+x)", opdp0); break;
                case 0x28: t = string.Format("and   a,#$%.2x", op0); break;
                case 0x29: t = string.Format("and   $%.3x,$%.3x", opdp1, opdp0); break;
                case 0x2a: t = string.Format("or1   c,!$%.4x:%d", opw & 0x1fff, opw >> 13); break;
                case 0x2b: t = string.Format("rol   $%.3x", opdp0); break;
                case 0x2c: t = string.Format("rol   $%.4x", opw); break;
                case 0x2d: t = string.Format("push  a"); break;
                case 0x2e: t = string.Format("cbne  $%.3x,$%.4x", opdp0, relb(op1, 3)); break;
                case 0x2f: t = string.Format("bra   $%.4x", relb(op0, 2)); break;
                case 0x30: t = string.Format("bmi   $%.4x", relb(op0, 2)); break;
                case 0x31: t = string.Format("tcall 3"); break;
                case 0x32: t = string.Format("clr1  $%.3x", opdp0); break;
                case 0x33: t = string.Format("bbc1  $%.3x,$%.4x", opdp0, relb(op1, 3)); break;
                case 0x34: t = string.Format("and   a,$%.3x+x", opdp0); break;
                case 0x35: t = string.Format("and   a,$%.4x+x", opw); break;
                case 0x36: t = string.Format("and   a,$%.4x+y", opw); break;
                case 0x37: t = string.Format("and   a,($%.3x)+y", opdp0); break;
                case 0x38: t = string.Format("and   $%.3x,#$%.2x", opdp1, op0); break;
                case 0x39: t = string.Format("and   (x),(y)"); break;
                case 0x3a: t = string.Format("incw  $%.3x", opdp0); break;
                case 0x3b: t = string.Format("rol   $%.3x+x", opdp0); break;
                case 0x3c: t = string.Format("rol   a"); break;
                case 0x3d: t = string.Format("inc   x"); break;
                case 0x3e: t = string.Format("cmp   x,$%.3x", opdp0); break;
                case 0x3f: t = string.Format("call  $%.4x", opw); break;
                case 0x40: t = string.Format("setp"); break;
                case 0x41: t = string.Format("tcall 4"); break;
                case 0x42: t = string.Format("set2  $%.3x", opdp0); break;
                case 0x43: t = string.Format("bbs2  $%.3x,$%.4x", opdp0, relb(op1, 3)); break;
                case 0x44: t = string.Format("eor   a,$%.3x", opdp0); break;
                case 0x45: t = string.Format("eor   a,$%.4x", opw); break;
                case 0x46: t = string.Format("eor   a,(x)"); break;
                case 0x47: t = string.Format("eor   a,($%.3x+x)", opdp0); break;
                case 0x48: t = string.Format("eor   a,#$%.2x", op0); break;
                case 0x49: t = string.Format("eor   $%.3x,$%.3x", opdp1, opdp0); break;
                case 0x4a: t = string.Format("and1  c,$%.4x:%d", opw & 0x1fff, opw >> 13); break;
                case 0x4b: t = string.Format("lsr   $%.3x", opdp0); break;
                case 0x4c: t = string.Format("lsr   $%.4x", opw); break;
                case 0x4d: t = string.Format("push  x"); break;
                case 0x4e: t = string.Format("tclr  $%.4x,a", opw); break;
                case 0x4f: t = string.Format("pcall $ff%.2x", op0); break;
                case 0x50: t = string.Format("bvc   $%.4x", relb(op0, 2)); break;
                case 0x51: t = string.Format("tcall 5"); break;
                case 0x52: t = string.Format("clr2  $%.3x", opdp0); break;
                case 0x53: t = string.Format("bbc2  $%.3x,$%.4x", opdp0, relb(op1, 3)); break;
                case 0x54: t = string.Format("eor   a,$%.3x+x", opdp0); break;
                case 0x55: t = string.Format("eor   a,$%.4x+x", opw); break;
                case 0x56: t = string.Format("eor   a,$%.4x+y", opw); break;
                case 0x57: t = string.Format("eor   a,($%.3x)+y", opdp0); break;
                case 0x58: t = string.Format("eor   $%.3x,#$%.2x", opdp1, op0); break;
                case 0x59: t = string.Format("eor   (x),(y)"); break;
                case 0x5a: t = string.Format("cmpw  ya,$%.3x", opdp0); break;
                case 0x5b: t = string.Format("lsr   $%.3x+x", opdp0); break;
                case 0x5c: t = string.Format("lsr   a"); break;
                case 0x5d: t = string.Format("mov   x,a"); break;
                case 0x5e: t = string.Format("cmp   y,$%.4x", opw); break;
                case 0x5f: t = string.Format("jmp   $%.4x", opw); break;
                case 0x60: t = string.Format("clrc"); break;
                case 0x61: t = string.Format("tcall 6"); break;
                case 0x62: t = string.Format("set3  $%.3x", opdp0); break;
                case 0x63: t = string.Format("bbs3  $%.3x,$%.4x", opdp0, relb(op1, 3)); break;
                case 0x64: t = string.Format("cmp   a,$%.3x", opdp0); break;
                case 0x65: t = string.Format("cmp   a,$%.4x", opw); break;
                case 0x66: t = string.Format("cmp   a,(x)"); break;
                case 0x67: t = string.Format("cmp   a,($%.3x+x)", opdp0); break;
                case 0x68: t = string.Format("cmp   a,#$%.2x", op0); break;
                case 0x69: t = string.Format("cmp   $%.3x,$%.3x", opdp1, opdp0); break;
                case 0x6a: t = string.Format("and1  c,!$%.4x:%d", opw & 0x1fff, opw >> 13); break;
                case 0x6b: t = string.Format("ror   $%.3x", opdp0); break;
                case 0x6c: t = string.Format("ror   $%.4x", opw); break;
                case 0x6d: t = string.Format("push  y"); break;
                case 0x6e: t = string.Format("dbnz  $%.3x,$%.4x", opdp0, relb(op1, 3)); break;
                case 0x6f: t = string.Format("ret"); break;
                case 0x70: t = string.Format("bvs   $%.4x", relb(op0, 2)); break;
                case 0x71: t = string.Format("tcall 7"); break;
                case 0x72: t = string.Format("clr3  $%.3x", opdp0); break;
                case 0x73: t = string.Format("bbc3  $%.3x,$%.4x", opdp0, relb(op1, 3)); break;
                case 0x74: t = string.Format("cmp   a,$%.3x+x", opdp0); break;
                case 0x75: t = string.Format("cmp   a,$%.4x+x", opw); break;
                case 0x76: t = string.Format("cmp   a,$%.4x+y", opw); break;
                case 0x77: t = string.Format("cmp   a,($%.3x)+y", opdp0); break;
                case 0x78: t = string.Format("cmp   $%.3x,#$%.2x", opdp1, op0); break;
                case 0x79: t = string.Format("cmp   (x),(y)"); break;
                case 0x7a: t = string.Format("addw  ya,$%.3x", opdp0); break;
                case 0x7b: t = string.Format("ror   $%.3x+x", opdp0); break;
                case 0x7c: t = string.Format("ror   a"); break;
                case 0x7d: t = string.Format("mov   a,x"); break;
                case 0x7e: t = string.Format("cmp   y,$%.3x", opdp0); break;
                case 0x7f: t = string.Format("reti"); break;
                case 0x80: t = string.Format("setc"); break;
                case 0x81: t = string.Format("tcall 8"); break;
                case 0x82: t = string.Format("set4  $%.3x", opdp0); break;
                case 0x83: t = string.Format("bbs4  $%.3x,$%.4x", opdp0, relb(op1, 3)); break;
                case 0x84: t = string.Format("adc   a,$%.3x", opdp0); break;
                case 0x85: t = string.Format("adc   a,$%.4x", opw); break;
                case 0x86: t = string.Format("adc   a,(x)"); break;
                case 0x87: t = string.Format("adc   a,($%.3x+x)", opdp0); break;
                case 0x88: t = string.Format("adc   a,#$%.2x", op0); break;
                case 0x89: t = string.Format("adc   $%.3x,$%.3x", opdp1, opdp0); break;
                case 0x8a: t = string.Format("eor1  c,$%.4x:%d", opw & 0x1fff, opw >> 13); break;
                case 0x8b: t = string.Format("dec   $%.3x", opdp0); break;
                case 0x8c: t = string.Format("dec   $%.4x", opw); break;
                case 0x8d: t = string.Format("mov   y,#$%.2x", op0); break;
                case 0x8e: t = string.Format("pop   p"); break;
                case 0x8f: t = string.Format("mov   $%.3x,#$%.2x", opdp1, op0); break;
                case 0x90: t = string.Format("bcc   $%.4x", relb(op0, 2)); break;
                case 0x91: t = string.Format("tcall 9"); break;
                case 0x92: t = string.Format("clr4  $%.3x", opdp0); break;
                case 0x93: t = string.Format("bbc4  $%.3x,$%.4x", opdp0, relb(op1, 3)); break;
                case 0x94: t = string.Format("adc   a,$%.3x+x", opdp0); break;
                case 0x95: t = string.Format("adc   a,$%.4x+x", opw); break;
                case 0x96: t = string.Format("adc   a,$%.4x+y", opw); break;
                case 0x97: t = string.Format("adc   a,($%.3x)+y", opdp0); break;
                case 0x98: t = string.Format("adc   $%.3x,#$%.2x", opdp1, op0); break;
                case 0x99: t = string.Format("adc   (x),(y)"); break;
                case 0x9a: t = string.Format("subw  ya,$%.3x", opdp0); break;
                case 0x9b: t = string.Format("dec   $%.3x+x", opdp0); break;
                case 0x9c: t = string.Format("dec   a"); break;
                case 0x9d: t = string.Format("mov   x,sp"); break;
                case 0x9e: t = string.Format("div   ya,x"); break;
                case 0x9f: t = string.Format("xcn   a"); break;
                case 0xa0: t = string.Format("ei"); break;
                case 0xa1: t = string.Format("tcall 10"); break;
                case 0xa2: t = string.Format("set5  $%.3x", opdp0); break;
                case 0xa3: t = string.Format("bbs5  $%.3x,$%.4x", opdp0, relb(op1, 3)); break;
                case 0xa4: t = string.Format("sbc   a,$%.3x", opdp0); break;
                case 0xa5: t = string.Format("sbc   a,$%.4x", opw); break;
                case 0xa6: t = string.Format("sbc   a,(x)"); break;
                case 0xa7: t = string.Format("sbc   a,($%.3x+x)", opdp0); break;
                case 0xa8: t = string.Format("sbc   a,#$%.2x", op0); break;
                case 0xa9: t = string.Format("sbc   $%.3x,$%.3x", opdp1, opdp0); break;
                case 0xaa: t = string.Format("mov1  c,$%.4x:%d", opw & 0x1fff, opw >> 13); break;
                case 0xab: t = string.Format("inc   $%.3x", opdp0); break;
                case 0xac: t = string.Format("inc   $%.4x", opw); break;
                case 0xad: t = string.Format("cmp   y,#$%.2x", op0); break;
                case 0xae: t = string.Format("pop   a"); break;
                case 0xaf: t = string.Format("mov   (x)+,a"); break;
                case 0xb0: t = string.Format("bcs   $%.4x", relb(op0, 2)); break;
                case 0xb1: t = string.Format("tcall 11"); break;
                case 0xb2: t = string.Format("clr5  $%.3x", opdp0); break;
                case 0xb3: t = string.Format("bbc5  $%.3x,$%.4x", opdp0, relb(op1, 3)); break;
                case 0xb4: t = string.Format("sbc   a,$%.3x+x", opdp0); break;
                case 0xb5: t = string.Format("sbc   a,$%.4x+x", opw); break;
                case 0xb6: t = string.Format("sbc   a,$%.4x+y", opw); break;
                case 0xb7: t = string.Format("sbc   a,($%.3x)+y", opdp0); break;
                case 0xb8: t = string.Format("sbc   $%.3x,#$%.2x", opdp1, op0); break;
                case 0xb9: t = string.Format("sbc   (x),(y)"); break;
                case 0xba: t = string.Format("movw  ya,$%.3x", opdp0); break;
                case 0xbb: t = string.Format("inc   $%.3x+x", opdp0); break;
                case 0xbc: t = string.Format("inc   a"); break;
                case 0xbd: t = string.Format("mov   sp,x"); break;
                case 0xbe: t = string.Format("das   a"); break;
                case 0xbf: t = string.Format("mov   a,(x)+"); break;
                case 0xc0: t = string.Format("di"); break;
                case 0xc1: t = string.Format("tcall 12"); break;
                case 0xc2: t = string.Format("set6  $%.3x", opdp0); break;
                case 0xc3: t = string.Format("bbs6  $%.3x,$%.4x", opdp0, relb(op1, 3)); break;
                case 0xc4: t = string.Format("mov   $%.3x,a", opdp0); break;
                case 0xc5: t = string.Format("mov   $%.4x,a", opw); break;
                case 0xc6: t = string.Format("mov   (x),a"); break;
                case 0xc7: t = string.Format("mov   ($%.3x+x),a", opdp0); break;
                case 0xc8: t = string.Format("cmp   x,#$%.2x", op0); break;
                case 0xc9: t = string.Format("mov   $%.4x,x", opw); break;
                case 0xca: t = string.Format("mov1  $%.4x:%d,c", opw & 0x1fff, opw >> 13); break;
                case 0xcb: t = string.Format("mov   $%.3x,y", opdp0); break;
                case 0xcc: t = string.Format("mov   $%.4x,y", opw); break;
                case 0xcd: t = string.Format("mov   x,#$%.2x", op0); break;
                case 0xce: t = string.Format("pop   x"); break;
                case 0xcf: t = string.Format("mul   ya"); break;
                case 0xd0: t = string.Format("bne   $%.4x", relb(op0, 2)); break;
                case 0xd1: t = string.Format("tcall 13"); break;
                case 0xd2: t = string.Format("clr6  $%.3x", opdp0); break;
                case 0xd3: t = string.Format("bbc6  $%.3x,$%.4x", opdp0, relb(op1, 3)); break;
                case 0xd4: t = string.Format("mov   $%.3x+x,a", opdp0); break;
                case 0xd5: t = string.Format("mov   $%.4x+x,a", opw); break;
                case 0xd6: t = string.Format("mov   $%.4x+y,a", opw); break;
                case 0xd7: t = string.Format("mov   ($%.3x)+y,a", opdp0); break;
                case 0xd8: t = string.Format("mov   $%.3x,x", opdp0); break;
                case 0xd9: t = string.Format("mov   $%.3x+y,x", opdp0); break;
                case 0xda: t = string.Format("movw  $%.3x,ya", opdp0); break;
                case 0xdb: t = string.Format("mov   $%.3x+x,y", opdp0); break;
                case 0xdc: t = string.Format("dec   y"); break;
                case 0xdd: t = string.Format("mov   a,y"); break;
                case 0xde: t = string.Format("cbne  $%.3x+x,$%.4x", opdp0, relb(op1, 3)); break;
                case 0xdf: t = string.Format("daa   a"); break;
                case 0xe0: t = string.Format("clrv"); break;
                case 0xe1: t = string.Format("tcall 14"); break;
                case 0xe2: t = string.Format("set7  $%.3x", opdp0); break;
                case 0xe3: t = string.Format("bbs7  $%.3x,$%.4x", opdp0, relb(op1, 3)); break;
                case 0xe4: t = string.Format("mov   a,$%.3x", opdp0); break;
                case 0xe5: t = string.Format("mov   a,$%.4x", opw); break;
                case 0xe6: t = string.Format("mov   a,(x)"); break;
                case 0xe7: t = string.Format("mov   a,($%.3x+x)", opdp0); break;
                case 0xe8: t = string.Format("mov   a,#$%.2x", op0); break;
                case 0xe9: t = string.Format("mov   x,$%.4x", opw); break;
                case 0xea: t = string.Format("not1  c,$%.4x:%d", opw & 0x1fff, opw >> 13); break;
                case 0xeb: t = string.Format("mov   y,$%.3x", opdp0); break;
                case 0xec: t = string.Format("mov   y,$%.4x", opw); break;
                case 0xed: t = string.Format("notc"); break;
                case 0xee: t = string.Format("pop   y"); break;
                case 0xef: t = string.Format("sleep"); break;
                case 0xf0: t = string.Format("beq   $%.4x", relb(op0, 2)); break;
                case 0xf1: t = string.Format("tcall 15"); break;
                case 0xf2: t = string.Format("clr7  $%.3x", opdp0); break;
                case 0xf3: t = string.Format("bbc7  $%.3x,$%.4x", opdp0, relb(op1, 3)); break;
                case 0xf4: t = string.Format("mov   a,$%.3x+x", opdp0); break;
                case 0xf5: t = string.Format("mov   a,$%.4x+x", opw); break;
                case 0xf6: t = string.Format("mov   a,$%.4x+y", opw); break;
                case 0xf7: t = string.Format("mov   a,($%.3x)+y", opdp0); break;
                case 0xf8: t = string.Format("mov   x,$%.3x", opdp0); break;
                case 0xf9: t = string.Format("mov   x,$%.3x+y", opdp0); break;
                case 0xfa: t = string.Format("mov   $%.3x,$%.3x", opdp1, opdp0); break;
                case 0xfb: t = string.Format("mov   y,$%.3x+x", opdp0); break;
                case 0xfc: t = string.Format("inc   y"); break;
                case 0xfd: t = string.Format("mov   y,a"); break;
                case 0xfe: t = string.Format("dbnz  y,$%.4x", relb(op0, 2)); break;
                case 0xff: t = string.Format("stop"); break;
            }

            t += ' ';
            s += t;

            t = string.Format("A:%.2x X:%.2x Y:%.2x SP:01%.2x YA:%.4x ",
              regs.a, regs.x, regs.y, regs.sp, (ushort)regs.ya);
            s += t;

            t = string.Format("%c%c%c%c%c%c%c%c",
              regs.p.n ? 'N' : 'n',
              regs.p.v ? 'V' : 'v',
              regs.p.p ? 'P' : 'p',
              regs.p.b ? 'B' : 'b',
              regs.p.h ? 'H' : 'h',
              regs.p.i ? 'I' : 'i',
              regs.p.z ? 'Z' : 'z',
              regs.p.c ? 'C' : 'c');
            s += t;
        }

        public byte disassemble_read(ushort addr)
        {
            if (addr >= 0xffc0)
            {
                return SMP.Iplrom[addr & 0x3f];
            }
            return StaticRAM.apuram[addr];
        }

        public ushort relb(byte offset, int op_len)
        {
            ushort pc = (ushort)(regs.pc + op_len);
            return (ushort)(pc + offset);
        }

        public Regs regs = new Regs();
        public ushort dp, sp, rd, wr, bit, ya;

        public abstract void op_io();
        public abstract byte op_read(ushort addr);
        public abstract void op_write(ushort addr, byte data);

        public byte op_adc(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public ushort op_addw(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public byte op_and(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public byte op_cmp(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public ushort op_cmpw(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public byte op_eor(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public byte op_inc(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public byte op_dec(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public byte op_or(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public byte op_sbc(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public ushort op_subw(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public byte op_asl(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public byte op_lsr(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public byte op_rol(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public byte op_ror(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_mov_reg_reg(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_mov_sp_x(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_mov_reg_const(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_mov_a_ix(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_mov_a_ixinc(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_mov_reg_dp(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_mov_reg_dpr(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_mov_reg_addr(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_mov_a_addrr(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_mov_a_idpx(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_mov_a_idpy(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_mov_dp_dp(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_mov_dp_const(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_mov_ix_a(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_mov_ixinc_a(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_mov_dp_reg(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_mov_dpr_reg(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_mov_addr_reg(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_mov_addrr_a(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_mov_idpx_a(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_mov_idpy_a(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_movw_ya_dp(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_movw_dp_ya(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_mov1_c_bit(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_mov1_bit_c(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_bra(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_branch(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_bitbranch(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_cbne_dp(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_cbne_dpx(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_dbnz_dp(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_dbnz_y(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_jmp_addr(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_jmp_iaddrx(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_call(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_pcall(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_tcall(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_brk(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_ret(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_reti(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_read_reg_const(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_read_a_ix(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_read_reg_dp(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_read_a_dpx(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_read_reg_addr(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_read_a_addrr(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_read_a_idpx(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_read_a_idpy(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_read_ix_iy(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_read_dp_dp(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_read_dp_const(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_read_ya_dp(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_cmpw_ya_dp(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_and1_bit(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_eor1_bit(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_not1_bit(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_or1_bit(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_adjust_reg(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_adjust_dp(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_adjust_dpx(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_adjust_addr(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_adjust_addr_a(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_adjustw_dp(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_nop(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_wait(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_xcn(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_daa(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_das(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_setbit(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_notc(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_seti(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_setbit_dp(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_push_reg(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_push_p(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_pop_reg(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_pop_p(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_mul_ya(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public void op_div_ya_x(SMPCoreOpArguments args) { throw new NotImplementedException(); }

        public SMPCoreOp[] opcode_table = new SMPCoreOp[256];

        public void initialize_opcode_table()
        {
            //opcode_table[0x00] = op_nop;
            //opcode_table[0x01] = op_tcall;
            //opcode_table[0x02] = op_setbit_dp;
            //opcode_table[0x03] = op_bitbranch;
            //opcode_table[0x04] = op_read_reg_dp;
            //opcode_table[0x05] = op_read_reg_addr;
            //opcode_table[0x06] = op_read_a_ix;
            //opcode_table[0x07] = op_read_a_idpx;
            //opcode_table[0x08] = op_read_reg_const;
            //opcode_table[0x09] = op_read_dp_dp;
            //opcode_table[0x0a] = op_or1_bit;
            //opcode_table[0x0b] = op_adjust_dp;
            //opcode_table[0x0c] = op_adjust_addr;
            //opcode_table[0x0d] = op_push_p;
            //opcode_table[0x0e] = op_adjust_addr_a;
            //opcode_table[0x0f] = op_brk;
            //opcode_table[0x10] = op_branch;
            //opcode_table[0x11] = op_tcall;
            //opcode_table[0x12] = op_setbit_dp;
            //opcode_table[0x13] = op_bitbranch;
            //opcode_table[0x14] = op_read_a_dpx;
            //opcode_table[0x15] = op_read_a_addrr;
            //opcode_table[0x16] = op_read_a_addrr;
            //opcode_table[0x17] = op_read_a_idpy;
            //opcode_table[0x18] = op_read_dp_const;
            //opcode_table[0x19] = op_read_ix_iy;
            //opcode_table[0x1a] = op_adjustw_dp;
            //opcode_table[0x1b] = op_adjust_dpx;
            //opcode_table[0x1c] = op_adjust_reg;
            //opcode_table[0x1d] = op_adjust_reg;
            //opcode_table[0x1e] = op_read_reg_addr;
            //opcode_table[0x1f] = op_jmp_iaddrx;
            //opcode_table[0x20] = op_setbit;
            //opcode_table[0x21] = op_tcall;
            //opcode_table[0x22] = op_setbit_dp;
            //opcode_table[0x23] = op_bitbranch;
            //opcode_table[0x24] = op_read_reg_dp;
            //opcode_table[0x25] = op_read_reg_addr;
            //opcode_table[0x26] = op_read_a_ix;
            //opcode_table[0x27] = op_read_a_idpx;
            //opcode_table[0x28] = op_read_reg_const;
            //opcode_table[0x29] = op_read_dp_dp;
            //opcode_table[0x2a] = op_or1_bit;
            //opcode_table[0x2b] = op_adjust_dp;
            //opcode_table[0x2c] = op_adjust_addr;
            //opcode_table[0x2d] = op_push_reg;
            //opcode_table[0x2e] = op_cbne_dp;
            //opcode_table[0x2f] = op_bra;
            //opcode_table[0x30] = op_branch;
            //opcode_table[0x31] = op_tcall;
            //opcode_table[0x32] = op_setbit_dp;
            //opcode_table[0x33] = op_bitbranch;
            //opcode_table[0x34] = op_read_a_dpx;
            //opcode_table[0x35] = op_read_a_addrr;
            //opcode_table[0x36] = op_read_a_addrr;
            //opcode_table[0x37] = op_read_a_idpy;
            //opcode_table[0x38] = op_read_dp_const;
            //opcode_table[0x39] = op_read_ix_iy;
            //opcode_table[0x3a] = op_adjustw_dp;
            //opcode_table[0x3b] = op_adjust_dpx;
            //opcode_table[0x3c] = op_adjust_reg;
            //opcode_table[0x3d] = op_adjust_reg;
            //opcode_table[0x3e] = op_read_reg_dp;
            //opcode_table[0x3f] = op_call;
            //opcode_table[0x40] = op_setbit;
            //opcode_table[0x41] = op_tcall;
            //opcode_table[0x42] = op_setbit_dp;
            //opcode_table[0x43] = op_bitbranch;
            //opcode_table[0x44] = op_read_reg_dp;
            //opcode_table[0x45] = op_read_reg_addr;
            //opcode_table[0x46] = op_read_a_ix;
            //opcode_table[0x47] = op_read_a_idpx;
            //opcode_table[0x48] = op_read_reg_const;
            //opcode_table[0x49] = op_read_dp_dp;
            //opcode_table[0x4a] = op_and1_bit;
            //opcode_table[0x4b] = op_adjust_dp;
            //opcode_table[0x4c] = op_adjust_addr;
            //opcode_table[0x4d] = op_push_reg;
            //opcode_table[0x4e] = op_adjust_addr_a;
            //opcode_table[0x4f] = op_pcall;
            //opcode_table[0x50] = op_branch;
            //opcode_table[0x51] = op_tcall;
            //opcode_table[0x52] = op_setbit_dp;
            //opcode_table[0x53] = op_bitbranch;
            //opcode_table[0x54] = op_read_a_dpx;
            //opcode_table[0x55] = op_read_a_addrr;
            //opcode_table[0x56] = op_read_a_addrr;
            //opcode_table[0x57] = op_read_a_idpy;
            //opcode_table[0x58] = op_read_dp_const;
            //opcode_table[0x59] = op_read_ix_iy;
            //opcode_table[0x5a] = op_cmpw_ya_dp;
            //opcode_table[0x5b] = op_adjust_dpx;
            //opcode_table[0x5c] = op_adjust_reg;
            //opcode_table[0x5d] = op_mov_reg_reg;
            //opcode_table[0x5e] = op_read_reg_addr;
            //opcode_table[0x5f] = op_jmp_addr;
            //opcode_table[0x60] = op_setbit;
            //opcode_table[0x61] = op_tcall;
            //opcode_table[0x62] = op_setbit_dp;
            //opcode_table[0x63] = op_bitbranch;
            //opcode_table[0x64] = op_read_reg_dp;
            //opcode_table[0x65] = op_read_reg_addr;
            //opcode_table[0x66] = op_read_a_ix;
            //opcode_table[0x67] = op_read_a_idpx;
            //opcode_table[0x68] = op_read_reg_const;
            //opcode_table[0x69] = op_read_dp_dp;
            //opcode_table[0x6a] = op_and1_bit;
            //opcode_table[0x6b] = op_adjust_dp;
            //opcode_table[0x6c] = op_adjust_addr;
            //opcode_table[0x6d] = op_push_reg;
            //opcode_table[0x6e] = op_dbnz_dp;
            //opcode_table[0x6f] = op_ret;
            //opcode_table[0x70] = op_branch;
            //opcode_table[0x71] = op_tcall;
            //opcode_table[0x72] = op_setbit_dp;
            //opcode_table[0x73] = op_bitbranch;
            //opcode_table[0x74] = op_read_a_dpx;
            //opcode_table[0x75] = op_read_a_addrr;
            //opcode_table[0x76] = op_read_a_addrr;
            //opcode_table[0x77] = op_read_a_idpy;
            //opcode_table[0x78] = op_read_dp_const;
            //opcode_table[0x79] = op_read_ix_iy;
            //opcode_table[0x7a] = op_read_ya_dp;
            //opcode_table[0x7b] = op_adjust_dpx;
            //opcode_table[0x7c] = op_adjust_reg;
            //opcode_table[0x7d] = op_mov_reg_reg;
            //opcode_table[0x7e] = op_read_reg_dp;
            //opcode_table[0x7f] = op_reti;
            //opcode_table[0x80] = op_setbit;
            //opcode_table[0x81] = op_tcall;
            //opcode_table[0x82] = op_setbit_dp;
            //opcode_table[0x83] = op_bitbranch;
            //opcode_table[0x84] = op_read_reg_dp;
            //opcode_table[0x85] = op_read_reg_addr;
            //opcode_table[0x86] = op_read_a_ix;
            //opcode_table[0x87] = op_read_a_idpx;
            //opcode_table[0x88] = op_read_reg_const;
            //opcode_table[0x89] = op_read_dp_dp;
            //opcode_table[0x8a] = op_eor1_bit;
            //opcode_table[0x8b] = op_adjust_dp;
            //opcode_table[0x8c] = op_adjust_addr;
            //opcode_table[0x8d] = op_mov_reg_const;
            //opcode_table[0x8e] = op_pop_p;
            //opcode_table[0x8f] = op_mov_dp_const;
            //opcode_table[0x90] = op_branch;
            //opcode_table[0x91] = op_tcall;
            //opcode_table[0x92] = op_setbit_dp;
            //opcode_table[0x93] = op_bitbranch;
            //opcode_table[0x94] = op_read_a_dpx;
            //opcode_table[0x95] = op_read_a_addrr;
            //opcode_table[0x96] = op_read_a_addrr;
            //opcode_table[0x97] = op_read_a_idpy;
            //opcode_table[0x98] = op_read_dp_const;
            //opcode_table[0x99] = op_read_ix_iy;
            //opcode_table[0x9a] = op_read_ya_dp;
            //opcode_table[0x9b] = op_adjust_dpx;
            //opcode_table[0x9c] = op_adjust_reg;
            //opcode_table[0x9d] = op_mov_reg_reg;
            //opcode_table[0x9e] = op_div_ya_x;
            //opcode_table[0x9f] = op_xcn;
            //opcode_table[0xa0] = op_seti;
            //opcode_table[0xa1] = op_tcall;
            //opcode_table[0xa2] = op_setbit_dp;
            //opcode_table[0xa3] = op_bitbranch;
            //opcode_table[0xa4] = op_read_reg_dp;
            //opcode_table[0xa5] = op_read_reg_addr;
            //opcode_table[0xa6] = op_read_a_ix;
            //opcode_table[0xa7] = op_read_a_idpx;
            //opcode_table[0xa8] = op_read_reg_const;
            //opcode_table[0xa9] = op_read_dp_dp;
            //opcode_table[0xaa] = op_mov1_c_bit;
            //opcode_table[0xab] = op_adjust_dp;
            //opcode_table[0xac] = op_adjust_addr;
            //opcode_table[0xad] = op_read_reg_const;
            //opcode_table[0xae] = op_pop_reg;
            //opcode_table[0xaf] = op_mov_ixinc_a;
            //opcode_table[0xb0] = op_branch;
            //opcode_table[0xb1] = op_tcall;
            //opcode_table[0xb2] = op_setbit_dp;
            //opcode_table[0xb3] = op_bitbranch;
            //opcode_table[0xb4] = op_read_a_dpx;
            //opcode_table[0xb5] = op_read_a_addrr;
            //opcode_table[0xb6] = op_read_a_addrr;
            //opcode_table[0xb7] = op_read_a_idpy;
            //opcode_table[0xb8] = op_read_dp_const;
            //opcode_table[0xb9] = op_read_ix_iy;
            //opcode_table[0xba] = op_movw_ya_dp;
            //opcode_table[0xbb] = op_adjust_dpx;
            //opcode_table[0xbc] = op_adjust_reg;
            //opcode_table[0xbd] = op_mov_sp_x;
            //opcode_table[0xbe] = op_das;
            //opcode_table[0xbf] = op_mov_a_ixinc;
            //opcode_table[0xc0] = op_seti;
            //opcode_table[0xc1] = op_tcall;
            //opcode_table[0xc2] = op_setbit_dp;
            //opcode_table[0xc3] = op_bitbranch;
            //opcode_table[0xc4] = op_mov_dp_reg;
            //opcode_table[0xc5] = op_mov_addr_reg;
            //opcode_table[0xc6] = op_mov_ix_a;
            //opcode_table[0xc7] = op_mov_idpx_a;
            //opcode_table[0xc8] = op_read_reg_const;
            //opcode_table[0xc9] = op_mov_addr_reg;
            //opcode_table[0xca] = op_mov1_bit_c;
            //opcode_table[0xcb] = op_mov_dp_reg;
            //opcode_table[0xcc] = op_mov_addr_reg;
            //opcode_table[0xcd] = op_mov_reg_const;
            //opcode_table[0xce] = op_pop_reg;
            //opcode_table[0xcf] = op_mul_ya;
            //opcode_table[0xd0] = op_branch;
            //opcode_table[0xd1] = op_tcall;
            //opcode_table[0xd2] = op_setbit_dp;
            //opcode_table[0xd3] = op_bitbranch;
            //opcode_table[0xd4] = op_mov_dpr_reg;
            //opcode_table[0xd5] = op_mov_addrr_a;
            //opcode_table[0xd6] = op_mov_addrr_a;
            //opcode_table[0xd7] = op_mov_idpy_a;
            //opcode_table[0xd8] = op_mov_dp_reg;
            //opcode_table[0xd9] = op_mov_dpr_reg;
            //opcode_table[0xda] = op_movw_dp_ya;
            //opcode_table[0xdb] = op_mov_dpr_reg;
            //opcode_table[0xdc] = op_adjust_reg;
            //opcode_table[0xdd] = op_mov_reg_reg;
            //opcode_table[0xde] = op_cbne_dpx;
            //opcode_table[0xdf] = op_daa;
            //opcode_table[0xe0] = op_setbit;
            //opcode_table[0xe1] = op_tcall;
            //opcode_table[0xe2] = op_setbit_dp;
            //opcode_table[0xe3] = op_bitbranch;
            //opcode_table[0xe4] = op_mov_reg_dp;
            //opcode_table[0xe5] = op_mov_reg_addr;
            //opcode_table[0xe6] = op_mov_a_ix;
            //opcode_table[0xe7] = op_mov_a_idpx;
            //opcode_table[0xe8] = op_mov_reg_const;
            //opcode_table[0xe9] = op_mov_reg_addr;
            //opcode_table[0xea] = op_not1_bit;
            //opcode_table[0xeb] = op_mov_reg_dp;
            //opcode_table[0xec] = op_mov_reg_addr;
            //opcode_table[0xed] = op_notc;
            //opcode_table[0xee] = op_pop_reg;
            //opcode_table[0xef] = op_wait;
            //opcode_table[0xf0] = op_branch;
            //opcode_table[0xf1] = op_tcall;
            //opcode_table[0xf2] = op_setbit_dp;
            //opcode_table[0xf3] = op_bitbranch;
            //opcode_table[0xf4] = op_mov_reg_dpr;
            //opcode_table[0xf5] = op_mov_a_addrr;
            //opcode_table[0xf6] = op_mov_a_addrr;
            //opcode_table[0xf7] = op_mov_a_idpy;
            //opcode_table[0xf8] = op_mov_reg_dp;
            //opcode_table[0xf9] = op_mov_reg_dpr;
            //opcode_table[0xfa] = op_mov_dp_dp;
            //opcode_table[0xfb] = op_mov_reg_dpr;
            //opcode_table[0xfc] = op_adjust_reg;
            //opcode_table[0xfd] = op_mov_reg_reg;
            //opcode_table[0xfe] = op_dbnz_y;
            //opcode_table[0xff] = op_wait;
        }

        public SMPCore()
        {
            initialize_opcode_table();
        }
    }
}
