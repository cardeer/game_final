using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace game_final
{
    static class Assets
    {
        private static ContentManager s_content;
        private static SpriteBatch s_spriteBatch;
        private static GraphicsDevice s_graphics;

        public static Sprites.Shooter Shooter;
        public static Scenes.Playing PlayingScene;


        public static void Initialize(ContentManager content, SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            s_content = content;
            s_spriteBatch = spriteBatch;
            s_graphics = graphics;

            loadTextures();
            loadSounds();
            createSprites();
        }

        private static void loadTextures() {
            AssetTypes.Texture.BlueBall = s_content.Load<Texture2D>("Balls/blue");
            AssetTypes.Texture.BrownBall = s_content.Load<Texture2D>("Balls/brown");
            AssetTypes.Texture.GreenBall = s_content.Load<Texture2D>("Balls/green");
            AssetTypes.Texture.GreyBall = s_content.Load<Texture2D>("Balls/grey");
            AssetTypes.Texture.LightBlueBall = s_content.Load<Texture2D>("Balls/light_blue");
            AssetTypes.Texture.PurpleBall = s_content.Load<Texture2D>("Balls/purple");
            AssetTypes.Texture.RedBall = s_content.Load<Texture2D>("Balls/red");
            AssetTypes.Texture.YellowBall = s_content.Load<Texture2D>("Balls/yellow");
        }
        private static void loadSounds()
        {
            AssetTypes.Sound.MusicSound = s_content.Load<Song>("Sounds/gurenge");
        }

        private static void createSprites()
        {
            Shooter = new Sprites.Shooter(s_spriteBatch, s_graphics);
            PlayingScene = new Scenes.Playing(s_spriteBatch, s_graphics);
        }
    }
}
