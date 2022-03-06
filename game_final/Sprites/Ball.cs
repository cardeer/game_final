using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace game_final.Sprites
{
    class Ball : Base.Sprite
    {
        public Types.BallType Type;
        public Vector2 SnapPoint;
        public Vector2 Unit;

        private bool _snapped = false;

        public Ball(Types.BallType type, int x, int y, Vector2 unit) : base(Types.Ball.GetBallTexture(type), Settings.BALL_SIZE)
        {
            Type = type;

            Unit = unit;
            SetOrigin(Instance.Width / 2, Instance.Height / 2);
            SetPosition(x + unit.X * Width, y + unit.Y * Height);
        }

        public Ball(Types.BallType type, int x, int y) : base(Types.Ball.GetBallTexture(type), Settings.BALL_SIZE)
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
            SnapPoint = Utils.Ball.GetSnappedPosition(this);

            if (X <= Constants.REFLECT_LEFT || X >= Constants.REFLECT_RIGHT)
            {
                Unit.X = -Unit.X;
            }

            Types.Snap snap = Utils.Ball.ShouldSnap(this);

            if (snap.ShouldSnap)
            {
                _snapped = true;
                Environments.GameData.SetBallTemplate(snap.SnapRow, snap.SnapCol, Types.Ball.BallCode(Type));
            }
        }
    }
}
