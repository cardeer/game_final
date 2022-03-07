﻿using System;
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

        private Sprites.MenuButtons _playButton;
        private Sprites.MenuButtons _levelButton;
        private Sprites.MenuButtons _challengeButton;
        private Sprites.MenuButtons _quitButton;

        private int _buttonWidth = 400;
        private int _buttonHeight = 70;

        public override void LoadContent()
        {
            //Texture
            AssetTypes.Texture.MainMenuBG = Environments.Global.Content.Load<Texture2D>("Scenes/MainMenu/mainMenuBG");

            //Button
            _playButton = new Sprites.MenuButtons(AssetTypes.Texture.Button, AssetTypes.Font.SpriteFont, "PLAY", _buttonWidth, _buttonHeight);
            _playButton.SetPosition(Settings.WINDOW_WIDTH / 2, 500);

            _levelButton = new Sprites.MenuButtons(AssetTypes.Texture.Button, AssetTypes.Font.SpriteFont, "LEVEL", _buttonWidth, _buttonHeight);
            _levelButton.SetPosition(Settings.WINDOW_WIDTH / 2, 600);

            _challengeButton = new Sprites.MenuButtons(AssetTypes.Texture.Button, AssetTypes.Font.SpriteFont, "CHALLENGE", _buttonWidth, _buttonHeight);
            _challengeButton.SetPosition(Settings.WINDOW_WIDTH / 2, 700);

            _quitButton = new Sprites.MenuButtons(AssetTypes.Texture.Button, AssetTypes.Font.SpriteFont, "QUIT", _buttonWidth, _buttonHeight);
            _quitButton.SetPosition(Settings.WINDOW_WIDTH / 2, 800);

            _playButton.Click += PlayButton_Click;
            _levelButton.Click += LevelButton_Click;
            _challengeButton.Click += ChallengeButton_Click;
            _quitButton.Click += QuitButton_Click;

        }

        public override void Setup()
        {
            _logo = AssetTypes.Texture.Logo;
            _logoPosition = new Vector2(Settings.WINDOW_WIDTH / 2, 75);

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
            Environments.Global.SpriteBatch.Draw(_logo, _logoPosition, null, Color.White, 0f, new Vector2(_logo.Width / 2, 0), 0.8f, SpriteEffects.None, 0f);

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
