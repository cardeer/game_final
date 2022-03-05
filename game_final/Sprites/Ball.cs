using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace game_final.Sprites
{
    class Ball : Base.Sprite
    {
        public Environments.Ball.BallType Type;

        private Vector2 _unit;
        private bool _snapped = false;

        public Ball(Environments.Ball.BallType type, int x, int y, Vector2 unit) : base(Utils.Ball.GetBallTexture(type), Settings.BALL_SIZE)
        {
            Type = type;

            _unit = unit;
            SetOrigin(Instance.Width / 2, Instance.Height / 2);
            SetPosition(x + unit.X * Width, y + unit.Y * Height);
        }

        public Ball(Environments.Ball.BallType type, int x, int y) : base(Utils.Ball.GetBallTexture(type), Settings.BALL_SIZE)
        {
            Type = type;
            _snapped = true;

            SetOrigin(Instance.Width / 2, Instance.Height / 2);
            SetPosition(x, y);
        }

        public void Update()
        {
            if (_snapped) return;

            SetPosition(X + _unit.X * Settings.BALL_SPEED, Y + _unit.Y * Settings.BALL_SPEED);

            if (X <= Constants.REFLECT_LEFT || X >= Constants.REFLECT_RIGHT)
            {
                _unit.X = -_unit.X;
            }

            if (Y - Height / 2 <= 0)
            {
                _snapped = true;
                Utils.Shooting.GetSnappedPosition(this);
                Destroy();
            }
        }
    }
}
