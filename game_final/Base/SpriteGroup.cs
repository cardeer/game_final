using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_final.Base
{
    class SpriteGroup : Object
    {
        public SpriteGroup(int width, int height)
        {
            _width = width;
            _height = height;

            Position = new Vector2(0, 0);
        }

        protected void DrawSprite(Sprite sprite)
        {
            Environments.Global.SpriteBatch.Draw(
                sprite.Instance,
                new Vector2(Position.X + sprite.X, Position.Y + sprite.Y),
                null,
                sprite.Color,
                Rotation + sprite.Rotation,
                sprite.Origin,
                1f,
                SpriteEffects.None,
                0f
            );
        }
    }
}
