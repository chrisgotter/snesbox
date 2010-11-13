using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Snes;

namespace SnesBox.Console
{
    static class Input
    {
        private static Dictionary<LibSnes.SnesDeviceIdJoypad, Buttons> _snesToXnaButtons = new Dictionary<LibSnes.SnesDeviceIdJoypad, Buttons>();

        static Input()
        {
            _snesToXnaButtons.Add(LibSnes.SnesDeviceIdJoypad.A, Buttons.B);
            _snesToXnaButtons.Add(LibSnes.SnesDeviceIdJoypad.B, Buttons.A);
            _snesToXnaButtons.Add(LibSnes.SnesDeviceIdJoypad.X, Buttons.Y);
            _snesToXnaButtons.Add(LibSnes.SnesDeviceIdJoypad.Y, Buttons.X);
            _snesToXnaButtons.Add(LibSnes.SnesDeviceIdJoypad.L, Buttons.LeftShoulder);
            _snesToXnaButtons.Add(LibSnes.SnesDeviceIdJoypad.R, Buttons.RightShoulder);
            _snesToXnaButtons.Add(LibSnes.SnesDeviceIdJoypad.START, Buttons.Start);
            _snesToXnaButtons.Add(LibSnes.SnesDeviceIdJoypad.SELECT, Buttons.Back);
            _snesToXnaButtons.Add(LibSnes.SnesDeviceIdJoypad.DOWN, Buttons.DPadDown);
            _snesToXnaButtons.Add(LibSnes.SnesDeviceIdJoypad.UP, Buttons.DPadUp);
            _snesToXnaButtons.Add(LibSnes.SnesDeviceIdJoypad.LEFT, Buttons.DPadLeft);
            _snesToXnaButtons.Add(LibSnes.SnesDeviceIdJoypad.RIGHT, Buttons.DPadRight);
        }

        public static LibSnes.SnesDeviceIdJoypad ParseInput(GamePadState gamePadState)
        {
            var snesButtonStates = default(LibSnes.SnesDeviceIdJoypad);
            var xnaButtonStates = gamePadState.Buttons;

            foreach (LibSnes.SnesDeviceIdJoypad button in Enum.GetValues(typeof(LibSnes.SnesDeviceIdJoypad)))
            {
                if (gamePadState.IsButtonDown(_snesToXnaButtons[button]))
                {
                    snesButtonStates |= button;
                }
            }

            return snesButtonStates;
        }
    }
}
