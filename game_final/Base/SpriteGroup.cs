using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_final.Base
{
    class SpriteGroup : Object
    {
        
        protected SpriteBatch _spriteBatch;

        public SpriteGroup(SpriteBatch spriteBatch, int width, int height)
        {
            _spriteBatch = spriteBatch;

            _width = width;
            _height = height;

            Position = new Vector2(0, 0);
        }

        protected void DrawSprite(Sprite sprite)
        {
            _spriteBatch.Draw(
                sprite.Instance,
                new Vector2(Position.X + sprite.X, Position.Y + sprite.Y),
                null,
                sprite.Color,
                Rotation + sprite.Rotation,
                sprite.Origin,
                1f,
                SpriteEffects.None,
                0f
            ); ;
        }
    }
}
