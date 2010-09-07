
namespace Nall
{
    public class ModuloArray<T>
    {
        public T this[int index]
        {
            get { return buffer[_size + index]; }
        }

        public T read(int index)
        {
            return buffer[_size + index];
        }

        public void write(uint index, T value)
        {
            buffer[index] = buffer[index + _size] = buffer[index + _size + _size] = value;
        }

        public ModuloArray(int size)
        {
            _size = size;
            buffer = new T[_size * 3];
        }

        private int _size;
        private T[] buffer;
    }
}
