using System;

namespace SnesBox
{
    public enum SnesMemoryType
    {
        CartridgeRam = 0,
        CartridgeRtc = 1,
        BsxRam = 2,
        BsxPram = 3,
        SufamiTurboARam = 4,
        SufamiTurboBRam = 5,
        GameBoyRam = 6,
        GameBoyRtc = 7
    }

    public enum SnesDevice
    {
        None = 0,
        Joypad = 1,
        Multitap = 2,
        Mouse = 3,
        SuperScope = 4,
        Justifier = 5,
        Justifiers = 6
    }

    [Flags]
    public enum SnesJoypadButtons
    {
        None = 0,
        B = 1 << 0,
        Y = 1 << 1,
        Select = 1 << 2,
        Start = 1 << 3,
        Up = 1 << 4,
        Down = 1 << 5,
        Left = 1 << 6,
        Right = 1 << 7,
        A = 1 << 8,
        X = 1 << 9,
        L = 1 << 10,
        R = 1 << 11,
        All = -1
    }

    [Flags]
    public enum SnesMouseButtons
    {
        None = 0,
        //X     = 1 << 0,
        //Y     = 1 << 1,
        Left = 1 << 2,
        Right = 1 << 3,
        All = -1
    }

    [Flags]
    public enum SnesSuperScopeButtons
    {
        None = 0,
        //X       = 1 << 0,
        //Y       = 1 << 1,
        Trigger = 1 << 2,
        Cursor = 1 << 3,
        Turbo = 1 << 4,
        Pause = 1 << 5,
        All = -1
    }

    [Flags]
    public enum SnesJustifierButtons
    {
        None = 0,
        //X       = 1 << 0,
        //Y       = 1 << 1,
        Trigger = 1 << 2,
        Start = 1 << 3,
        All = -1
    }
}
