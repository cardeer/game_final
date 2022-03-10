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

        private Base.Sprite _body;

        private Shapes.Line _extendGuide;
        private Shapes.Line _reflectGuide;

        private float _rotation = 0f;
        private Vector2 _unitVector;

        private Sprites.Ball _shotBall;
        private Sprites.Ball _currentBall;
        private Sprites.Ball _nextBall;

        private int _nextBallPosX = 0;
        private int _nextBallPosY = 0;
        private float _originNextBallScale = 0;

        private float _magicCircleScale = 2f;
        private float _magicCircleRotation = 0;

        public Sprites.Ball NextBall
        {
            get { return _nextBall; }
        }

        public Shooter() : base(50, 50)
        {
            ReflectPoint = new Vector2(0, 0);

            SetPosition(Constants.REFLECT_CENTER_X - _width / 2, Settings.WINDOW_HEIGHT - Settings.SHOOTER_BOTTOM - _height);

            _body = new Base.Sprite(AssetTypes.Texture.Wand, 400);
            _body.SetOrigin(_body.Instance.Width / 2, _body.Instance.Height / 2);
            _body.SetPosition(X + _width / 2, Y + _height / 2);
            _body.Rotation = Utils.Converter.DegressToRadians(-90 + 10);

            _unitVector = new Vector2((float)Math.Cos(_rotation), (float)Math.Sin(_rotation));

            //_nextBallPosX = (int)(X + Width + Settings.BALL_SIZE);
            //_nextBallPosY = (int)(Y + Height / 2);

            _nextBallPosX = Constants.REFLECT_RIGHT - Settings.BALL_SIZE / 2 - 10;
            _nextBallPosY = Settings.WINDOW_HEIGHT - Settings.PLAYING_UI_BOTTOM_HEIGHT - Settings.BALL_SIZE - 40;

            int code = Utils.Ball.RandomBallCode();
            _currentBall = new Sprites.Ball(Types.Ball.BallTypeFromCode(code), (int)(X + Width / 2), (int)(Y + Height / 2), false);

            int nextCode = Utils.Ball.RandomBallCode();
            _nextBall = new Sprites.Ball(Types.Ball.BallTypeFromCode(nextCode), _nextBallPosX, _nextBallPosY, false);

            _originNextBallScale = _nextBall.Scale;
        }

        private void updateUnitVector()
        {
            _unitVector.X = (float)Math.Cos(_rotation);
            _unitVector.Y = (float)Math.Sin(_rotation);
        }

        public void Update()
        {
            _magicCircleRotation += Utils.Converter.DegressToRadians(360 / 4 * Environments.Global.GameTime.ElapsedGameTime.TotalSeconds);

            MouseState mouseState = Environments.Global.CurrentMouseState;
            MouseState previousMouseState = Environments.Global.PreviousMouseState;

            int mouseX = mouseState.X;
            int mouseY = mouseState.Y;

            _rotation = (float)Math.Atan2(mouseY - (Y + Width / 2), mouseX - (X + Height / 2)) + Converter.DegressToRadians(180);

            float rotationDegrees = Converter.RadiansToDegrees(_rotation);

            if (mouseX < Constants.REFLECT_CENTER_X && (rotationDegrees < Settings.MIN_SHOOTER_ANGLE || rotationDegrees > 270))
            {
                _rotation = Converter.DegressToRadians(Settings.MIN_SHOOTER_ANGLE);
            }
            else if (mouseX > Constants.REFLECT_CENTER_X && rotationDegrees > 180 - Settings.MIN_SHOOTER_ANGLE)
            {
                _rotation = Converter.DegressToRadians(180 - Settings.MIN_SHOOTER_ANGLE);
            }

            Rotation = _rotation;

            updateUnitVector();

            if (_currentBall != null)
            {
                _currentBall.Rotation = Rotation - (float)Math.PI / 2;

                //_nextBall.Rotation += Converter.DegressToRadians(3);
            }
            else
            {
                if (_nextBall.Scale >= 0)
                {
                    _nextBall.Scale -= (float)Environments.Global.GameTime.ElapsedGameTime.TotalSeconds * 1;
                }
                else
                {
                    if (_shotBall == null)
                    {
                        _currentBall = _nextBall;
                        _currentBall.Scale = _originNextBallScale;
                        _currentBall.SetPosition((int)(X + Width / 2), (int)(Y + Height / 2));

                        int nextCode = Utils.Ball.RandomBallCode();
                        _nextBall = new Sprites.Ball(Types.Ball.BallTypeFromCode(nextCode), _nextBallPosX, _nextBallPosY, false);

                        Environments.GameData.CanShoot = true;
                    }
                }
            }

            if (_shotBall != null && _shotBall.isDestroyed) _shotBall = null;
            if (_shotBall != null) _shotBall.Update();

            if (Environments.Global.IsLeftClicked() && Environments.GameData.CanShoot && Environments.GameData.DataReady)
            {
                Environments.GameData.CanShoot = false;
                AssetTypes.Sound.BallShoot.Play(0.5f, 0, 0);

                _currentBall.Unit = -_unitVector;
                _currentBall.SetPosition((int)(X + Width / 2), (int)(Y + Height / 2));
                _shotBall = _currentBall;

                _currentBall = null;
            }
            else if (Environments.Global.IsRightClicked() && Environments.GameData.CanShoot && Environments.GameData.DataReady)
            {
                AssetTypes.Sound.BallSwap.Play();
                Sprites.Ball tmp = _currentBall;
                _currentBall = _nextBall;
                _currentBall.SetPosition((int)(X + Width / 2), (int)(Y + Height / 2));

                _nextBall = tmp;
                _nextBall.SetPosition(_nextBallPosX, _nextBallPosY);
                _nextBall.Rotation = 0f;
            }
            

            //bool isRightClicked = mouseState.RightButton != previousMouseState.RightButton && mouseState.RightButton == ButtonState.Pressed;
            //if (Environments.Global.WindowActive && isRightClicked)
            //{
            //    Environments.GameData.PrintTemplate();
            //}

            int reflectX = _rotation < Math.PI / 2 ? Constants.REFLECT_LEFT : _rotation > Math.PI / 2 ? Constants.REFLECT_RIGHT : Constants.REFLECT_CENTER_X;

            if (_rotation > Math.PI / 2) _rotation = (float)(Math.PI - _rotation);

            int reflectY = (int)((Settings.WINDOW_HEIGHT - Settings.SHOOTER_BOTTOM - Height / 2) - (Constants.PLAY_HALF_WIDTH * Math.Tan(_rotation)));

            int diffX = Constants.PLAY_HALF_WIDTH;
            int diffY = reflectY - (Settings.WINDOW_HEIGHT - Settings.SHOOTER_BOTTOM - Height / 2);

            int length = (int)Math.Ceiling(Math.Sqrt(diffX * diffX + diffY * diffY));

            GuideLength = length;
            SetReflectPoint(reflectX, reflectY);

            if (GuideLength > Settings.MAX_GUIDE_LENGTH || GuideLength <= 0) GuideLength = Settings.MAX_GUIDE_LENGTH;

            if (_extendGuide != null)
            {
                _extendGuide.Destroy();
                _extendGuide = null;
            }

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
                int pointX = (int)(reflectX + unitX * int.MaxValue);
                int pointY = (int)(reflectY + unitY * int.MaxValue);

                _reflectGuide = new Shapes.Line(reflectX, reflectY, pointX, pointY, Settings.REFLECT_GUIDE_WIDTH, 100);
                _reflectGuide.SetColor(Settings.REFLEFCT_GUIDE_COLOR);
                _reflectGuide.SetOrigin(2, 0);
                _reflectGuide.SetPosition(ReflectPoint.X, ReflectPoint.Y);
                _reflectGuide.Rotation += Converter.DegressToRadians(90);

                int heightFromShooter = (int)(reflectY - (Y + Height / 2.0f));
                int extendLength = (int)Math.Sqrt(heightFromShooter * heightFromShooter + Constants.PLAY_HALF_WIDTH * Constants.PLAY_HALF_WIDTH) - 200;

                _extendGuide = new Shapes.Line((int)(X + Width / 2), (int)(Y + Height / 2), reflectX, reflectY, Settings.REFLECT_GUIDE_WIDTH, extendLength);
                _extendGuide.SetColor(Settings.REFLEFCT_GUIDE_COLOR);
                _extendGuide.SetOrigin(2, 0);
                _extendGuide.SetPosition(ReflectPoint.X, ReflectPoint.Y);
                _extendGuide.Rotation += Converter.DegressToRadians(90);
            }
        }

        public void Draw()
        {
            if (_reflectGuide != null)
            {
                Environments.Global.SpriteBatch.Draw(_reflectGuide.Instance, _reflectGuide.Position, null, _reflectGuide.Color, _reflectGuide.Rotation, _reflectGuide.Origin, 1f, SpriteEffects.None, 0f);
            }

            DrawSprite(_body);

            Environments.Global.SpriteBatch.Draw(
                AssetTypes.Texture.ShooterArrow,
                new Vector2(X + _width / 2, Y + _height / 2) - new Vector2((float)Math.Cos(Rotation) * 115, (float)Math.Sin(Rotation) * 115),
                null,
                Settings.GUIDE_COLOR,
                Rotation - (float)Math.PI / 2,
                new Vector2(AssetTypes.Texture.ShooterArrow.Width / 2, AssetTypes.Texture.ShooterArrow.Height),
                0.5f,
                SpriteEffects.None,
                0f
            );

            if (_extendGuide != null)
            {
                Environments.Global.SpriteBatch.Draw(_extendGuide.Instance, _extendGuide.Position, null, _extendGuide.Color, _extendGuide.Rotation, _extendGuide.Origin, 1f, SpriteEffects.None, 0f);
            }

            if (_shotBall != null && !Environments.GameData.Failed && !Environments.GameData.Won)
            {
                Environments.Global.SpriteBatch.Draw(_shotBall.Instance, _shotBall.Position, null, _shotBall.DrawColor, 0f, _shotBall.Origin, _shotBall.Scale, SpriteEffects.None, 0f);
            }

            if (_currentBall != null)
            {
                Environments.Global.SpriteBatch.Draw(_currentBall.Instance, _currentBall.Position, null, _currentBall.DrawColor, _currentBall.Rotation, _currentBall.Origin, _currentBall.Scale, SpriteEffects.None, 0f);
            }

            Environments.Global.SpriteBatch.Draw(
                AssetTypes.Texture.MagicCircle,
                new Rectangle(_nextBallPosX, _nextBallPosY, (int)(Settings.BALL_SIZE * _magicCircleScale), (int)(Settings.BALL_SIZE * _magicCircleScale)),
                null,
                Color.Black,
                _magicCircleRotation,
                new Vector2(AssetTypes.Texture.MagicCircle.Width / 2, AssetTypes.Texture.MagicCircle.Height / 2),
                SpriteEffects.None,
                0f
            );

            Environments.Global.SpriteBatch.Draw(_nextBall.Instance, _nextBall.Position, null, _nextBall.DrawColor, _nextBall.Rotation, _nextBall.Origin, _nextBall.Scale, SpriteEffects.None, 0f);
        }

        public void SetReflectPoint(int x, int y)
        {
            ReflectPoint.X = x;
            ReflectPoint.Y = y;
        }
    }
}
