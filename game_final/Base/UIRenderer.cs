using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_final.Base
{
    class UIRenderer
    {
        protected SpriteBatch _spriteBatch;
        protected UIRenderer(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
        }
        protected void DrawSprite(Base.Sprite sprite)
        {
            _spriteBatch.Draw(
                sprite.Instance,
                sprite.Position,
                null,
                sprite.DrawColor,
                sprite.Rotation,
                sprite.Origin,
                sprite.Scale,
                SpriteEffects.None,
                0f
            );
        }
    }
}
