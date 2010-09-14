using System;
using Snes;

namespace SnesBox
{
    class Snes
    {
        public static readonly int VersionMajor;
        public static readonly int VersionMinor;
        public static readonly uint MaxVideoBufferLength;
        public static readonly uint MaxAudioBufferLength;

        static Snes()
        {
            VersionMajor = (int)LibSnes.snes_library_revision_major();
            VersionMinor = (int)LibSnes.snes_library_revision_minor();
            //LibSnes.snes_get_buffer_lengths(ref MaxVideoBufferLength, ref MaxAudioBufferLength);
        }

        Cartridge _cartridge;
        ClrSnesFrameCompleteHandler _frameCompleteHandler;

        public event VideoUpdatedEventHandler VideoUpdated;
        public event AudioUpdatedEventHandler AudioUpdated;

        public Snes()
        {
            _cartridge = null;

            _frameCompleteHandler = new ClrSnesFrameCompleteHandler(FrameCompleteHandler);
            //LibSnes.snes_set_frame_complete(_frameCompleteHandler);

            LibSnes.snes_init();
        }

        public Cartridge Cartridge
        {
            get
            {
                if (_cartridge != null) { _cartridge.Refresh(); }

                return _cartridge;
            }
        }

        public void SetControllerPortDevice(int port, SnesDevice device)
        {
            if (port < 1 || port > 2) { throw new ArgumentOutOfRangeException("port"); }

            LibSnes.snes_set_controller_port_device(port == 2 ? true : false, (uint)device);
        }

        public void RunToFrame()
        {
            if (_cartridge == null) { throw new InvalidOperationException("No rom loaded."); }

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

            SnesJoypadButtons leftRight = SnesJoypadButtons.Left | SnesJoypadButtons.Right;
            if ((buttonStates & (int)leftRight) == (int)(leftRight))
            {
                buttonStates &= (int)~leftRight;
            }

            SnesJoypadButtons upDown = SnesJoypadButtons.Up | SnesJoypadButtons.Down;
            if ((buttonStates & (int)upDown) == (int)upDown)
            {
                buttonStates &= (int)~upDown;
            }

            //LibSnes.snes_set_input_state((uint)port - 1, (uint)index, (ushort)buttonStates, (uint)(((ushort)y << 16) | (ushort)x));
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

        void FrameCompleteHandler(IntPtr videoBuffer, uint width, uint height, IntPtr audioBuffer, uint count)
        {
            if (VideoUpdated != null)
            {
                VideoUpdated(this, new VideoUpdatedEventArgs(videoBuffer, (int)width, (int)height));
            }

            if (AudioUpdated != null)
            {
                AudioUpdated(this, new AudioUpdatedEventArgs(audioBuffer, (int)count));
            }
        }

        internal delegate void ClrSnesFrameCompleteHandler(IntPtr videoData, uint width, uint height, IntPtr audioData, uint count);
    }
}
