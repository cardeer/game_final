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
                new Vector2(sprite.X, sprite.Y),
                null,
                sprite.DrawColor,
                Rotation + sprite.Rotation,
                sprite.Origin,
                sprite.Scale,
                SpriteEffects.None,
                0f
            );
        }
    }
}
