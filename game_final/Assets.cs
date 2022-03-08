using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

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

            // load sound effects
            AssetTypes.Sound.ButtonHover = s_content.Load<SoundEffect>("Sounds/SoundEffects/button_hover");
            AssetTypes.Sound.ButtonClick = s_content.Load<SoundEffect>("Sounds/SoundEffects/button_click");
            AssetTypes.Sound.BallPop = s_content.Load<SoundEffect>("Sounds/SoundEffects/ball_pop");
        }
    }
}
