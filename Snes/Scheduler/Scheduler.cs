using System;

namespace Snes.Scheduler
{
    class Scheduler
    {
        public enum SynchronizeMode : uint { None, CPU, All }
        SynchronizeMode sync;

        public enum ExitReason : uint { UnknownEvent, FrameEvent, SynchronizeEvent, DebuggerEvent }
        public ExitReason exit_reason;

        public Object host_thread; //program thread (used to exit emulation)
        public Object thread; //active emulation thread (used to enter emulation)

        public void enter() { throw new NotImplementedException(); }
        public void exit(ExitReason reason) { throw new NotImplementedException(); }

        public void init() { throw new NotImplementedException(); }
        public Scheduler() { throw new NotImplementedException(); }
    }
}
