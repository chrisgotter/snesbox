using System;
using System.Collections.ObjectModel;
using System.Linq;
using Snes;

namespace SnesBox
{
    class Snes
    {
        public static readonly int VersionMajor;
        public static readonly int VersionMinor;
        private static Collection<uint> audio_buffer = new Collection<uint>();
        private static volatile ushort[] input_buttons = new ushort[8];
        private static volatile int[] input_coords = new int[8];

        static Snes()
        {
            VersionMajor = (int)LibSnes.snes_library_revision_major();
            VersionMinor = (int)LibSnes.snes_library_revision_minor();
        }

        Cartridge _cartridge;
        public event VideoUpdatedEventHandler VideoUpdated;
        public event AudioUpdatedEventHandler AudioUpdated;

        public Snes()
        {
            _cartridge = null;

            LibSnes.snes_init();
            LibSnes.snes_audio_sample += new LibSnes.SnesAudioSample(LibSnes_snes_audio_sample);
            LibSnes.snes_video_refresh += new LibSnes.SnesVideoRefresh(LibSnes_snes_video_refresh);
            LibSnes.snes_input_state += new LibSnes.SnesInputState(LibSnes_snes_input_state);
        }

        short LibSnes_snes_input_state(bool port, uint device, uint index, uint id)
        {
            int i = (int)((port ? 1 : 0) * 4 + index);

            if (device >= (uint)LibSnes.SnesDevice.MOUSE && id <= 1)
            {
                return (short)(input_coords[i] >> (int)(id * 16));
            }
            else
            {
                return Convert.ToInt16((input_buttons[i] & (1 << (int)id)) != 0);
            }
        }

        void LibSnes_snes_video_refresh(ArraySegment<ushort> data, uint width, uint height)
        {
            VideoUpdated(this, new VideoUpdatedEventArgs(data, (int)width, (int)height));
            AudioUpdated(this, new AudioUpdatedEventArgs(audio_buffer.ToArray(), audio_buffer.Count));
            audio_buffer.Clear();
        }

        void LibSnes_snes_audio_sample(ushort left, ushort right)
        {
            audio_buffer.Add((uint)((right << 16) | left));
        }

        public Cartridge Cartridge
        {
            get
            {
                if (_cartridge != null)
                {
                    _cartridge.Refresh();
                }

                return _cartridge;
            }
        }

        public void SetControllerPortDevice(int port, LibSnes.SnesDevice device)
        {
            if (port < 1 || port > 2)
            {
                throw new ArgumentOutOfRangeException("port");
            }

            LibSnes.snes_set_controller_port_device(port == 2 ? true : false, (uint)device);
        }

        public void RunToFrame()
        {
            if (_cartridge == null)
            {
                throw new InvalidOperationException("No rom loaded.");
            }

            LibSnes.snes_run();
        }

        public void PowerCycle()
        {
            LibSnes.snes_power();
        }

        public void Reset()
        {
            LibSnes.snes_reset();
        }

        public void SetInputState(int port, int index, int buttonStates, int x, int y)
        {
            if (port < 1 || port > 2) { throw new ArgumentOutOfRangeException("port"); }

            LibSnes.SnesDeviceIdJoypad leftRight = LibSnes.SnesDeviceIdJoypad.LEFT | LibSnes.SnesDeviceIdJoypad.RIGHT;
            if ((buttonStates & (int)leftRight) == (int)(leftRight))
            {
                buttonStates &= (int)~leftRight;
            }

            LibSnes.SnesDeviceIdJoypad upDown = LibSnes.SnesDeviceIdJoypad.UP | LibSnes.SnesDeviceIdJoypad.DOWN;
            if ((buttonStates & (int)upDown) == (int)upDown)
            {
                buttonStates &= (int)~upDown;
            }

            SetInputState((uint)(port - 1), (uint)index, (ushort)buttonStates, (int)(((ushort)y << 16) | (ushort)x));
        }

        void SetInputState(uint port, uint index, ushort buttons, int coords)
        {
            int i = (int)(port * 4 + index);
            input_buttons[i] = buttons;
            input_coords[i] = coords;
        }

        public void LoadCartridge(Cartridge cartridge)
        {
            if (_cartridge != null)
            {
                _cartridge.Refresh();
                LibSnes.snes_unload_cartridge();
            }

            _cartridge = cartridge;
            cartridge.Load(this);
        }
    }
}
