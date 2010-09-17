using System.Threading;

namespace Nall
{
    public static class Libco
    {
        private static Thread _main = null;

        public static Thread Active()
        {
            return Thread.CurrentThread;
        }

        public static Thread Create(int size, ThreadStart entrypoint)
        {
            if (ReferenceEquals(_main, null))
            {
                _main = Thread.CurrentThread;
            }

            size += 256; /* allocate additional space for storage */
            size &= ~15; /* align stack to 16-byte boundary */
            return new Thread(entrypoint, size);
        }

        public static void Delete(Thread handle)
        {
            handle.Abort();
            handle = null;
        }

        public static void Switch(Thread handle)
        {
            if (!ReferenceEquals(Thread.CurrentThread, _main))
            {
                Thread.CurrentThread.Priority = ThreadPriority.Lowest;
            }
            handle.Priority = ThreadPriority.Normal;
            if (handle.ThreadState == ThreadState.Unstarted)
            {
                handle.Start();
            }
            Thread.Yield();
        }
    }
}
