using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_final.Shapes
{
    class Line : Base.Sprite
    {
        public Line(GraphicsDevice graphics, int x1, int y1, int x2, int y2, int width, int height) : base(graphics, width, height)
        {
            SetPosition(x1, y1);
            Rotation = (float)Math.Atan2(y2 - y1, x2 - x1);
        }
    }
}
