using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_final.Scenes
{
    class MainMenu : Base.SceneRenderer
    {
        private Sprites.Buttons testButton;
        private Sprites.Buttons quitButton;

        public MainMenu()
        {
            testButton = new Sprites.Buttons(AssetTypes.Texture.Button, AssetTypes.Font.spritefont)
            {
                Position = new Vector2(150, 150),
                Text = "Test Button",
            };

            testButton.Click += TestButton_Click;

            quitButton = new Sprites.Buttons(AssetTypes.Texture.Button, AssetTypes.Font.spritefont)
            {
                Position = new Vector2(150, 400),
                Text = "Quit Button"
            };

            quitButton.Click += QuitButton_Click;

        }

        private void TestButton_Click(object sender, System.EventArgs e)
        {
            Environments.Global.SetScene(Types.SceneType.IN_GAME);
        }

        private void QuitButton_Click(object sender, System.EventArgs e)
        {
            System.Environment.Exit(0);
        }

        public override void Update()
        {

        }

        public override void Draw()
        {
            testButton.Draw();
            quitButton.Draw();
        }
    }
}
