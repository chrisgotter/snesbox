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
            s = new UTF8Encoding().GetString(output);

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
              regs.a[0], regs.x[0], regs.y[0], regs.sp[0], (ushort)regs.ya);
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

            output = new UTF8Encoding().GetBytes(s);
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
        public enum OpCode { A = 0, X = 1, Y = 2, SP = 3 };

        public abstract void op_io();
        public abstract byte op_read(ushort addr);
        public abstract void op_write(ushort addr, byte data);

        public SMPCoreOpResult op_adc(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_addw(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_and(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_cmp(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_cmpw(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_eor(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_inc(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_dec(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_or(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_sbc(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_subw(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_asl(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_lsr(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_rol(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_ror(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_mov_reg_reg(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_mov_sp_x(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_mov_reg_const(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_mov_a_ix(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_mov_a_ixinc(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_mov_reg_dp(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_mov_reg_dpr(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_mov_reg_addr(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_mov_a_addrr(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_mov_a_idpx(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_mov_a_idpy(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_mov_dp_dp(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_mov_dp_const(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_mov_ix_a(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_mov_ixinc_a(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_mov_dp_reg(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_mov_dpr_reg(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_mov_addr_reg(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_mov_addrr_a(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_mov_idpx_a(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_mov_idpy_a(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_movw_ya_dp(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_movw_dp_ya(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_mov1_c_bit(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_mov1_bit_c(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_bra(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_branch(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_bitbranch(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_cbne_dp(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_cbne_dpx(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_dbnz_dp(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_dbnz_y(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_jmp_addr(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_jmp_iaddrx(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_call(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_pcall(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_tcall(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_brk(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_ret(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_reti(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_read_reg_const(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_read_a_ix(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_read_reg_dp(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_read_a_dpx(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_read_reg_addr(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_read_a_addrr(SMPCoreOpArgument args)
        {
            dp = (ushort)(op_readpc() << 0);
            dp |= (ushort)(op_readpc() << 8);
            op_io();
            rd = op_readaddr((ushort)(dp + regs.r[args.i]));
            regs.a[0] = args.op_func(new SMPCoreOpArgument() { x_byte = regs.a[0], y_byte = (byte)rd }).return_byte;
            return null;
        }

        public SMPCoreOpResult op_read_a_idpx(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_read_a_idpy(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_read_ix_iy(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_read_dp_dp(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_read_dp_const(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_read_ya_dp(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_cmpw_ya_dp(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_and1_bit(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_eor1_bit(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_not1_bit(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_or1_bit(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_adjust_reg(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_adjust_dp(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_adjust_dpx(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_adjust_addr(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_adjust_addr_a(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_adjustw_dp(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_nop(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_wait(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_xcn(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_daa(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_das(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_setbit(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_notc(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_seti(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_setbit_dp(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_push_reg(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_push_p(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_pop_reg(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_pop_p(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_mul_ya(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOpResult op_div_ya_x(SMPCoreOpArgument args) { throw new NotImplementedException(); }

        public SMPCoreOperation[] opcode_table = new SMPCoreOperation[256];

        public void initialize_opcode_table()
        {
            opcode_table[0x00] = new SMPCoreOperation(op_nop, null);
            opcode_table[0x01] = new SMPCoreOperation(op_tcall, new SMPCoreOpArgument() { n = 0 });
            opcode_table[0x02] = new SMPCoreOperation(op_setbit_dp, new SMPCoreOpArgument() { op = 1, value = 0x01 });
            opcode_table[0x03] = new SMPCoreOperation(op_bitbranch, new SMPCoreOpArgument() { mask = 0x01, value = Convert.ToInt32(true) });
            opcode_table[0x04] = new SMPCoreOperation(op_read_reg_dp, new SMPCoreOpArgument() { op_func = op_or, n = (int)OpCode.A });
            opcode_table[0x05] = new SMPCoreOperation(op_read_reg_addr, new SMPCoreOpArgument() { op_func = op_or, n = (int)OpCode.A });
            opcode_table[0x06] = new SMPCoreOperation(op_read_a_ix, new SMPCoreOpArgument() { op_func = op_or });
            opcode_table[0x07] = new SMPCoreOperation(op_read_a_idpx, new SMPCoreOpArgument() { op_func = op_or });
            opcode_table[0x08] = new SMPCoreOperation(op_read_reg_const, new SMPCoreOpArgument() { op_func = op_or, n = (int)OpCode.A });
            opcode_table[0x09] = new SMPCoreOperation(op_read_dp_dp, new SMPCoreOpArgument() { op_func = op_or });
            opcode_table[0x0a] = new SMPCoreOperation(op_or1_bit, new SMPCoreOpArgument() { op = 0 });
            opcode_table[0x0b] = new SMPCoreOperation(op_adjust_dp, new SMPCoreOpArgument() { op_func = op_asl });
            opcode_table[0x0c] = new SMPCoreOperation(op_adjust_addr, new SMPCoreOpArgument() { op_func = op_asl });
            opcode_table[0x0d] = new SMPCoreOperation(op_push_p, null);
            opcode_table[0x0e] = new SMPCoreOperation(op_adjust_addr_a, new SMPCoreOpArgument() { op = 1 });
            opcode_table[0x0f] = new SMPCoreOperation(op_brk, null);
            opcode_table[0x10] = new SMPCoreOperation(op_branch, new SMPCoreOpArgument() { flag = 0x80, value = Convert.ToInt32(false) });
            opcode_table[0x11] = new SMPCoreOperation(op_tcall, new SMPCoreOpArgument() { n = 1 });
            opcode_table[0x12] = new SMPCoreOperation(op_setbit_dp, new SMPCoreOpArgument() { op = 0, value = 0x01 });
            opcode_table[0x13] = new SMPCoreOperation(op_bitbranch, new SMPCoreOpArgument() { mask = 0x01, value = Convert.ToInt32(false) });
            opcode_table[0x14] = new SMPCoreOperation(op_read_a_dpx, new SMPCoreOpArgument() { op_func = op_or });
            opcode_table[0x15] = new SMPCoreOperation(op_read_a_addrr, new SMPCoreOpArgument() { op_func = op_or, i = (int)OpCode.X });
            opcode_table[0x16] = new SMPCoreOperation(op_read_a_addrr, new SMPCoreOpArgument() { op_func = op_or, i = (int)OpCode.Y });
            opcode_table[0x17] = new SMPCoreOperation(op_read_a_idpy, new SMPCoreOpArgument() { op_func = op_or });
            opcode_table[0x18] = new SMPCoreOperation(op_read_dp_const, new SMPCoreOpArgument() { op_func = op_or });
            opcode_table[0x19] = new SMPCoreOperation(op_read_ix_iy, new SMPCoreOpArgument() { op_func = op_or });
            opcode_table[0x1a] = new SMPCoreOperation(op_adjustw_dp, new SMPCoreOpArgument() { adjust = -1 });
            opcode_table[0x1b] = new SMPCoreOperation(op_adjust_dpx, new SMPCoreOpArgument() { op_func = op_asl });
            opcode_table[0x1c] = new SMPCoreOperation(op_adjust_reg, new SMPCoreOpArgument() { op_func = op_asl, n = (int)OpCode.A });
            opcode_table[0x1d] = new SMPCoreOperation(op_adjust_reg, new SMPCoreOpArgument() { op_func = op_dec, n = (int)OpCode.X });
            opcode_table[0x1e] = new SMPCoreOperation(op_read_reg_addr, new SMPCoreOpArgument() { op_func = op_cmp, n = (int)OpCode.X });
            opcode_table[0x1f] = new SMPCoreOperation(op_jmp_iaddrx, null);
            opcode_table[0x20] = new SMPCoreOperation(op_setbit, new SMPCoreOpArgument() { mask = 0x20, value = 0x00 });
            opcode_table[0x21] = new SMPCoreOperation(op_tcall, new SMPCoreOpArgument() { n = 2 });
            opcode_table[0x22] = new SMPCoreOperation(op_setbit_dp, new SMPCoreOpArgument() { op = 1, value = 0x02 });
            opcode_table[0x23] = new SMPCoreOperation(op_bitbranch, new SMPCoreOpArgument() { mask = 0x02, value = Convert.ToInt32(true) });
            opcode_table[0x24] = new SMPCoreOperation(op_read_reg_dp, new SMPCoreOpArgument() { op_func = op_and, n = (int)OpCode.A });
            opcode_table[0x25] = new SMPCoreOperation(op_read_reg_addr, new SMPCoreOpArgument() { op_func = op_and, n = (int)OpCode.A });
            opcode_table[0x26] = new SMPCoreOperation(op_read_a_ix, new SMPCoreOpArgument() { op_func = op_and });
            opcode_table[0x27] = new SMPCoreOperation(op_read_a_idpx, new SMPCoreOpArgument() { op_func = op_and });
            opcode_table[0x28] = new SMPCoreOperation(op_read_reg_const, new SMPCoreOpArgument() { op_func = op_and, n = (int)OpCode.A });
            opcode_table[0x29] = new SMPCoreOperation(op_read_dp_dp, new SMPCoreOpArgument() { op_func = op_and });
            opcode_table[0x2a] = new SMPCoreOperation(op_or1_bit, new SMPCoreOpArgument() { op = 1 });
            opcode_table[0x2b] = new SMPCoreOperation(op_adjust_dp, new SMPCoreOpArgument() { op_func = op_rol });
            opcode_table[0x2c] = new SMPCoreOperation(op_adjust_addr, new SMPCoreOpArgument() { op_func = op_rol });
            opcode_table[0x2d] = new SMPCoreOperation(op_push_reg, new SMPCoreOpArgument() { n = (int)OpCode.A });
            opcode_table[0x2e] = new SMPCoreOperation(op_cbne_dp, null);
            opcode_table[0x2f] = new SMPCoreOperation(op_bra, null);
            opcode_table[0x30] = new SMPCoreOperation(op_branch, new SMPCoreOpArgument() { flag = 0x80, value = Convert.ToInt32(true) });
            opcode_table[0x31] = new SMPCoreOperation(op_tcall, new SMPCoreOpArgument() { n = 3 });
            opcode_table[0x32] = new SMPCoreOperation(op_setbit_dp, new SMPCoreOpArgument() { op = 0, value = 0x02 });
            opcode_table[0x33] = new SMPCoreOperation(op_bitbranch, new SMPCoreOpArgument() { mask = 0x02, value = Convert.ToInt32(false) });
            opcode_table[0x34] = new SMPCoreOperation(op_read_a_dpx, new SMPCoreOpArgument() { op_func = op_and });
            opcode_table[0x35] = new SMPCoreOperation(op_read_a_addrr, new SMPCoreOpArgument() { op_func = op_and, i = (int)OpCode.X });
            opcode_table[0x36] = new SMPCoreOperation(op_read_a_addrr, new SMPCoreOpArgument() { op_func = op_and, i = (int)OpCode.Y });
            opcode_table[0x37] = new SMPCoreOperation(op_read_a_idpy, new SMPCoreOpArgument() { op_func = op_and });
            opcode_table[0x38] = new SMPCoreOperation(op_read_dp_const, new SMPCoreOpArgument() { op_func = op_and });
            opcode_table[0x39] = new SMPCoreOperation(op_read_ix_iy, new SMPCoreOpArgument() { op_func = op_and });
            opcode_table[0x3a] = new SMPCoreOperation(op_adjustw_dp, new SMPCoreOpArgument() { adjust = +1 });
            opcode_table[0x3b] = new SMPCoreOperation(op_adjust_dpx, new SMPCoreOpArgument() { op_func = op_rol });
            opcode_table[0x3c] = new SMPCoreOperation(op_adjust_reg, new SMPCoreOpArgument() { op_func = op_rol, n = (int)OpCode.A });
            opcode_table[0x3d] = new SMPCoreOperation(op_adjust_reg, new SMPCoreOpArgument() { op_func = op_inc, n = (int)OpCode.X });
            opcode_table[0x3e] = new SMPCoreOperation(op_read_reg_dp, new SMPCoreOpArgument() { op_func = op_cmp, n = (int)OpCode.X });
            opcode_table[0x3f] = new SMPCoreOperation(op_call, null);
            opcode_table[0x40] = new SMPCoreOperation(op_setbit, new SMPCoreOpArgument() { mask = 0x20, value = 0x20 });
            opcode_table[0x41] = new SMPCoreOperation(op_tcall, new SMPCoreOpArgument() { n = 4 });
            opcode_table[0x42] = new SMPCoreOperation(op_setbit_dp, new SMPCoreOpArgument() { op = 1, value = 0x04 });
            opcode_table[0x43] = new SMPCoreOperation(op_bitbranch, new SMPCoreOpArgument() { mask = 0x04, value = Convert.ToInt32(true) });
            opcode_table[0x44] = new SMPCoreOperation(op_read_reg_dp, new SMPCoreOpArgument() { op_func = op_eor, n = (int)OpCode.A });
            opcode_table[0x45] = new SMPCoreOperation(op_read_reg_addr, new SMPCoreOpArgument() { op_func = op_eor, n = (int)OpCode.A });
            opcode_table[0x46] = new SMPCoreOperation(op_read_a_ix, new SMPCoreOpArgument() { op_func = op_eor });
            opcode_table[0x47] = new SMPCoreOperation(op_read_a_idpx, new SMPCoreOpArgument() { op_func = op_eor });
            opcode_table[0x48] = new SMPCoreOperation(op_read_reg_const, new SMPCoreOpArgument() { op_func = op_eor, n = (int)OpCode.A });
            opcode_table[0x49] = new SMPCoreOperation(op_read_dp_dp, new SMPCoreOpArgument() { op_func = op_eor });
            opcode_table[0x4a] = new SMPCoreOperation(op_and1_bit, new SMPCoreOpArgument() { op = 0 });
            opcode_table[0x4b] = new SMPCoreOperation(op_adjust_dp, new SMPCoreOpArgument() { op_func = op_lsr });
            opcode_table[0x4c] = new SMPCoreOperation(op_adjust_addr, new SMPCoreOpArgument() { op_func = op_lsr });
            opcode_table[0x4d] = new SMPCoreOperation(op_push_reg, new SMPCoreOpArgument() { n = (int)OpCode.X });
            opcode_table[0x4e] = new SMPCoreOperation(op_adjust_addr_a, new SMPCoreOpArgument() { op = 0 });
            opcode_table[0x4f] = new SMPCoreOperation(op_pcall, null);
            opcode_table[0x50] = new SMPCoreOperation(op_branch, new SMPCoreOpArgument() { flag = 0x40, value = Convert.ToInt32(false) });
            opcode_table[0x51] = new SMPCoreOperation(op_tcall, new SMPCoreOpArgument() { n = 5 });
            opcode_table[0x52] = new SMPCoreOperation(op_setbit_dp, new SMPCoreOpArgument() { op = 0, value = 0x04 });
            opcode_table[0x53] = new SMPCoreOperation(op_bitbranch, new SMPCoreOpArgument() { mask = 0x04, value = Convert.ToInt32(false) });
            opcode_table[0x54] = new SMPCoreOperation(op_read_a_dpx, new SMPCoreOpArgument() { op_func = op_eor });
            opcode_table[0x55] = new SMPCoreOperation(op_read_a_addrr, new SMPCoreOpArgument() { op_func = op_eor, i = (int)OpCode.X });
            opcode_table[0x56] = new SMPCoreOperation(op_read_a_addrr, new SMPCoreOpArgument() { op_func = op_eor, i = (int)OpCode.Y });
            opcode_table[0x57] = new SMPCoreOperation(op_read_a_idpy, new SMPCoreOpArgument() { op_func = op_eor });
            opcode_table[0x58] = new SMPCoreOperation(op_read_dp_const, new SMPCoreOpArgument() { op_func = op_eor });
            opcode_table[0x59] = new SMPCoreOperation(op_read_ix_iy, new SMPCoreOpArgument() { op_func = op_eor });
            opcode_table[0x5a] = new SMPCoreOperation(op_cmpw_ya_dp, null);
            opcode_table[0x5b] = new SMPCoreOperation(op_adjust_dpx, new SMPCoreOpArgument() { op_func = op_lsr });
            opcode_table[0x5c] = new SMPCoreOperation(op_adjust_reg, new SMPCoreOpArgument() { op_func = op_lsr, n = (int)OpCode.A });
            opcode_table[0x5d] = new SMPCoreOperation(op_mov_reg_reg, new SMPCoreOpArgument() { to = (int)OpCode.X, from = (int)OpCode.A });
            opcode_table[0x5e] = new SMPCoreOperation(op_read_reg_addr, new SMPCoreOpArgument() { op_func = op_cmp, n = (int)OpCode.Y });
            opcode_table[0x5f] = new SMPCoreOperation(op_jmp_addr, null);
            opcode_table[0x60] = new SMPCoreOperation(op_setbit, new SMPCoreOpArgument() { mask = 0x01, value = 0x00 });
            opcode_table[0x61] = new SMPCoreOperation(op_tcall, new SMPCoreOpArgument() { n = 6 });
            opcode_table[0x62] = new SMPCoreOperation(op_setbit_dp, new SMPCoreOpArgument() { op = 1, value = 0x08 });
            opcode_table[0x63] = new SMPCoreOperation(op_bitbranch, new SMPCoreOpArgument() { mask = 0x08, value = Convert.ToInt32(true) });
            opcode_table[0x64] = new SMPCoreOperation(op_read_reg_dp, new SMPCoreOpArgument() { op_func = op_cmp, n = (int)OpCode.A });
            opcode_table[0x65] = new SMPCoreOperation(op_read_reg_addr, new SMPCoreOpArgument() { op_func = op_cmp, n = (int)OpCode.A });
            opcode_table[0x66] = new SMPCoreOperation(op_read_a_ix, new SMPCoreOpArgument() { op_func = op_cmp });
            opcode_table[0x67] = new SMPCoreOperation(op_read_a_idpx, new SMPCoreOpArgument() { op_func = op_cmp });
            opcode_table[0x68] = new SMPCoreOperation(op_read_reg_const, new SMPCoreOpArgument() { op_func = op_cmp, n = (int)OpCode.A });
            opcode_table[0x69] = new SMPCoreOperation(op_read_dp_dp, new SMPCoreOpArgument() { op_func = op_cmp });
            opcode_table[0x6a] = new SMPCoreOperation(op_and1_bit, new SMPCoreOpArgument() { op = 1 });
            opcode_table[0x6b] = new SMPCoreOperation(op_adjust_dp, new SMPCoreOpArgument() { op_func = op_ror });
            opcode_table[0x6c] = new SMPCoreOperation(op_adjust_addr, new SMPCoreOpArgument() { op_func = op_ror });
            opcode_table[0x6d] = new SMPCoreOperation(op_push_reg, new SMPCoreOpArgument() { n = (int)OpCode.Y });
            opcode_table[0x6e] = new SMPCoreOperation(op_dbnz_dp, null);
            opcode_table[0x6f] = new SMPCoreOperation(op_ret, null);
            opcode_table[0x70] = new SMPCoreOperation(op_branch, new SMPCoreOpArgument() { flag = 0x40, value = Convert.ToInt32(true) });
            opcode_table[0x71] = new SMPCoreOperation(op_tcall, new SMPCoreOpArgument() { n = 7 });
            opcode_table[0x72] = new SMPCoreOperation(op_setbit_dp, new SMPCoreOpArgument() { op = 0, value = 0x08 });
            opcode_table[0x73] = new SMPCoreOperation(op_bitbranch, new SMPCoreOpArgument() { mask = 0x08, value = Convert.ToInt32(false) });
            opcode_table[0x74] = new SMPCoreOperation(op_read_a_dpx, new SMPCoreOpArgument() { op_func = op_cmp });
            opcode_table[0x75] = new SMPCoreOperation(op_read_a_addrr, new SMPCoreOpArgument() { op_func = op_cmp, i = (int)OpCode.X });
            opcode_table[0x76] = new SMPCoreOperation(op_read_a_addrr, new SMPCoreOpArgument() { op_func = op_cmp, i = (int)OpCode.Y });
            opcode_table[0x77] = new SMPCoreOperation(op_read_a_idpy, new SMPCoreOpArgument() { op_func = op_cmp });
            opcode_table[0x78] = new SMPCoreOperation(op_read_dp_const, new SMPCoreOpArgument() { op_func = op_cmp });
            opcode_table[0x79] = new SMPCoreOperation(op_read_ix_iy, new SMPCoreOpArgument() { op_func = op_cmp });
            opcode_table[0x7a] = new SMPCoreOperation(op_read_ya_dp, new SMPCoreOpArgument() { op_func = op_addw });
            opcode_table[0x7b] = new SMPCoreOperation(op_adjust_dpx, new SMPCoreOpArgument() { op_func = op_ror });
            opcode_table[0x7c] = new SMPCoreOperation(op_adjust_reg, new SMPCoreOpArgument() { op_func = op_ror, n = (int)OpCode.A });
            opcode_table[0x7d] = new SMPCoreOperation(op_mov_reg_reg, new SMPCoreOpArgument() { to = (int)OpCode.A, from = (int)OpCode.X });
            opcode_table[0x7e] = new SMPCoreOperation(op_read_reg_dp, new SMPCoreOpArgument() { op_func = op_cmp, n = (int)OpCode.Y });
            opcode_table[0x7f] = new SMPCoreOperation(op_reti, null);
            opcode_table[0x80] = new SMPCoreOperation(op_setbit, new SMPCoreOpArgument() { mask = 0x01, value = 0x01 });
            opcode_table[0x81] = new SMPCoreOperation(op_tcall, new SMPCoreOpArgument() { n = 8 });
            opcode_table[0x82] = new SMPCoreOperation(op_setbit_dp, new SMPCoreOpArgument() { op = 1, value = 0x10 });
            opcode_table[0x83] = new SMPCoreOperation(op_bitbranch, new SMPCoreOpArgument() { mask = 0x10, value = Convert.ToInt32(true) });
            opcode_table[0x84] = new SMPCoreOperation(op_read_reg_dp, new SMPCoreOpArgument() { op_func = op_adc, n = (int)OpCode.A });
            opcode_table[0x85] = new SMPCoreOperation(op_read_reg_addr, new SMPCoreOpArgument() { op_func = op_adc, n = (int)OpCode.A });
            opcode_table[0x86] = new SMPCoreOperation(op_read_a_ix, new SMPCoreOpArgument() { op_func = op_adc });
            opcode_table[0x87] = new SMPCoreOperation(op_read_a_idpx, new SMPCoreOpArgument() { op_func = op_adc });
            opcode_table[0x88] = new SMPCoreOperation(op_read_reg_const, new SMPCoreOpArgument() { op_func = op_adc, n = (int)OpCode.A });
            opcode_table[0x89] = new SMPCoreOperation(op_read_dp_dp, new SMPCoreOpArgument() { op_func = op_adc });
            opcode_table[0x8a] = new SMPCoreOperation(op_eor1_bit, null);
            opcode_table[0x8b] = new SMPCoreOperation(op_adjust_dp, new SMPCoreOpArgument() { op_func = op_dec });
            opcode_table[0x8c] = new SMPCoreOperation(op_adjust_addr, new SMPCoreOpArgument() { op_func = op_dec });
            opcode_table[0x8d] = new SMPCoreOperation(op_mov_reg_const, new SMPCoreOpArgument() { n = (int)OpCode.Y });
            opcode_table[0x8e] = new SMPCoreOperation(op_pop_p, null);
            opcode_table[0x8f] = new SMPCoreOperation(op_mov_dp_const, null);
            opcode_table[0x90] = new SMPCoreOperation(op_branch, new SMPCoreOpArgument() { flag = 0x01, value = Convert.ToInt32(false) });
            opcode_table[0x91] = new SMPCoreOperation(op_tcall, new SMPCoreOpArgument() { n = 9 });
            opcode_table[0x92] = new SMPCoreOperation(op_setbit_dp, new SMPCoreOpArgument() { op = 0, value = 0x10 });
            opcode_table[0x93] = new SMPCoreOperation(op_bitbranch, new SMPCoreOpArgument() { mask = 0x10, value = Convert.ToInt32(false) });
            opcode_table[0x94] = new SMPCoreOperation(op_read_a_dpx, new SMPCoreOpArgument() { op_func = op_adc });
            opcode_table[0x95] = new SMPCoreOperation(op_read_a_addrr, new SMPCoreOpArgument() { op_func = op_adc, i = (int)OpCode.X });
            opcode_table[0x96] = new SMPCoreOperation(op_read_a_addrr, new SMPCoreOpArgument() { op_func = op_adc, i = (int)OpCode.Y });
            opcode_table[0x97] = new SMPCoreOperation(op_read_a_idpy, new SMPCoreOpArgument() { op_func = op_adc });
            opcode_table[0x98] = new SMPCoreOperation(op_read_dp_const, new SMPCoreOpArgument() { op_func = op_adc });
            opcode_table[0x99] = new SMPCoreOperation(op_read_ix_iy, new SMPCoreOpArgument() { op_func = op_adc });
            opcode_table[0x9a] = new SMPCoreOperation(op_read_ya_dp, new SMPCoreOpArgument() { op_func = op_subw });
            opcode_table[0x9b] = new SMPCoreOperation(op_adjust_dpx, new SMPCoreOpArgument() { op_func = op_dec });
            opcode_table[0x9c] = new SMPCoreOperation(op_adjust_reg, new SMPCoreOpArgument() { op_func = op_dec, n = (int)OpCode.A });
            opcode_table[0x9d] = new SMPCoreOperation(op_mov_reg_reg, new SMPCoreOpArgument() { to = (int)OpCode.X, from = (int)OpCode.SP });
            opcode_table[0x9e] = new SMPCoreOperation(op_div_ya_x, null);
            opcode_table[0x9f] = new SMPCoreOperation(op_xcn, null);
            opcode_table[0xa0] = new SMPCoreOperation(op_seti, new SMPCoreOpArgument() { value = 1 });
            opcode_table[0xa1] = new SMPCoreOperation(op_tcall, new SMPCoreOpArgument() { n = 10 });
            opcode_table[0xa2] = new SMPCoreOperation(op_setbit_dp, new SMPCoreOpArgument() { op = 1, value = 0x20 });
            opcode_table[0xa3] = new SMPCoreOperation(op_bitbranch, new SMPCoreOpArgument() { mask = 0x20, value = Convert.ToInt32(true) });
            opcode_table[0xa4] = new SMPCoreOperation(op_read_reg_dp, new SMPCoreOpArgument() { op_func = op_sbc, n = (int)OpCode.A });
            opcode_table[0xa5] = new SMPCoreOperation(op_read_reg_addr, new SMPCoreOpArgument() { op_func = op_sbc, n = (int)OpCode.A });
            opcode_table[0xa6] = new SMPCoreOperation(op_read_a_ix, new SMPCoreOpArgument() { op_func = op_sbc });
            opcode_table[0xa7] = new SMPCoreOperation(op_read_a_idpx, new SMPCoreOpArgument() { op_func = op_sbc });
            opcode_table[0xa8] = new SMPCoreOperation(op_read_reg_const, new SMPCoreOpArgument() { op_func = op_sbc, n = (int)OpCode.A });
            opcode_table[0xa9] = new SMPCoreOperation(op_read_dp_dp, new SMPCoreOpArgument() { op_func = op_sbc });
            opcode_table[0xaa] = new SMPCoreOperation(op_mov1_c_bit, null);
            opcode_table[0xab] = new SMPCoreOperation(op_adjust_dp, new SMPCoreOpArgument() { op_func = op_inc });
            opcode_table[0xac] = new SMPCoreOperation(op_adjust_addr, new SMPCoreOpArgument() { op_func = op_inc });
            opcode_table[0xad] = new SMPCoreOperation(op_read_reg_const, new SMPCoreOpArgument() { op_func = op_cmp, n = (int)OpCode.Y });
            opcode_table[0xae] = new SMPCoreOperation(op_pop_reg, new SMPCoreOpArgument() { n = (int)OpCode.A });
            opcode_table[0xaf] = new SMPCoreOperation(op_mov_ixinc_a, null);
            opcode_table[0xb0] = new SMPCoreOperation(op_branch, new SMPCoreOpArgument() { flag = 0x01, value = Convert.ToInt32(true) });
            opcode_table[0xb1] = new SMPCoreOperation(op_tcall, new SMPCoreOpArgument() { n = 11 });
            opcode_table[0xb2] = new SMPCoreOperation(op_setbit_dp, new SMPCoreOpArgument() { op = 0, value = 0x20 });
            opcode_table[0xb3] = new SMPCoreOperation(op_bitbranch, new SMPCoreOpArgument() { mask = 0x20, value = Convert.ToInt32(false) });
            opcode_table[0xb4] = new SMPCoreOperation(op_read_a_dpx, new SMPCoreOpArgument() { op_func = op_sbc });
            opcode_table[0xb5] = new SMPCoreOperation(op_read_a_addrr, new SMPCoreOpArgument() { op_func = op_sbc, i = (int)OpCode.X });
            opcode_table[0xb6] = new SMPCoreOperation(op_read_a_addrr, new SMPCoreOpArgument() { op_func = op_sbc, i = (int)OpCode.Y });
            opcode_table[0xb7] = new SMPCoreOperation(op_read_a_idpy, new SMPCoreOpArgument() { op_func = op_sbc });
            opcode_table[0xb8] = new SMPCoreOperation(op_read_dp_const, new SMPCoreOpArgument() { op_func = op_sbc });
            opcode_table[0xb9] = new SMPCoreOperation(op_read_ix_iy, new SMPCoreOpArgument() { op_func = op_sbc });
            opcode_table[0xba] = new SMPCoreOperation(op_movw_ya_dp, null);
            opcode_table[0xbb] = new SMPCoreOperation(op_adjust_dpx, new SMPCoreOpArgument() { op_func = op_inc });
            opcode_table[0xbc] = new SMPCoreOperation(op_adjust_reg, new SMPCoreOpArgument() { op_func = op_inc, n = (int)OpCode.A });
            opcode_table[0xbd] = new SMPCoreOperation(op_mov_sp_x, null);
            opcode_table[0xbe] = new SMPCoreOperation(op_das, null);
            opcode_table[0xbf] = new SMPCoreOperation(op_mov_a_ixinc, null);
            opcode_table[0xc0] = new SMPCoreOperation(op_seti, new SMPCoreOpArgument() { value = 0 });
            opcode_table[0xc1] = new SMPCoreOperation(op_tcall, new SMPCoreOpArgument() { n = 12 });
            opcode_table[0xc2] = new SMPCoreOperation(op_setbit_dp, new SMPCoreOpArgument() { op = 1, value = 0x40 });
            opcode_table[0xc3] = new SMPCoreOperation(op_bitbranch, new SMPCoreOpArgument() { mask = 0x40, value = Convert.ToInt32(true) });
            opcode_table[0xc4] = new SMPCoreOperation(op_mov_dp_reg, new SMPCoreOpArgument() { n = (int)OpCode.A });
            opcode_table[0xc5] = new SMPCoreOperation(op_mov_addr_reg, new SMPCoreOpArgument() { n = (int)OpCode.A });
            opcode_table[0xc6] = new SMPCoreOperation(op_mov_ix_a, null);
            opcode_table[0xc7] = new SMPCoreOperation(op_mov_idpx_a, null);
            opcode_table[0xc8] = new SMPCoreOperation(op_read_reg_const, new SMPCoreOpArgument() { op_func = op_cmp, n = (int)OpCode.X });
            opcode_table[0xc9] = new SMPCoreOperation(op_mov_addr_reg, new SMPCoreOpArgument() { n = (int)OpCode.X });
            opcode_table[0xca] = new SMPCoreOperation(op_mov1_bit_c, null);
            opcode_table[0xcb] = new SMPCoreOperation(op_mov_dp_reg, new SMPCoreOpArgument() { n = (int)OpCode.Y });
            opcode_table[0xcc] = new SMPCoreOperation(op_mov_addr_reg, new SMPCoreOpArgument() { n = (int)OpCode.Y });
            opcode_table[0xcd] = new SMPCoreOperation(op_mov_reg_const, new SMPCoreOpArgument() { n = (int)OpCode.X });
            opcode_table[0xce] = new SMPCoreOperation(op_pop_reg, new SMPCoreOpArgument() { n = (int)OpCode.X });
            opcode_table[0xcf] = new SMPCoreOperation(op_mul_ya, null);
            opcode_table[0xd0] = new SMPCoreOperation(op_branch, new SMPCoreOpArgument() { flag = 0x02, value = Convert.ToInt32(false) });
            opcode_table[0xd1] = new SMPCoreOperation(op_tcall, new SMPCoreOpArgument() { n = 13 });
            opcode_table[0xd2] = new SMPCoreOperation(op_setbit_dp, new SMPCoreOpArgument() { op = 1, value = 0x40 });
            opcode_table[0xd3] = new SMPCoreOperation(op_bitbranch, new SMPCoreOpArgument() { mask = 0x40, value = Convert.ToInt32(false) });
            opcode_table[0xd4] = new SMPCoreOperation(op_mov_dpr_reg, new SMPCoreOpArgument() { n = (int)OpCode.A, i = (int)OpCode.X });
            opcode_table[0xd5] = new SMPCoreOperation(op_mov_addrr_a, new SMPCoreOpArgument() { i = (int)OpCode.X });
            opcode_table[0xd6] = new SMPCoreOperation(op_mov_addrr_a, new SMPCoreOpArgument() { i = (int)OpCode.Y });
            opcode_table[0xd7] = new SMPCoreOperation(op_mov_idpy_a, null);
            opcode_table[0xd8] = new SMPCoreOperation(op_mov_dp_reg, new SMPCoreOpArgument() { n = (int)OpCode.X });
            opcode_table[0xd9] = new SMPCoreOperation(op_mov_dpr_reg, new SMPCoreOpArgument() { n = (int)OpCode.X, i = (int)OpCode.Y });
            opcode_table[0xda] = new SMPCoreOperation(op_movw_dp_ya, null);
            opcode_table[0xdb] = new SMPCoreOperation(op_mov_dpr_reg, new SMPCoreOpArgument() { n = (int)OpCode.Y, i = (int)OpCode.X });
            opcode_table[0xdc] = new SMPCoreOperation(op_adjust_reg, new SMPCoreOpArgument() { op_func = op_dec, n = (int)OpCode.Y });
            opcode_table[0xdd] = new SMPCoreOperation(op_mov_reg_reg, new SMPCoreOpArgument() { to = (int)OpCode.A, from = (int)OpCode.Y });
            opcode_table[0xde] = new SMPCoreOperation(op_cbne_dpx, null);
            opcode_table[0xdf] = new SMPCoreOperation(op_daa, null);
            opcode_table[0xe0] = new SMPCoreOperation(op_setbit, new SMPCoreOpArgument() { mask = 0x48, value = 0x00 });
            opcode_table[0xe1] = new SMPCoreOperation(op_tcall, new SMPCoreOpArgument() { n = 14 });
            opcode_table[0xe2] = new SMPCoreOperation(op_setbit_dp, new SMPCoreOpArgument() { op = 1, value = 0x80 });
            opcode_table[0xe3] = new SMPCoreOperation(op_bitbranch, new SMPCoreOpArgument() { mask = 0x80, value = Convert.ToInt32(true) });
            opcode_table[0xe4] = new SMPCoreOperation(op_mov_reg_dp, new SMPCoreOpArgument() { n = (int)OpCode.A });
            opcode_table[0xe5] = new SMPCoreOperation(op_mov_reg_addr, new SMPCoreOpArgument() { n = (int)OpCode.A });
            opcode_table[0xe6] = new SMPCoreOperation(op_mov_a_ix, null);
            opcode_table[0xe7] = new SMPCoreOperation(op_mov_a_idpx, null);
            opcode_table[0xe8] = new SMPCoreOperation(op_mov_reg_const, new SMPCoreOpArgument() { n = (int)OpCode.A });
            opcode_table[0xe9] = new SMPCoreOperation(op_mov_reg_addr, new SMPCoreOpArgument() { n = (int)OpCode.X });
            opcode_table[0xea] = new SMPCoreOperation(op_not1_bit, null);
            opcode_table[0xeb] = new SMPCoreOperation(op_mov_reg_dp, new SMPCoreOpArgument() { n = (int)OpCode.Y });
            opcode_table[0xec] = new SMPCoreOperation(op_mov_reg_addr, new SMPCoreOpArgument() { n = (int)OpCode.Y });
            opcode_table[0xed] = new SMPCoreOperation(op_notc, null);
            opcode_table[0xee] = new SMPCoreOperation(op_pop_reg, new SMPCoreOpArgument() { n = (int)OpCode.Y });
            opcode_table[0xef] = new SMPCoreOperation(op_wait, null);
            opcode_table[0xf0] = new SMPCoreOperation(op_branch, new SMPCoreOpArgument() { flag = 0x02, value = Convert.ToInt32(true) });
            opcode_table[0xf1] = new SMPCoreOperation(op_tcall, new SMPCoreOpArgument() { n = 15 });
            opcode_table[0xf2] = new SMPCoreOperation(op_setbit_dp, new SMPCoreOpArgument() { op = 0, value = 0x80 });
            opcode_table[0xf3] = new SMPCoreOperation(op_bitbranch, new SMPCoreOpArgument() { mask = 0x80, value = Convert.ToInt32(false) });
            opcode_table[0xf4] = new SMPCoreOperation(op_mov_reg_dpr, new SMPCoreOpArgument() { n = (int)OpCode.A, i = (int)OpCode.X });
            opcode_table[0xf5] = new SMPCoreOperation(op_mov_a_addrr, new SMPCoreOpArgument() { i = (int)OpCode.X });
            opcode_table[0xf6] = new SMPCoreOperation(op_mov_a_addrr, new SMPCoreOpArgument() { i = (int)OpCode.Y });
            opcode_table[0xf7] = new SMPCoreOperation(op_mov_a_idpy, null);
            opcode_table[0xf8] = new SMPCoreOperation(op_mov_reg_dp, new SMPCoreOpArgument() { n = (int)OpCode.X });
            opcode_table[0xf9] = new SMPCoreOperation(op_mov_reg_dpr, new SMPCoreOpArgument() { n = (int)OpCode.X, i = (int)OpCode.Y });
            opcode_table[0xfa] = new SMPCoreOperation(op_mov_dp_dp, null);
            opcode_table[0xfb] = new SMPCoreOperation(op_mov_reg_dpr, new SMPCoreOpArgument() { n = (int)OpCode.Y, i = (int)OpCode.X });
            opcode_table[0xfc] = new SMPCoreOperation(op_adjust_reg, new SMPCoreOpArgument() { op_func = op_inc, n = (int)OpCode.Y });
            opcode_table[0xfd] = new SMPCoreOperation(op_mov_reg_reg, new SMPCoreOpArgument() { to = (int)OpCode.Y, from = (int)OpCode.A });
            opcode_table[0xfe] = new SMPCoreOperation(op_dbnz_y, null);
            opcode_table[0xff] = new SMPCoreOperation(op_wait, null);
        }

        public SMPCore()
        {
            initialize_opcode_table();
        }
    }
}
