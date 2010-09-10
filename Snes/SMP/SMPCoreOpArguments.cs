using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snes
{
    public delegate byte SingleByteFunction(byte x);
    public delegate byte DoubleByteFunction(byte x, byte y);
    public delegate ushort DoubleUShortFunction(ushort x, ushort y);

    public class SMPCoreOpArguments
    {
        public byte x_byte { get; set; }
        public byte y_byte { get; set; }
        public ushort x_ushort { get; set; }
        public ushort y_ushort { get; set; }
        public int to { get; set; }
        public int from { get; set; }
        public int n { get; set; }
        public int i { get; set; }
        public int flag { get; set; }
        public int value { get; set; }
        public int mask { get; set; }
        public DoubleByteFunction op_DoubleByteFunction { get; set; }
        public DoubleUShortFunction op_DoubleUShortFunction { get; set; }
        public int op { get; set; }
        public SingleByteFunction op_SingleByteFunction { get; set; }
        public int adjust { get; set; }
    }
}
