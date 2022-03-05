using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_final.Base
{
    class UIRenderer
    {
        private SpriteBatch _spriteBatch;
        public UIRenderer(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
        }
        protected void DrawSprite(Base.Sprite sprite)
        {
            _spriteBatch.Draw(sprite.Instance, sprite.Position, null, sprite.Color, 0f, sprite.Origin, 1f, SpriteEffects.None, 0f);
        }
    }
}
