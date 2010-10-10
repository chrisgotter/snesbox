using System;

namespace Nall
{
    public delegate void Callback(uint arg);
    //priority queue implementation using binary min-heap array;
    //does not require normalize() function.
    //O(1)     find   (tick)
    //O(log n) insert (enqueue)
    //O(log n) remove (dequeue)

    public class PriorityQueue
    {
        public void priority_queue_nocallback(uint arg) { }

        public void tick(uint ticks) { throw new NotImplementedException(); }

        //counter is relative to current time (eg enqueue(64, ...) fires in 64 ticks);
        //counter cannot exceed std::numeric_limits<uint>::max() >> 1.
        public void enqueue(uint counter, uint Event) { throw new NotImplementedException(); }

        public uint dequeue() { throw new NotImplementedException(); }

        public void reset() { throw new NotImplementedException(); }

        public void serialize(Serializer s) { throw new NotImplementedException(); }

        public PriorityQueue(uint size, Callback callback_ = null)
        {
            if (ReferenceEquals(callback_, null))
            {
                callback_ = priority_queue_nocallback;
            }
        }

        private Callback callback;
        private uint basecounter;
        private uint heapsize;
        private uint heapcapacity;

        private struct Heap
        {
            uint counter;
            uint Event;
        }
        Heap heap;

        //return true if x is greater than or equal to y
        private bool gte(uint x, uint y) { throw new NotImplementedException(); }
    }
}
