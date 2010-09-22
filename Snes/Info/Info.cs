
namespace Snes
{
    static class Info
    {
        public const string Name = "bsnes";
        public const string Version = "068";
        public const uint SerializerVersion = 12;
        public const string Profile =
#if ACCURACY
 "Accuracy";
#elif COMPATIBILITY
 "Compatibility";
#elif PERFORMANCE
 "Performance";
#endif
    }
}
