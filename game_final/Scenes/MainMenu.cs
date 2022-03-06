using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_final.Scenes
{
    class MainMenu : Base.SceneRenderer
    {
        public MainMenu()
        {
            var TestButton = new Sprites.Buttons(AssetTypes.Texture.Button, AssetTypes.Font.spritefont)
            {
                Position = new Vector2(150, 150),
                Text = "Test Button"
            };
        }

        public override void Update()
        {

        }

        public override void Draw()
        {

        }
    }
}
