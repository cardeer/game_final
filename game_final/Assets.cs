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
            AssetTypes.Texture.IconMute = s_content.Load<Texture2D>("Buttons/icon_audio");
            AssetTypes.Texture.IconUnmute = s_content.Load<Texture2D>("Buttons/icon_audio_mute");
            AssetTypes.Texture.IconReplay = s_content.Load<Texture2D>("Buttons/icon_replay");
            AssetTypes.Texture.IconHome = s_content.Load<Texture2D>("Buttons/icon_home");
        }

        private static void loadFont()
        {
            AssetTypes.Font.SpriteFont = s_content.Load<SpriteFont>("Fonts/Font");
            AssetTypes.Font.PlayingButton = s_content.Load<SpriteFont>("Fonts/PlayingButton");
            AssetTypes.Font.UIFont = s_content.Load<SpriteFont>("Fonts/UIFont");
        }

        private static void loadSounds()
        {
            // load sound effects
            AssetTypes.Sound.ButtonHover = s_content.Load<SoundEffect>("Sounds/SoundEffects/button_hover");
            AssetTypes.Sound.ButtonClick = s_content.Load<SoundEffect>("Sounds/SoundEffects/button_click");
            AssetTypes.Sound.BallShoot = s_content.Load<SoundEffect>("Sounds/SoundEffects/ball_shoot");
            AssetTypes.Sound.BallSnap = s_content.Load<SoundEffect>("Sounds/SoundEffects/ball_snap");
            AssetTypes.Sound.BallPop = s_content.Load<SoundEffect>("Sounds/SoundEffects/ball_pop");
        }
    }
}
