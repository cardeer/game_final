using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace game_final
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = Settings.WINDOW_WIDTH;
            _graphics.PreferredBackBufferHeight = Settings.WINDOW_HEIGHT;
            _graphics.ApplyChanges();

            Window.Title = Settings.GAME_NAME;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Environments.Global.Graphics = _graphics.GraphicsDevice;
            Environments.Global.SpriteBatch = _spriteBatch;
            Environments.Global.Content = Content;

            Assets.Initialize(Content);

            Environments.Scene.SetScene(Types.SceneType.SPLASH);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Environments.Global.WindowActive = IsActive;
            Environments.Global.GameTime = gameTime;

            // TODO: Add your update logic here
            Environments.Global.PreviousMouseState = Environments.Global.CurrentMouseState;

            MouseState mouseState = Mouse.GetState();
            Environments.Global.CurrentMouseState = mouseState;

            if (Environments.Scene.CurrentScene != null && Environments.Scene.CurrentScene.IsReady)
            {
                Environments.Scene.CurrentScene.Update();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            if (Environments.Scene.CurrentScene != null && Environments.Scene.CurrentScene.IsReady)
            {
                Environments.Scene.CurrentScene.Draw();
            }
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
