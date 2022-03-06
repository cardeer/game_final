using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using System;
using game_final.Utils;
using System.Diagnostics;

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

            Window.Title = "Midterm Game Project";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Environments.Global.Graphics = _graphics.GraphicsDevice;
            Environments.Global.SpriteBatch = _spriteBatch;

            Assets.Initialize(Content);

            //MediaPlayer.Play(AssetTypes.Sound.MusicSound);
            //MediaPlayer.Volume = 0.2f;

            Environments.Global.CurrentScene = new Scenes.Playing();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Environments.Global.GameTime = gameTime;

            // TODO: Add your update logic here
            Environments.Global.PreviousMouseState = Environments.Global.CurrentMouseState;

            MouseState mouseState = Mouse.GetState();
            Environments.Global.CurrentMouseState = mouseState;

            Environments.Global.CurrentScene.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            Environments.Global.CurrentScene.Draw();
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
