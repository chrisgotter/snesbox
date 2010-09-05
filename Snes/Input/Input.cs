using System;

namespace Snes.Input
{
    partial class Input
    {
        public enum Device : uint
        {
            None,
            Joypad,
            Multitap,
            Mouse,
            SuperScope,
            Justifier,
            Justifiers,
        }

        public enum JoypadID : uint
        {
            B = 0, Y = 1, Select = 2, Start = 3,
            Up = 4, Down = 5, Left = 6, Right = 7,
            A = 8, X = 9, L = 10, R = 11,
        }

        public enum MouseID : uint
        {
            X = 0, Y = 1, Left = 2, Right = 3,
        }

        public enum SuperScopeID : uint
        {
            X = 0, Y = 1, Trigger = 2, Cursor = 3, Turbo = 4, Pause = 5,
        }

        public enum JustifierID : uint
        {
            X = 0, Y = 1, Trigger = 2, Start = 3,
        }

        public byte port_read(bool port) { throw new NotImplementedException(); }
        public void port_set_device(bool port, Device device) { throw new NotImplementedException(); }
        public void init() { throw new NotImplementedException(); }
        public void poll() { throw new NotImplementedException(); }
        public void update() { throw new NotImplementedException(); }

        //light guns (Super Scope, Justifier(s)) strobe IOBit whenever the CRT
        //beam cannon is detected. this needs to be tested at the cycle level
        //(hence inlining here for speed) to avoid 'dead space' during DRAM refresh.
        //iobit is updated during port_set_device(),
        //latchx, latchy are updated during update() (once per frame)

        public void tick() { throw new NotImplementedException(); }

        private bool iobit;
        private short latchx, latchy;

        private Port[] port = new Port[2];
    }
}
