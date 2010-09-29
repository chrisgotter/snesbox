using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace SnesBox
{
    public class SnesBoxGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Snes _snes = new Snes();
        Texture2D _videoFrame;
        DynamicSoundEffectInstance _audioFrame;

        public SnesBoxGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 597;
            graphics.PreferredBackBufferHeight = 448;
        }

        protected override void Initialize()
        {
            _videoFrame = new Texture2D(GraphicsDevice, 256, 224, false, SurfaceFormat.Color);
            _audioFrame = new DynamicSoundEffectInstance(32040, AudioChannels.Stereo);

            _snes.VideoUpdated += new VideoUpdatedEventHandler(Snes_VideoUpdated);
            _snes.AudioUpdated += new AudioUpdatedEventHandler(Snes_AudioUpdated);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            using (FileStream fs = new FileStream("SMW.smc", FileMode.Open))
            {
                var rom = new byte[fs.Length];
                fs.Read(rom, 0, (int)fs.Length);
                _snes.LoadCartridge(new NormalCartridge() { RomData = rom });
            }
        }

        protected override void Update(GameTime gameTime)
        {
            _snes.RunToFrame();
            Window.Title = gameTime.IsRunningSlowly ? "Slow" : string.Empty;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            var vp = GraphicsDevice.Viewport;
            spriteBatch.Begin();
            spriteBatch.Draw(_videoFrame, new Rectangle(0, 0, vp.Width, vp.Height), Color.White);
            spriteBatch.End();

            _audioFrame.Play();

            base.Draw(gameTime);
        }

        void Snes_AudioUpdated(object sender, AudioUpdatedEventArgs e)
        {
            var audioBuffer = new byte[e.SampleCount * 4];
            int bufferIndex = 0;

            for (int i = 0; i < e.AudioBuffer.Length; i++)
            {
                var samples = BitConverter.GetBytes(e.AudioBuffer[bufferIndex++]);
                audioBuffer[i++] = samples[0];
                audioBuffer[i++] = samples[1];
            }

            if (audioBuffer.Length > 0)
            {
                _audioFrame.SubmitBuffer(audioBuffer, 0, audioBuffer.Length);
            }
        }

        void Snes_VideoUpdated(object sender, VideoUpdatedEventArgs e)
        {
            var videoBuffer = new uint[_videoFrame.Width * _videoFrame.Height];
            int bufferIndex = 0;
            _videoFrame.GetData<uint>(videoBuffer);

            for (int y = 0; y < e.Height; y++)
            {
                for (int x = 0; x < e.Width; x++)
                {
                    ushort color = e.VideoBuffer.Array[e.VideoBuffer.Offset + bufferIndex];
                    int b;

                    b = ((color >> 10) & 31) * 8;
                    var red = (byte)(b + b / 35);
                    b = ((color >> 5) & 31) * 8;
                    var green = (byte)(b + b / 35);
                    b = ((color >> 0) & 31) * 8;
                    var blue = (byte)(b + b / 35);
                    var alpha = (byte)255;

                    videoBuffer[y * e.Width + x] = new Color() { R = red, G = green, B = blue, A = alpha }.PackedValue;
                }

                bufferIndex += e.Height > 256 ? 512 - e.Width : 1024 - e.Width;
            }

            GraphicsDevice.Textures[0] = null;
            _videoFrame.SetData<uint>(videoBuffer);
        }
    }
}
