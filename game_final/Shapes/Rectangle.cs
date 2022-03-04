using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_final.Shapes
{
    class Rectangle : Base.Sprite
    {
        public Rectangle(GraphicsDevice graphics, int width, int height) {
            base.Initialize(graphics, width, height);
        }
    }
}
