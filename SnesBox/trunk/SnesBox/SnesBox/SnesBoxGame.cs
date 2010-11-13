using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Snes;

namespace SnesBox
{
    public class SnesBoxGame : Microsoft.Xna.Framework.Game
    {
        enum Filters { None, HQ2X }
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        Snes _snes = new Snes();
        FrameRateComponent _frameRate;
        Dictionary<LibSnes.SnesDeviceIdJoypad, Buttons> _snesToXnaButtons = new Dictionary<LibSnes.SnesDeviceIdJoypad, Buttons>();

        Texture2D _videoFrame;
        Color[] _videoBuffer;
        Rectangle _videoRect;
        Dictionary<Filters, Effect> _effects = new Dictionary<Filters, Effect>();

        DynamicSoundEffectInstance _audioFrame;

        public SnesBoxGame()
        {
            IsFixedTimeStep = false;
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.IsFullScreen = true;

            _frameRate = new FrameRateComponent(this);
            Components.Add(_frameRate);

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

        protected override void Initialize()
        {
            _audioFrame = new DynamicSoundEffectInstance(32040, AudioChannels.Stereo);
            _audioFrame.Play();

            _snes.SetControllerPortDevice(1, LibSnes.SnesDevice.JOYPAD);
            _snes.VideoUpdated += new VideoUpdatedEventHandler(Snes_VideoUpdated);
            _snes.AudioUpdated += new AudioUpdatedEventHandler(Snes_AudioUpdated);

            _videoFrame = new Texture2D(GraphicsDevice, 512, 512, false, SurfaceFormat.Color);
            _videoBuffer = new Color[512 * 512];

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var viewport = GraphicsDevice.Viewport;
            var projection = Matrix.CreateOrthographicOffCenter(0, viewport.Width, viewport.Height, 0, 0, 1);
            var halfPixelOffset = Matrix.CreateTranslation(-0.5f, -0.5f, 0);

            foreach (var effectType in Enum.GetValues(typeof(Filters)))
            {
                var effect = Content.Load<Effect>(@"Content\" + effectType.ToString());
                effect.Parameters["MatrixTransform"].SetValue(halfPixelOffset * projection);
                effect.Parameters["TextureSize"].SetValue(new Vector2(_videoFrame.Width, _videoFrame.Height));
                _effects.Add((Filters)effectType, effect);
            }

            using (FileStream fs = new FileStream("SMW.smc", FileMode.Open))
            {
                var rom = new byte[fs.Length];
                fs.Read(rom, 0, (int)fs.Length);
                _snes.LoadCartridge(new NormalCartridge() { RomData = rom });
            }
        }

        protected override void Update(GameTime gameTime)
        {
            var buttonStates = ParseInput(GamePad.GetState(PlayerIndex.One));

            _snes.SetInputState(1, 0, (int)buttonStates, 0, 0);
            _snes.RunToFrame();

            Window.Title = string.Format("{0:##} FPS", _frameRate.FrameRate);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            var vp = GraphicsDevice.Viewport;

            _spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, _effects[Filters.HQ2X]);
            _graphics.GraphicsDevice.SamplerStates[0] = new SamplerState() { Filter = TextureFilter.Point };
            _spriteBatch.Draw(_videoFrame, new Rectangle(0, 0, vp.Width, vp.Height), _videoRect, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private LibSnes.SnesDeviceIdJoypad ParseInput(GamePadState gamePadState)
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

        void Snes_AudioUpdated(object sender, AudioUpdatedEventArgs e)
        {
            var audioBuffer = new byte[e.SampleCount * 4];
            int bufferIndex = 0;

            for (int i = 0; i < e.AudioBuffer.Length; i++)
            {
                var samples = BitConverter.GetBytes(e.AudioBuffer[i]);
                audioBuffer[bufferIndex++] = samples[0];
                audioBuffer[bufferIndex++] = samples[1];
                audioBuffer[bufferIndex++] = samples[2];
                audioBuffer[bufferIndex++] = samples[3];
            }

            if (audioBuffer.Length > 0)
            {
                _audioFrame.SubmitBuffer(audioBuffer, 0, audioBuffer.Length);
            }
        }

        void Snes_VideoUpdated(object sender, VideoUpdatedEventArgs e)
        {
            bool interlace = (e.Height >= 240);
            uint pitch = interlace ? 1024U : 2048U;
            pitch >>= 1;

            for (int y = 0; y < e.Height; y++)
            {
                for (int x = 0; x < e.Width; x++)
                {
                    ushort color = e.VideoBuffer.Array[e.VideoBuffer.Offset + (y * pitch) + x];
                    int b;

                    b = ((color >> 10) & 31) * 8;
                    var red = (byte)(b + b / 35);
                    b = ((color >> 5) & 31) * 8;
                    var green = (byte)(b + b / 35);
                    b = ((color >> 0) & 31) * 8;
                    var blue = (byte)(b + b / 35);
                    var alpha = (byte)255;

                    _videoBuffer[y * _videoFrame.Width + x] = new Color() { R = red, G = green, B = blue, A = alpha };
                }
            }

            GraphicsDevice.Textures[0] = null;
            _videoFrame.SetData<Color>(_videoBuffer);
            _videoRect = new Rectangle(0, 0, e.Width, e.Height);
        }
    }
}
