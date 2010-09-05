using System;

namespace Snes
{
    public delegate void EntryPoint();
    public delegate void Operation();

    class Processor
    {
        public Object thread;
        public uint frequency;
        public long clock;

        public void create(EntryPoint entryPoint, uint frequency_) { throw new NotImplementedException(); }

        public Processor() { throw new NotImplementedException(); }
    }
}
