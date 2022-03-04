using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace game_final
{
    /// <summary>
    /// Store all asset references
    /// </summary>
    static class Assets
    {
        private static ContentManager s_content;
        private static SpriteBatch s_spriteBatch;
        private static GraphicsDevice s_graphics;

        public static Sprites.Shooter Shooter;

        /// <summary>
        /// Load all assets
        /// </summary>
        /// <param name="content"></param>
        /// <param name="graphics"></param>
        public static void Initialize(ContentManager content, SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            s_content = content;
            s_spriteBatch = spriteBatch;
            s_graphics = graphics;

            loadAll();
        }

        private static void loadAll()
        {
            Shooter = new Sprites.Shooter(s_spriteBatch, s_graphics);
        }
    }
}
