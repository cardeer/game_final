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

        private static void loadTextures() {
            AssetTypes.Texture.Logo = s_content.Load<Texture2D>("logo");

            AssetTypes.Texture.Wand = s_content.Load<Texture2D>("Shooter/wand");

            AssetTypes.Texture.BlueBall = s_content.Load<Texture2D>("Balls/blue");
            AssetTypes.Texture.BrownBall = s_content.Load<Texture2D>("Balls/brown");
            AssetTypes.Texture.GreenBall = s_content.Load<Texture2D>("Balls/green");
            AssetTypes.Texture.GreyBall = s_content.Load<Texture2D>("Balls/grey");
            AssetTypes.Texture.LightBlueBall = s_content.Load<Texture2D>("Balls/light_blue");
            AssetTypes.Texture.PurpleBall = s_content.Load<Texture2D>("Balls/purple");
            AssetTypes.Texture.RedBall = s_content.Load<Texture2D>("Balls/red");
            AssetTypes.Texture.YellowBall = s_content.Load<Texture2D>("Balls/yellow");

            AssetTypes.Texture.LeftWall = s_content.Load<Texture2D>("Scenes/Playing/left_wall");
            AssetTypes.Texture.WallBorder = s_content.Load<Texture2D>("Scenes/Playing/wall_border");
            AssetTypes.Texture.TopWallBorder = s_content.Load<Texture2D>("Scenes/Playing/top_wall_border");
            AssetTypes.Texture.BottomWallBorder = s_content.Load<Texture2D>("Scenes/Playing/bottom_wall_border");
            AssetTypes.Texture.Button = s_content.Load<Texture2D>("Buttons/button");
        }

        private static void loadFont()
        {
            AssetTypes.Font.spritefont = s_content.Load<SpriteFont>("Fonts/Font");
        }

        private static void loadSounds()
        {
            AssetTypes.Sound.MusicSound = s_content.Load<Song>("Sounds/gurenge");
        }
    }
}
