using System;
using System.Collections.Generic;
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

        private float _rotation = 0f;
        private Vector2 _unitVector;

        public Shooter(SpriteBatch spriteBatch, GraphicsDevice graphics) : base(spriteBatch, 50, 50)
        {
            ReflectPoint = new Vector2(0, 0);

            SetPosition(Constants.REFLECT_CENTER_X - _width / 2, Settings.WINDOW_HEIGHT - _height);

            _graphics = graphics;

            _body = new Shapes.Rectangle(graphics, _width, _height);
            _body.SetColor(Color.Red);
            _body.SetOrigin(_width / 2, _height / 2);
            _body.SetPosition(_width / 2, _height / 2);

            _unitVector = new Vector2((float)Math.Cos(_rotation), (float)Math.Sin(_rotation));
        }

        private void updateUnitVector()
        {
            _unitVector.X = (float)Math.Cos(_rotation);
            _unitVector.Y = (float)Math.Sin(_rotation);
        }

        public void Update(MouseState mouseState, MouseState previousMouseState)
        {
            Environments.GameData.ShotBalls.RemoveAll(b => b.isDestroyed);

            foreach (Sprites.Ball ball in Environments.GameData.ShotBalls)
            {
                ball.Update();
            }

            int mouseX = mouseState.X;
            int mouseY = mouseState.Y;

            _rotation = (float)Math.Atan2(mouseY - (Assets.Shooter.Y + Assets.Shooter.Width / 2), mouseX - (Assets.Shooter.X + Assets.Shooter.Height / 2)) + Converter.DegressToRadians(180);

            float rotationDegrees = Converter.RadiansToDegrees(_rotation);

            if (mouseX < Constants.REFLECT_CENTER_X && (rotationDegrees < 10 || rotationDegrees > 270))
            {
                _rotation = Converter.DegressToRadians(10);
            }
            else if (mouseX > Constants.REFLECT_CENTER_X && rotationDegrees > 170)
            {
                _rotation = Converter.DegressToRadians(170);
            }

            Rotation = _rotation;

            updateUnitVector();

            bool isClicked = mouseState.LeftButton != previousMouseState.LeftButton && mouseState.LeftButton == ButtonState.Pressed;
            if (isClicked)
            {
                Environments.GameData.ShotBalls.Add(new Sprites.Ball(Environments.Ball.BallType.LIGHT_BLUE, (int)(X + Width / 2), (int)(Y + Height / 2), -_unitVector));
            }

            int reflectX = _rotation < Math.PI / 2 ? Constants.REFLECT_LEFT : _rotation > Math.PI / 2 ? Constants.REFLECT_RIGHT : Constants.REFLECT_CENTER_X;

            if (_rotation > Math.PI / 2) _rotation = (float)(Math.PI - _rotation);

            int reflectY = (int)((Settings.WINDOW_HEIGHT - Assets.Shooter.Height / 2) - (Constants.PLAY_HALF_WIDTH * Math.Tan(_rotation)));

            int diffX = Constants.PLAY_HALF_WIDTH;
            int diffY = reflectY - (Settings.WINDOW_HEIGHT - Assets.Shooter.Height / 2);

            int length = (int)Math.Ceiling(Math.Sqrt(diffX * diffX + diffY * diffY));

            GuideLength = length;
            SetReflectPoint(reflectX, reflectY);

            if (GuideLength > Settings.MAX_GUIDE_LENGTH || GuideLength <= 0) GuideLength = Settings.MAX_GUIDE_LENGTH;

            _guide = new Shapes.Line(_graphics, _width / 2, _height / 2, _width / 2, 0, 5, GuideLength);
            _guide.SetColor(Color.Black);
            _guide.SetOrigin(5 / 2, _guide.Height);
            _guide.SetPosition(_width / 2, _height / 2);

            if (_reflectGuide != null)
            {
                _reflectGuide.Destroy();
                _reflectGuide = null;
            }

            if (GuideLength < Settings.MAX_GUIDE_LENGTH)
            {
                float relativeRotation = (float)(Rotation > Math.PI / 2 ? Math.PI - Rotation : Rotation);

                float unitX = (float)(Rotation > Math.PI / 2 ? Math.Cos(relativeRotation) : -Math.Cos(relativeRotation));
                float unitY = (float)Math.Sin(relativeRotation);
                int pointX = (int)(reflectX + unitX * int.MaxValue) ;
                int pointY = (int)(reflectY + unitY * int.MaxValue);

                _reflectGuide = new Shapes.Line(_graphics, reflectX, reflectY, pointX, pointY, 5, Settings.MAX_GUIDE_LENGTH - GuideLength);
                _reflectGuide.SetColor(Color.Black);
                _reflectGuide.SetOrigin(2, 0);
                _reflectGuide.SetPosition(ReflectPoint.X, ReflectPoint.Y);
                _reflectGuide.Rotation += Converter.DegressToRadians(90);
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

            foreach (Sprites.Ball ball in Environments.GameData.ShotBalls)
            {
                _spriteBatch.Draw(ball.Instance, ball.Position, null, ball.DrawColor, ball.Rotation, ball.Origin, ball.Scale, SpriteEffects.None, 0f);
            }
        }

        public void SetReflectPoint(int x, int y)
        {
            ReflectPoint.X = x;
            ReflectPoint.Y = y;
        }
    }
}
