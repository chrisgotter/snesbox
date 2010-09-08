using Nall;

namespace Snes
{
    public delegate void Operation();

    class Processor : IProcessor
    {
        public Cothread thread;
        public uint frequency;
        public long clock;

        public void create(EntryPoint entryPoint, uint frequency_)
        {
            if (!ReferenceEquals(thread, null))
                Libco.Delete(thread);

            thread = Libco.Create(65536 * sizeof(int), entryPoint);
            frequency = frequency_;
            clock = 0;
        }

        public Processor()
        {
            thread = null;
        }

        Processor IProcessor.Processor
        {
            get
            {
                return this;
            }
        }
    }
}
