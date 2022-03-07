using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Diagnostics;

namespace game_final.Scenes
{
    class MainMenu : Base.SceneRenderer
    {
        private Texture2D _logo;
        private Vector2 _logoPosition;

        private Texture2D _background;
        private Vector2 _bgPosition;

        private Sprites.Buttons _playButton;
        private Sprites.Buttons _levelButton;
        private Sprites.Buttons _challengeButton;
        private Sprites.Buttons _quitButton;

        public override void LoadContent()
        {
            //Texture
            AssetTypes.Texture.MainMenuBG = Environments.Global.Content.Load<Texture2D>("Scenes/MainMenu/mainMenuBG");

            //Button
            _playButton = new Sprites.Buttons(AssetTypes.Texture.Button, AssetTypes.Font.spritefont)
            {
                Position = new Vector2(400, 350),
                Text = "Play",
            };

            _levelButton = new Sprites.Buttons(AssetTypes.Texture.Button, AssetTypes.Font.spritefont)
            {
                Position = new Vector2(400, 450),
                Text = "Level",
            };

            _challengeButton = new Sprites.Buttons(AssetTypes.Texture.Button, AssetTypes.Font.spritefont)
            {
                Position = new Vector2(400, 550),
                Text = "Challenge",
            };

            _quitButton = new Sprites.Buttons(AssetTypes.Texture.Button, AssetTypes.Font.spritefont)
            {
                Position = new Vector2(400, 650),
                Text = "Quit"
            };

            _playButton.Click += PlayButton_Click;
            _levelButton.Click += LevelButton_Click;
            _challengeButton.Click += ChallengeButton_Click;
            _quitButton.Click += QuitButton_Click;

        }

        public override void Setup()
        {
            _logo = AssetTypes.Texture.Logo;
            _logoPosition = new Vector2(Settings.WINDOW_WIDTH / 2 - _logo.Width / 4, 100);

            _background = AssetTypes.Texture.MainMenuBG;
            _bgPosition = new Vector2(0, 0);
        }

        public override void Update()
        {
            _playButton.Update();
            _levelButton.Update();
            _challengeButton.Update();
            _quitButton.Update();
        }

        public override void Draw()
        {
            Environments.Global.SpriteBatch.Draw(_background, _bgPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            Environments.Global.SpriteBatch.Draw(_logo, _logoPosition, null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);

            _playButton.Draw();
            _levelButton.Draw();
            _challengeButton.Draw();
            _quitButton.Draw();
        }

        private void PlayButton_Click(object sender, System.EventArgs e)
        {
            Environments.Scene.SetScene(Types.SceneType.IN_GAME);
        }

        private void LevelButton_Click(object sender, System.EventArgs e)
        {
            Environments.Scene.SetScene(Types.SceneType.LEVEL_MENU);
        }

        private void ChallengeButton_Click(object sender, System.EventArgs e)
        {
            //Environments.Scene.SetScene(Types.SceneType.IN_GAME);
        }

        private void QuitButton_Click(object sender, System.EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
