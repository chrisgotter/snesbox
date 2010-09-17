using System;
using System.Threading;

namespace Nall
{
    //COPYRIGHT PETER RITCHIE
    //http://msmvps.com/blogs/peterritchie/archive/2006/10/13/_2700_System.Threading.Thread.Suspend_280029002700_-is-obsolete_3A00_-_2700_Thread.Suspend-has-been-deprecated_2E002E002E00_.aspx
    public abstract class SuspendableThread
    {
        #region Data
        private ManualResetEvent _suspendChangedEvent = new ManualResetEvent(false);
        private ManualResetEvent _terminateEvent = new ManualResetEvent(false);
        private long _suspended;
        protected Thread _thread;
        private System.Threading.ThreadState _failsafeThreadState = System.Threading.ThreadState.Unstarted;
        #endregion Data

        public SuspendableThread()
        {
        }

        private void ThreadEntry()
        {
            _failsafeThreadState = System.Threading.ThreadState.Stopped;
            OnDoWork();
        }

        protected abstract void OnDoWork();

        #region Protected methods
        protected Boolean SuspendIfNeeded()
        {
            Boolean suspendEventChanged = _suspendChangedEvent.WaitOne(0, true);
            if (suspendEventChanged)
            {
                Boolean needToSuspend = Interlocked.Read(ref _suspended) != 0;
                _suspendChangedEvent.Reset();
                if (needToSuspend)
                {
                    /// Suspending...
                    if (1 == WaitHandle.WaitAny(new WaitHandle[] { _suspendChangedEvent, _terminateEvent }))
                    {
                        return true;
                    }
                    /// ...Waking
                }
            }
            return false;
        }

        protected bool HasTerminateRequest()
        {
            return _terminateEvent.WaitOne(0, true);
        }
        #endregion Protected methods

        public void Start()
        {
            _thread = new Thread(new ThreadStart(ThreadEntry));

            // make sure this thread won't be automaticaly
            // terminated by the runtime when the
            // application exits
            _thread.IsBackground = false;

            _thread.Start();
        }

        public void Join()
        {
            if (!ReferenceEquals(_thread, null))
            {
                _thread.Join();
            }
        }

        public Boolean Join(Int32 milliseconds)
        {
            if (!ReferenceEquals(_thread, null))
            {
                return _thread.Join(milliseconds);
            }
            return true;
        }

        /// <remarks>Not supported in .NET Compact Framework</remarks>
        public Boolean Join(TimeSpan timeSpan)
        {
            if (!ReferenceEquals(_thread, null))
            {
                return _thread.Join(timeSpan);
            }
            return true;
        }

        public void Terminate()
        {
            _terminateEvent.Set();
        }

        public void TerminateAndWait()
        {
            _terminateEvent.Set();
            _thread.Join();
        }

        public void Suspend()
        {
            while (1 != Interlocked.Exchange(ref _suspended, 1))
            {
            }
            _suspendChangedEvent.Set();
        }

        public void Resume()
        {
            if (ReferenceEquals(_thread, null))
            {
                Start();
            }

            while (0 != Interlocked.Exchange(ref _suspended, 0))
            {
            }
            _suspendChangedEvent.Set();
        }

        public System.Threading.ThreadState ThreadState
        {
            get
            {
                if (!ReferenceEquals(_thread, null))
                {
                    return _thread.ThreadState;
                }
                return _failsafeThreadState;
            }
        }
    }
}
