using System.Threading;

namespace Nall
{
    public static class Libco
    {
        private static SnesThread _active;

        public static SnesThread Active()
        {
            if (ReferenceEquals(_active, null))
            {
                _active = new SnesThread(Thread.CurrentThread);
            }
            return _active;
        }

        public static SnesThread Create(string name, int size, ThreadStart entrypoint)
        {
            if (ReferenceEquals(_active, null))
            {
                _active = new SnesThread(Thread.CurrentThread);
            }

            size += 256; /* allocate additional space for storage */
            size &= ~15; /* align stack to 16-byte boundary */
            return new SnesThread(name, size, entrypoint);
        }

        public static void Delete(SnesThread handle)
        {
            handle.Terminate();
            handle = null;
        }

        public static void Switch(SnesThread handle)
        {
            var previous = _active;
            _active = handle;
            _active.Resume();
            previous.Suspend();
        }
    }
}
