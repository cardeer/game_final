using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
            _graphics.PreferredBackBufferHeight = Settings.WINDOW_WIDTH;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Assets.Initialize(Content, _spriteBatch, _graphics.GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            MouseState mouseState = Mouse.GetState();
            _mouseX = mouseState.X;
            _mouseY = mouseState.Y;

            _previousMouseState = mouseState;

            float rotation = (float)Math.Atan2(_mouseY - (Assets.Shooter.Y + Assets.Shooter.Width / 2), _mouseX - (Assets.Shooter.X + Assets.Shooter.Height / 2)) + Converter.DegressToRadians(180);

            float rotationDegrees = Converter.RadiansToDegrees(rotation);

            if (rotationDegrees < 10 || rotationDegrees > 170)
            {
                if (rotationDegrees <= 270 && rotationDegrees > 90)
                {
                    rotationDegrees = 170;
                }
                else if (rotationDegrees > 270 || rotationDegrees >= 0)
                {
                    rotationDegrees = 10;
                }
            }

            rotation = Converter.DegressToRadians(rotationDegrees);
            Assets.Shooter.Rotation = rotation;

            Vector2 unit = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));

            int touchX = rotation < Math.PI / 2 ? 0 : rotation > Math.PI / 2 ? 800 : 0;

            if (rotation > Math.PI / 2) rotation = (float)(Math.PI - rotation);

            int touchY = (int)((Settings.WINDOW_HEIGHT - 50) - (Settings.WINDOW_WIDTH / 2 * Math.Tan(rotation)));

            int diffX = Settings.WINDOW_WIDTH / 2;
            int diffY = touchY - (Settings.WINDOW_HEIGHT - Assets.Shooter.Height);

            int length = (int)Math.Ceiling(Math.Sqrt(diffX * diffX + diffY * diffY));

            Assets.Shooter.GuideLength = length;
            Assets.Shooter.SetReflectPoint(touchX, touchY);

            Assets.Shooter.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            Assets.Shooter.Draw();

            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
