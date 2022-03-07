using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace game_final
{
    static class Assets
    {
        private static ContentManager s_content;

        public static void Initialize(ContentManager content)
        {
            s_content = content;

            loadTextures();
            loadFont();
            loadSounds();
        }

        private static void loadTextures()
        {
            AssetTypes.Texture.Button = s_content.Load<Texture2D>("Buttons/button");
        }

        private static void loadFont()
        {
            AssetTypes.Font.SpriteFont = s_content.Load<SpriteFont>("Fonts/Font");
        }

        private static void loadSounds()
        {
            AssetTypes.Sound.MusicSound = s_content.Load<Song>("Sounds/gurenge");
        }
    }
}
