
namespace Snes
{
    partial class Input
    {
        partial struct Port
        {
            public struct Superscope
            {
                public int x, y;

                public bool trigger;
                public bool cursor;
                public bool turbo;
                public bool pause;
                public bool offscreen;

                public bool turbolock;
                public bool triggerlock;
                public bool pauselock;
            }
        }
    }
}
