
namespace Snes
{
    partial class Input
    {
        public partial struct Port
        {
            public Device device;
            public uint counter0; //read counters
            public uint counter1;

            public Superscope superscope;
            public Justifier justifier;
        }
    }
}
