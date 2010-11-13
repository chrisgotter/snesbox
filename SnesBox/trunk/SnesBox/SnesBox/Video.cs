using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SnesBox
{
    class Video : DrawableGameComponent
    {
        public enum Filters { None, HQ2X }

        private Texture2D _videoFrame;
        private Color[] _videoBuffer;
        private Rectangle _videoRect;
        private Dictionary<Filters, Effect> _effects = new Dictionary<Filters, Effect>();

        private SpriteBatch SpriteBatch { get; set; }

        public Video(Game game, Snes snes)
            : base(game)
        {
            snes.VideoUpdated += new VideoUpdatedEventHandler(OnVideoUpdated);
        }

        public override void Initialize()
        {
            _videoFrame = new Texture2D(Game.GraphicsDevice, 512, 512, false, SurfaceFormat.Color);
            _videoBuffer = new Color[512 * 512];

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            LoadFilters();
        }

        private void LoadFilters()
        {
            var viewport = GraphicsDevice.Viewport;
            var projection = Matrix.CreateOrthographicOffCenter(0, viewport.Width, viewport.Height, 0, 0, 1);
            var halfPixelOffset = Matrix.CreateTranslation(-0.5f, -0.5f, 0);

            foreach (var effectType in Enum.GetValues(typeof(Filters)))
            {
                var effect = Game.Content.Load<Effect>(@"Content\" + effectType.ToString());
                effect.Parameters["MatrixTransform"].SetValue(halfPixelOffset * projection);
                effect.Parameters["TextureSize"].SetValue(new Vector2(_videoFrame.Width, _videoFrame.Height));
                _effects.Add((Filters)effectType, effect);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            var vp = GraphicsDevice.Viewport;

            SpriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, _effects[Filters.HQ2X]);
            Game.GraphicsDevice.SamplerStates[0] = new SamplerState() { Filter = TextureFilter.Point };
            SpriteBatch.Draw(_videoFrame, new Rectangle(0, 0, vp.Width, vp.Height), _videoRect, Color.White);
            SpriteBatch.End();

            base.Draw(gameTime);
        }

        void OnVideoUpdated(object sender, VideoUpdatedEventArgs e)
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
