using System;
using System.Diagnostics;
using System.Threading;

namespace Nall
{
    public class SnesThread : SuspendableThread
    {
        private ThreadStart Work { get; set; }

        public SnesThread(ThreadStart start, int maxStackSize)
            : base()
        {
            Work = start;
        }

        public SnesThread(Thread thread)
        {
            _thread = thread;
        }

        protected override void OnDoWork()
        {
            try
            {
                while (false == HasTerminateRequest())
                {
                    Boolean awokenByTerminate = SuspendIfNeeded();
                    if (awokenByTerminate)
                    {
                        return;
                    }

                    Work();
                }
            }
            finally { }
        }
    }
}
