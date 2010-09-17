using System;
using System.Diagnostics;
using System.Threading;

namespace Nall
{
    public class SnesThread : SuspendableThread
    {
        private ThreadStart Work { get; set; }
        private string Name { get; set; }

        public SnesThread(string name, int maxStackSize, ThreadStart start)
            : base()
        {
            Name = name;
            Work = start;
        }

        public SnesThread(Thread thread)
        {
            _thread = thread;
        }

        protected override void OnDoWork()
        {
            if (!string.IsNullOrEmpty(Name))
            {
                _thread.Name = Name;
            }

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
