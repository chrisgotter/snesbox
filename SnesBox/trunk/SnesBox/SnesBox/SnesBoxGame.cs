using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Snes;

namespace SnesBox
{
    public class SnesBoxGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager _graphics;
        Snes _snes = new Snes();

        public SnesBoxGame()
        {
            IsFixedTimeStep = false;
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;

            Components.Add(new FrameRate(this));
            Components.Add(new Audio(this, _snes));
            Components.Add(new Video(this, _snes));
        }

        protected override void Initialize()
        {
            _snes.SetControllerPortDevice(1, LibSnes.SnesDevice.JOYPAD);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            using (var fs = new FileStream("SMW.smc", FileMode.Open))
            {
                var rom = new byte[fs.Length];
                fs.Read(rom, 0, (int)fs.Length);
                _snes.LoadCartridge(new NormalCartridge() { RomData = rom });
            }
        }

        protected override void Update(GameTime gameTime)
        {
            var buttonStates = Input.ParseInput(GamePad.GetState(PlayerIndex.One));
            _snes.SetInputState(1, 0, (int)buttonStates, 0, 0);
            _snes.RunToFrame();

            var frameRate = (IFrameRateService)Services.GetService(typeof(IFrameRateService));
            Window.Title = string.Format("{0:##} FPS", frameRate.FPS);
            base.Update(gameTime);
        }
    }
}
