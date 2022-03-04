using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using game_final.Utils;

namespace game_final
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Sprite _rectangle;

        private const int WIDTH = 800;
        private const int HEIGHT = 800;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = WIDTH;
            _graphics.PreferredBackBufferHeight = HEIGHT;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _rectangle = new Sprites.Rectangle(_graphics.GraphicsDevice, 50, 50);
            Vector2 center =_rectangle.Center;
            _rectangle.SetPosition(WIDTH / 2 - center.X, HEIGHT - center.Y);
            _rectangle.SetColor(Color.Red);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            MouseState mouse = Mouse.GetState();
            int mouseX = mouse.X;
            int mouseY = mouse.Y;

            _rectangle.Rotation = (float) Math.Atan2(mouseY - HEIGHT, mouseX - WIDTH / 2);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_rectangle.Instance, _rectangle.Position, null, Color.White, _rectangle.Rotation, _rectangle.Center, 1f, SpriteEffects.None, 0f);
            
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
