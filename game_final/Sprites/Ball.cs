using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace game_final.Sprites
{
    class Ball : Base.Sprite
    {
        public Environments.Ball.BallType Type;
        public Vector2 SnapPoint;
        public Vector2 Unit;

        private bool _snapped = false;

        public Ball(Environments.Ball.BallType type, int x, int y, Vector2 unit) : base(Utils.Ball.GetBallTexture(type), Settings.BALL_SIZE)
        {
            Type = type;

            Unit = unit;
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
            if (_snapped)
            {
                Destroy();
                return;
            }

            SetPosition(X + Unit.X * Settings.BALL_SPEED, Y + Unit.Y * Settings.BALL_SPEED);

            if (X <= Constants.REFLECT_LEFT || X >= Constants.REFLECT_RIGHT)
            {
                Unit.X = -Unit.X;
            }

            SnapPoint = Utils.Ball.GetSnappedPosition(this);
            int snapX = (int)SnapPoint.X, snapY = (int)SnapPoint.Y;

            int[,] template = Environments.GameData.BallsTemplate;

            if (snapY <= 0)
            {
                snapY = 0;
                _snapped = true;
                Environments.GameData.SetBallTemplate(snapX, snapY, Utils.Ball.BallCode(Type));
            }
            else if (Utils.Ball.ShouldSnap(this))
            {
                _snapped = true;
                Environments.GameData.SetBallTemplate(snapX, snapY, Utils.Ball.BallCode(Type));
            }
        }
    }
}
