using System;
using Nall;

namespace Snes
{
    partial class PPU
    {
        private class LayerWindow
        {
            public bool one_enable;
            public bool one_invert;
            public bool two_enable;
            public bool two_invert;

            public uint mask;

            public bool main_enable;
            public bool sub_enable;

            public byte[] main = new byte[256];
            public byte[] sub = new byte[256];

            public void render(bool screen) { throw new NotImplementedException(); }
            public void serialize(Serializer s) { throw new NotImplementedException(); }
        }
    }
}
