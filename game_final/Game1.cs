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

        private MouseState _previousMouseState;

        private int _mouseX;
        private int _mouseY;

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

            Environments.GameData.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Assets.Initialize(Content, _spriteBatch, _graphics.GraphicsDevice);

            //MediaPlayer.Play(AssetTypes.Sound.MusicSound);
            //MediaPlayer.Volume = 0.2f;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            MouseState mouseState = Mouse.GetState();
            _mouseX = mouseState.X;
            _mouseY = mouseState.Y;

            Assets.Shooter.Update(mouseState, _previousMouseState);

            _previousMouseState = mouseState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            Assets.PlayingScene.Draw();
            Assets.Shooter.Draw();


            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
