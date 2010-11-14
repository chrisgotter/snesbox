using System.Collections.ObjectModel;
using System.Threading;

namespace Nall
{
    public static class Libco
    {
        public static bool _alive = true;
        public static bool Alive
        {
            get
            {
                return _alive;
            }
        }

        private static Thread _active;
        private static Collection<Thread> _threads = new Collection<Thread>();

        public static Thread Active()
        {
            if (ReferenceEquals(_active, null))
            {
                _active = Thread.CurrentThread;
            }
            return _active;
        }

        public static Thread Create(string name, int size, ThreadStart entrypoint)
        {
            if (ReferenceEquals(_active, null))
            {
                _active = Thread.CurrentThread;
            }

            size += 256; /* allocate additional space for storage */
            size &= ~15; /* align stack to 16-byte boundary */
            var thread = new Thread(entrypoint, size) { Name = name };
            _threads.Add(thread);
            return thread;
        }

        public static void Delete(Thread thread)
        {
            thread.Abort();
            thread = null;
        }

        public static void Kill()
        {
            _alive = false;
            foreach (var thread in _threads)
            {
                thread.Resume();
            }
        }

        public static void Switch(Thread thread)
        {
            var previous = _active;
            _active = thread;
            if (_active.ThreadState == ThreadState.Unstarted)
            {
                _active.Start();
            }
            else
            {
                while (_active.ThreadState != ThreadState.Suspended) { }
                _active.Resume();
            }
            previous.Suspend();
        }
    }
}
