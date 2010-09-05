using System;

namespace Nall
{
    public class ModuloArray<T>
    {
        public T this[int index] { get { throw new NotImplementedException(); } }

        public T read(int index) { throw new NotImplementedException(); }

        public void write(uint index, T value) { throw new NotImplementedException(); }

        public ModuloArray(int size) { throw new NotImplementedException(); }

        private T[] buffer;
    }
}
