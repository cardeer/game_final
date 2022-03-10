using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace game_final.Scenes
{
	class LevelMenu : Base.SceneRenderer
    {
        private Texture2D _logo;
        private Vector2 _logoPosition;

        private Texture2D _background;
        private Vector2 _bgPosition;

        private Sprites.Buttons _easyButton;
        private Sprites.Buttons _normalButton;
        private Sprites.Buttons _hardButton;
        private Sprites.Buttons _backButton;

        private int _buttonWidth = 400;
        private int _buttonHeight = 70;

        public override void LoadContent()
        {
            //Texture
            AssetTypes.Texture.MainMenuBG = Environments.Global.Content.Load<Texture2D>("Scenes/MainMenu/mainMenuBG");
        }

        public override void Setup()
        {
            _logo = AssetTypes.Texture.Logo;
            _logoPosition = new Vector2(Settings.WINDOW_WIDTH / 2, 50);

            _background = AssetTypes.Texture.MainMenuBG;
            _bgPosition = new Vector2(0, 0);

            //Buttons
            _easyButton = new Sprites.Buttons(AssetTypes.Texture.Button, AssetTypes.Font.SpriteFont, "EASY", _buttonWidth, _buttonHeight);
            _easyButton.SetPosition(Settings.WINDOW_WIDTH / 2, 500);
            _easyButton.TextColor = Color.LightGreen;

            _normalButton = new Sprites.Buttons(AssetTypes.Texture.Button, AssetTypes.Font.SpriteFont, "NORMAL", _buttonWidth, _buttonHeight);
            _normalButton.SetPosition(Settings.WINDOW_WIDTH / 2, 600);
            _normalButton.TextColor = Color.Yellow;

            _hardButton = new Sprites.Buttons(AssetTypes.Texture.Button, AssetTypes.Font.SpriteFont, "HARD", _buttonWidth, _buttonHeight);
            _hardButton.SetPosition(Settings.WINDOW_WIDTH / 2, 700);
            _hardButton.TextColor = Color.OrangeRed;

            _backButton = new Sprites.Buttons(AssetTypes.Texture.Button, AssetTypes.Font.SpriteFont, "BACK", _buttonWidth, _buttonHeight);
            _backButton.SetPosition(Settings.WINDOW_WIDTH / 2, 800);

            _easyButton.Click += EasyButton_Click;
            _normalButton.Click += NormalButton_Click;
            _hardButton.Click += HardButton_Click;
            _backButton.Click += BackButton_Click;

            base.Setup();
        }

        public override void Update()
        {
            _easyButton.Update();
            _normalButton.Update();
            _hardButton.Update();
            _backButton.Update();

            base.Update();
        }

        public override void Draw()
        {
            Environments.Global.SpriteBatch.Draw(_background, _bgPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            Environments.Global.SpriteBatch.Draw(_logo, _logoPosition, null, Color.White, 0f, new Vector2(_logo.Width / 2, 0), 0.8f, SpriteEffects.None, 0f);

            _easyButton.Draw();
            _normalButton.Draw();
            _hardButton.Draw();
            _backButton.Draw();

            base.Draw();
        }

        private void EasyButton_Click(object sender, System.EventArgs e)
        {
            Environments.GameData.Level = 1;
            MainMenu._isPlaying = true;
            Environments.Scene.SetScene(Types.SceneType.IN_GAME, true);
        }

        private void NormalButton_Click(object sender, System.EventArgs e)
        {
            Environments.GameData.Level = 2;
            MainMenu._isPlaying = true;
            Environments.Scene.SetScene(Types.SceneType.IN_GAME, true);
        }

        private void HardButton_Click(object sender, System.EventArgs e)
        {
            Environments.GameData.Level = 3;
            MainMenu._isPlaying = true;
            Environments.Scene.SetScene(Types.SceneType.IN_GAME, true);
        }

        private void BackButton_Click(object sender, System.EventArgs e)
        {
            Environments.Scene.SetScene(Types.SceneType.MAIN_MENU);
        }

    }
}
