using System;

namespace Nall
{
    public delegate void EntryPoint();

    public class Cothread
    {
    }

    public static class Libco
    {
        public static Cothread Active() { throw new NotImplementedException(); }
        public static Cothread Create(uint size, EntryPoint entrypoint) { throw new NotImplementedException(); }
        public static void Delete(Cothread handle) { throw new NotImplementedException(); }
        public static void Switch(Cothread handle) { throw new NotImplementedException(); }
    }
}
