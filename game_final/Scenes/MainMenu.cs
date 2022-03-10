using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

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

        public static bool _isPlaying = false;

        private int _buttonWidth = 400;
        private int _buttonHeight = 70;

        public override void LoadContent()
        {
            //Texture
            AssetTypes.Texture.MainMenuBG = Environments.Global.Content.Load<Texture2D>("Scenes/MainMenu/mainMenuBG");

            //BGM
            AssetTypes.Sound.MusicSound = Environments.Global.Content.Load<Song>("Sounds/MainMenuBGM");
        }

        public override void Setup()
        {   
            //GameTitle
            _logo = AssetTypes.Texture.Logo;
            _logoPosition = new Vector2(Settings.WINDOW_WIDTH / 2, 50);
            
            //Background
            _background = AssetTypes.Texture.MainMenuBG;
            _bgPosition = new Vector2(0, 0);

            //BGM
            if (MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Volume = Settings.MAIN_MENU_BGM_VOLUME;
                MediaPlayer.Play(AssetTypes.Sound.MusicSound);
                MediaPlayer.IsRepeating = true;
            }

            //Buttons
            _playButton = new Sprites.Buttons(AssetTypes.Texture.Button, AssetTypes.Font.SpriteFont, "PLAY", _buttonWidth, _buttonHeight);
            _playButton.SetPosition(Settings.WINDOW_WIDTH / 2, 500);

            _levelButton = new Sprites.Buttons(AssetTypes.Texture.Button, AssetTypes.Font.SpriteFont, "LEVEL", _buttonWidth, _buttonHeight);
            _levelButton.SetPosition(Settings.WINDOW_WIDTH / 2, 600);

            _challengeButton = new Sprites.Buttons(AssetTypes.Texture.Button, AssetTypes.Font.SpriteFont, "CHALLENGE", _buttonWidth, _buttonHeight);
            _challengeButton.SetPosition(Settings.WINDOW_WIDTH / 2, 700);
            _challengeButton.MultipleClicks = true;
            _challengeButton.TextColor = Environments.GameData.ChallengeMode ? Color.OrangeRed : Color.White;

            _quitButton = new Sprites.Buttons(AssetTypes.Texture.Button, AssetTypes.Font.SpriteFont, "QUIT", _buttonWidth, _buttonHeight);
            _quitButton.SetPosition(Settings.WINDOW_WIDTH / 2, 800);

            _playButton.Click += PlayButton_Click;
            _levelButton.Click += LevelButton_Click;
            _challengeButton.Click += ChallengeButton_Click;
            _quitButton.Click += QuitButton_Click;

            base.Setup();
        }

        public override void Update()
        {
            _playButton.Update();
            _levelButton.Update();
            _challengeButton.Update();
            _quitButton.Update();

            base.Update();
        }

        public override void Draw()
        {
            Environments.Global.SpriteBatch.Draw(_background, _bgPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            Environments.Global.SpriteBatch.Draw(_logo, _logoPosition, null, Color.White, 0f, new Vector2(_logo.Width / 2, 0), 0.8f, SpriteEffects.None, 0f);

            _playButton.Draw();
            _levelButton.Draw();
            _challengeButton.Draw();
            _quitButton.Draw();

            base.Draw();
        }

        private void PlayButton_Click(object sender, System.EventArgs e)
        {
            Utils.Sound.PlayStartSound();
            Environments.GameData.Level = 1;
            Environments.Scene.SetScene(Types.SceneType.IN_GAME, true);
            MediaPlayer.Stop();
        }

        private void LevelButton_Click(object sender, System.EventArgs e)
        {
            Environments.Scene.SetScene(Types.SceneType.LEVEL_MENU);
        }

        private void ChallengeButton_Click(object sender, System.EventArgs e)
        {
            if (!Environments.GameData.ChallengeMode)
            {
                Environments.GameData.ChallengeMode = true;
                _challengeButton.TextColor = Color.OrangeRed;
                //Todo change mode
            }
            else
            {
                Environments.GameData.ChallengeMode = false;
                _challengeButton.TextColor = Color.White;
                //Todo change mode
            }
        }

        private void QuitButton_Click(object sender, System.EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
