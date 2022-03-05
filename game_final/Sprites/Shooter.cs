using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Diagnostics;
using game_final.Utils;

namespace game_final.Sprites
{
    class Shooter : Base.SpriteGroup
    {
        public int GuideLength = 1;
        public Vector2 ReflectPoint;

        private GraphicsDevice _graphics;

        private Shapes.Rectangle _body;
        private Shapes.Line _guide;
        private Shapes.Line _reflectGuide;

        public Shooter(SpriteBatch spriteBatch, GraphicsDevice graphics) : base(spriteBatch, 50, 50)
        {
            ReflectPoint = new Vector2(0, 0);

            SetPosition(Settings.WINDOW_WIDTH / 2 - _width / 2, Settings.WINDOW_HEIGHT - _height);

            _graphics = graphics;

            _body = new Shapes.Rectangle(graphics, _width, _height);
            _body.SetColor(Color.Red);
            _body.SetOrigin(_width / 2, _height / 2);
            _body.SetPosition(_width / 2, _height / 2);
        }

        public void Update(MouseState mouseState, MouseState previousMouseState)
        {
            int mouseX = mouseState.X;
            int mouseY = mouseState.Y;

            float rotation = (float)Math.Atan2(mouseY - (Assets.Shooter.Y + Assets.Shooter.Width / 2), mouseX - (Assets.Shooter.X + Assets.Shooter.Height / 2)) + Converter.DegressToRadians(180);
            float rotationDegrees = Converter.RadiansToDegrees(rotation);

            if (mouseX < Settings.WINDOW_WIDTH / 2 && (rotationDegrees < 10 || rotationDegrees > 270))
            {
                rotation = Converter.DegressToRadians(10);
            }
            else if (mouseX > Settings.WINDOW_WIDTH / 2 && rotationDegrees > 170)
            {
                rotation = Converter.DegressToRadians(170);
            }

            Rotation = rotation;

            int reflectX = rotation < Math.PI / 2 ? 0 : rotation > Math.PI / 2 ? 800 : 0;

            if (rotation > Math.PI / 2) rotation = (float)(Math.PI - rotation);

            int reflectY = (int)((Settings.WINDOW_HEIGHT - Assets.Shooter.Height / 2) - (Settings.WINDOW_WIDTH / 2 * Math.Tan(rotation)));

            int diffX = Settings.WINDOW_WIDTH / 2;
            int diffY = reflectY - (Settings.WINDOW_HEIGHT - Assets.Shooter.Height / 2);

            int length = (int)Math.Ceiling(Math.Sqrt(diffX * diffX + diffY * diffY));

            GuideLength = length;
            SetReflectPoint(reflectX, reflectY);

            if (GuideLength > Settings.MAX_GUIDE_LENGTH || GuideLength <= 0) GuideLength = Settings.MAX_GUIDE_LENGTH;

            _guide = new Shapes.Line(_graphics, _width / 2, _height / 2, _width / 2, 0, 5, GuideLength);
            _guide.SetColor(Color.Black);
            _guide.SetOrigin(5 / 2, _guide.Height);
            _guide.SetPosition(_width / 2, _height / 2);

            if (GuideLength < Settings.MAX_GUIDE_LENGTH)
            {
                int pointY = -(int)(400 * Math.Tan(Rotation));
                _reflectGuide = new Shapes.Line(_graphics, 0, 0, Settings.WINDOW_WIDTH / 2, pointY, 5, Settings.MAX_GUIDE_LENGTH - GuideLength);
                _reflectGuide.SetColor(Color.Black);
                _reflectGuide.SetOrigin(2, 0);
                _reflectGuide.SetPosition(ReflectPoint.X, ReflectPoint.Y);
                _reflectGuide.Rotation += Converter.DegressToRadians(ReflectPoint.X < 400 ? -90 : 90);
            }
            else
            {
                _reflectGuide = null;
            }
        }

        public void Draw()
        {
            DrawSprite(_body);
            DrawSprite(_guide);

            if (_reflectGuide != null)
            {
                _spriteBatch.Draw(_reflectGuide.Instance, _reflectGuide.Position, null, _reflectGuide.Color, _reflectGuide.Rotation, _reflectGuide.Origin, 1f, SpriteEffects.None, 0f);
            }
        }

        public void SetReflectPoint(int x, int y)
        {
            ReflectPoint.X = x;
            ReflectPoint.Y = y;
        }
    }
}
