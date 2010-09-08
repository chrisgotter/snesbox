using Nall;

namespace Snes.Scheduler
{
    class Scheduler
    {
        public enum SynchronizeMode : uint { None, CPU, All }
        SynchronizeMode sync;

        public enum ExitReason : uint { UnknownEvent, FrameEvent, SynchronizeEvent, DebuggerEvent }
        public ExitReason exit_reason;

        public Cothread host_thread; //program thread (used to exit emulation)
        public Cothread thread; //active emulation thread (used to enter emulation)

        public void enter()
        {
            host_thread = Libco.Active();
            Libco.Switch(thread);
        }

        public void exit(ExitReason reason)
        {
            exit_reason = reason;
            thread = Libco.Active();
            Libco.Switch(host_thread);
        }

        public void init()
        {
            host_thread = Libco.Active();
            thread = CPU.CPU.cpu.Processor.thread;
            sync = SynchronizeMode.None;
        }

        public Scheduler()
        {
            host_thread = null;
            thread = null;
            exit_reason = ExitReason.UnknownEvent;
        }
    }
}
