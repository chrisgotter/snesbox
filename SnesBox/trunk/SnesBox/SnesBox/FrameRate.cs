using System;
using Microsoft.Xna.Framework;

namespace SnesBox
{
    public interface IFrameRateService
    {
        string FPS { get; }
    }

    public class FrameRate : DrawableGameComponent, IFrameRateService
    {
        int _frameRate = 0;
        int _frameCounter = 0;
        TimeSpan _elapsedTime = TimeSpan.Zero;
        public string FPS { get; private set; }

        public FrameRate(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(IFrameRateService), this);
        }

        public override void Update(GameTime gameTime)
        {
            _elapsedTime += gameTime.ElapsedGameTime;

            if (_elapsedTime > TimeSpan.FromSeconds(1))
            {
                _elapsedTime -= TimeSpan.FromSeconds(1);
                _frameRate = _frameCounter;
                _frameCounter = 0;
            }
        }


        public override void Draw(GameTime gameTime)
        {
            _frameCounter++;

            FPS = _frameRate.ToString();
        }
    }
}
