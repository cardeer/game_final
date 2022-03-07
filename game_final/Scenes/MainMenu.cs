using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_final.Scenes
{
    class MainMenu : Base.SceneRenderer
    {
        private Sprites.Buttons playButton;
        private Sprites.Buttons quitButton;


        private void TestButton_Click(object sender, System.EventArgs e)
        {
            Environments.Scene.SetScene(Types.SceneType.IN_GAME);
        }

        private void QuitButton_Click(object sender, System.EventArgs e)
        {
            System.Environment.Exit(0);
        }

        public override void LoadContent()
        {
            playButton = new Sprites.Buttons(AssetTypes.Texture.Button, AssetTypes.Font.spritefont)
            {
                Position = new Vector2(150, 150),
                Text = "Play",
            };

            playButton.Click += TestButton_Click;

            quitButton = new Sprites.Buttons(AssetTypes.Texture.Button, AssetTypes.Font.spritefont)
            {
                Position = new Vector2(150, 400),
                Text = "Quit Button"
            };

            quitButton.Click += QuitButton_Click;
        }

        public override void Setup()
        {

        }

        public override void Update()
        {
            playButton.Update();
            quitButton.Update();
            
        }

        public override void Draw()
        {
            playButton.Draw();
            quitButton.Draw();
        }
    }
}
