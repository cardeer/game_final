using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace game_final.Sprites
{
    class Ball : Base.Sprite
    {
        private Vector2 _unit;
        public Ball(Texture2D texture, int size, int x, int y, Vector2 unit) : base(texture, size)
        {
            _unit = unit;
            SetOrigin(Instance.Width / 2, Instance.Height / 2);
            SetPosition(x + unit.X * Width, y + unit.Y * Height);
        }

        public void Update()
        {
            SetPosition(X + _unit.X * Settings.BALL_SPEED, Y + _unit.Y * Settings.BALL_SPEED);

            if (X <= Constants.REFLECT_LEFT || X >= Constants.REFLECT_RIGHT)
            {
                _unit.X = -_unit.X;
            }
        }
    }
}
