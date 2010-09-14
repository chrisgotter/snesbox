using System.Threading;

namespace Nall
{
    public delegate void EntryPoint();

    public static class Libco
    {
        private static Thread _active;
        private static Mutex _mutex = new Mutex(true);

        public static Thread Active()
        {
            if (ReferenceEquals(_active, null))
            {
                _active = Thread.CurrentThread;
            }
            return _active;
        }

        public static Thread Create(int size, EntryPoint entrypoint)
        {
            if (ReferenceEquals(_active, null))
            {
                _active = Thread.CurrentThread;
            }

            size += 256; /* allocate additional space for storage */
            size &= ~15; /* align stack to 16-byte boundary */
            return new Thread(new ThreadStart(entrypoint), size);
        }

        public static void Delete(Thread handle)
        {
            handle.Abort();
            handle = null;
        }

        public static void Switch(Thread handle)
        {
            _mutex.ReleaseMutex();
            handle.Start();
            _mutex.WaitOne();
            _active = handle;
        }
    }
}
