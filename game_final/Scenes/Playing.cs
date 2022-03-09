using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using System.Diagnostics;
using System;
using game_final.Types;

namespace game_final.Scenes
{
    class Playing : Base.SceneRenderer
    {
        private Shapes.Line _bottomLine;

        private Sprites.Shooter _shooter;

        private Song _playBGM;

        private Base.Sprite _leftWall;
        private Base.Sprite _wallpaper;
        private Base.Sprite _scoreboard;
        private Base.Sprite _leftWallBorder;
        private Base.Sprite _rightWallBorder;
        private Base.Sprite _topWallBorder;
        private Base.Sprite _bottomWallBorder;

        private Base.Sprite _infoBoard;
        private Base.Sprite _characterWindow;
        private Base.Sprite _megumin;

        private Sprites.Buttons _homeButton;

        private int _shake = 0;
        private double _shakeWait = 0;

        public Playing() : base(true) { }

        public override void LoadContent()
        {
            //ObjectTexture
            AssetTypes.Texture.Wand = Environments.Global.Content.Load<Texture2D>("Shooter/wand");
            AssetTypes.Texture.ShooterArrow = Environments.Global.Content.Load<Texture2D>("Shooter/arrow");

            //BallTexture
            AssetTypes.Texture.BlueBall = Environments.Global.Content.Load<Texture2D>("Balls/blue_slime");
            AssetTypes.Texture.LightBlueBall = Environments.Global.Content.Load<Texture2D>("Balls/lightBlue_slime");
            AssetTypes.Texture.GreenBall = Environments.Global.Content.Load<Texture2D>("Balls/green_slime");
            AssetTypes.Texture.GreyBall = Environments.Global.Content.Load<Texture2D>("Balls/grey_slime");
            AssetTypes.Texture.PurpleBall = Environments.Global.Content.Load<Texture2D>("Balls/purple_slime");
            AssetTypes.Texture.RedBall = Environments.Global.Content.Load<Texture2D>("Balls/red_slime");

            AssetTypes.Texture.LeftWall = Environments.Global.Content.Load<Texture2D>("Scenes/Playing/left_wall");
            AssetTypes.Texture.WallPaper = Environments.Global.Content.Load<Texture2D>("Scenes/Playing/wallPaper");
            AssetTypes.Texture.Scoreboard = Environments.Global.Content.Load<Texture2D>("Scenes/Playing/scoreboard");
            AssetTypes.Texture.WallBorder = Environments.Global.Content.Load<Texture2D>("Scenes/Playing/wall_border");
            AssetTypes.Texture.TopWallBorder = Environments.Global.Content.Load<Texture2D>("Scenes/Playing/top_wall_border");
            AssetTypes.Texture.BottomWallBorder = Environments.Global.Content.Load<Texture2D>("Scenes/Playing/bottom_wall_border");
            AssetTypes.Texture.MagicCircle = Environments.Global.Content.Load<Texture2D>("Effects/magic_circle");
            AssetTypes.Texture.PlayBackground = Environments.Global.Content.Load<Texture2D>("Scenes/Playing/background");
            AssetTypes.Texture.Ceiling = Environments.Global.Content.Load<Texture2D>("Scenes/Playing/ceiling");

            //CharacterInfoBoard
            AssetTypes.Texture.InfoBoard = Environments.Global.Content.Load<Texture2D>("Scenes/Playing/board_clean");
            AssetTypes.Texture.CharacterWindow = Environments.Global.Content.Load<Texture2D>("Scenes/Playing/Character_window");
            AssetTypes.Texture.Megumin = Environments.Global.Content.Load<Texture2D>("Scenes/Playing/megumin");

            //Sound
            AssetTypes.Sound.MusicSound = Environments.Global.Content.Load<Song>("Sounds/PlayBGM");
        }

        public override void Setup()
        {
            Environments.GameData.Initialize();
            Environments.GameData.GenerateLevel();

            int minX = Constants.REFLECT_LEFT - Settings.BALL_SIZE / 2;
            int maxX = (Settings.WINDOW_WIDTH - Settings.PLAYING_UI_RIGHT_WIDTH) + (Settings.BALL_SIZE / 2);

            _bottomLine = new Shapes.Line(minX, Settings.WINDOW_HEIGHT - Settings.PLAYING_UI_BOTTOM_HEIGHT, maxX, Settings.WINDOW_HEIGHT - Settings.PLAYING_UI_BOTTOM_HEIGHT, 5, Constants.PLAY_WIDTH_LEFT + Settings.BALL_SIZE);
            _bottomLine.SetColor(new Color(255, 238, 184));
            _bottomLine.SetOrigin(2, 0);
            _bottomLine.SetPosition(minX, Settings.WINDOW_HEIGHT - Settings.PLAYING_UI_BOTTOM_HEIGHT - 135);
            _bottomLine.Rotation -= (float)(Math.PI / 2);

            _infoBoard = new Base.Sprite(AssetTypes.Texture.InfoBoard);
            _infoBoard.SetOrigin(0, _infoBoard.Height);
            _infoBoard.SetPosition(minX, Settings.WINDOW_HEIGHT - AssetTypes.Texture.TopWallBorder.Height);

            _megumin = new Base.Sprite(AssetTypes.Texture.Megumin, 110);
            _megumin.SetOrigin(0, _megumin.Height);
            _megumin.SetPosition(minX + 35, Settings.WINDOW_HEIGHT - AssetTypes.Texture.InfoBoard.Height);

            _characterWindow = new Base.Sprite(AssetTypes.Texture.CharacterWindow, 120);
            _characterWindow.SetOrigin(0, _characterWindow.Height);
            _characterWindow.SetPosition(minX + 30, Settings.WINDOW_HEIGHT - AssetTypes.Texture.InfoBoard.Height);

            _leftWall = new Base.Sprite(AssetTypes.Texture.LeftWall);
            _leftWall.SetPosition(0, 0);

            _wallpaper = new Base.Sprite(AssetTypes.Texture.WallPaper);
            _wallpaper.SetPosition(0, 0);

            _scoreboard = new Base.Sprite(AssetTypes.Texture.Scoreboard, 180);
            _scoreboard.SetPosition(35, 120);

            _leftWallBorder = new Base.Sprite(AssetTypes.Texture.WallBorder);
            _leftWallBorder.SetPosition(_leftWall.Width, 0);

            _rightWallBorder = new Base.Sprite(AssetTypes.Texture.WallBorder);
            _rightWallBorder.SetPosition(maxX, 0);

            _topWallBorder = new Base.Sprite(AssetTypes.Texture.TopWallBorder);
            _topWallBorder.SetPosition(minX, 0);

            _bottomWallBorder = new Base.Sprite(AssetTypes.Texture.TopWallBorder);
            _bottomWallBorder.SetOrigin(0, _bottomWallBorder.Instance.Height);
            _bottomWallBorder.SetPosition(minX, Settings.WINDOW_HEIGHT);

            _homeButton = new Sprites.Buttons(AssetTypes.Texture.Button, AssetTypes.Font.PlayingButton, "HOME", 220, 40);
            _homeButton.SetPosition((Settings.PLAYING_UI_LEFT_WIDTH - 100) / 2 - 10, Settings.WINDOW_HEIGHT - 100);

            _homeButton.Click += _homeButton_Click;

            _shooter = new Sprites.Shooter();

            //BGM
            _playBGM = AssetTypes.Sound.MusicSound;
            MediaPlayer.Play(_playBGM);

            base.Setup();
        }

        private void _homeButton_Click(object sender, EventArgs e)
        {
            Environments.Scene.SetScene(Types.SceneType.MAIN_MENU, true);
        }

        public override void Update()
        {
            if (!Environments.GameData.Failed && !Environments.GameData.Won)
            {
                if (Environments.GameData.ShootCount >= 8)
                {
                    _shakeWait += Environments.Global.GameTime.ElapsedGameTime.TotalSeconds;

                    if (_shake == 0)
                    {
                        _shake = 2;
                    }

                    if (_shakeWait >= .1)
                    {
                        _shakeWait = 0;
                        _shake = -_shake;
                    }
                }

                _shooter.Update();
            }
            else
            {
                if (Math.Abs(_shooter.NextBall.X - (_shooter.X + _shooter.Width / 2)) > 1)
                {
                    _shooter.NextBall.X -= (float)Environments.Global.GameTime.ElapsedGameTime.TotalSeconds * Settings.BALL_SIZE * 3;
                }
            }

            Environments.GameData.MagicCircles.RemoveAll(m => m.ShouldDestroy);

            foreach (Sprites.MagicCircle magicCirle in Environments.GameData.MagicCircles)
            {
                magicCirle.Update();
            }

            _homeButton.Update();

            base.Update();
        }

        public override void Draw()
        {
            Environments.Global.SpriteBatch.Draw(
                AssetTypes.Texture.PlayBackground,
                new Rectangle(Settings.PLAYING_UI_LEFT_WIDTH - Settings.BALL_SIZE / 2, 0, Constants.PLAY_WIDTH_LEFT + Settings.BALL_SIZE, Settings.WINDOW_HEIGHT),
                Color.White
            );

            DrawSprite(_bottomLine);
            DrawSprite(_infoBoard);
            DrawSprite(_characterWindow);
            DrawSprite(_megumin);

            DrawSprite(_leftWall);
            DrawSprite(_wallpaper);
            DrawSprite(_scoreboard);
            DrawSprite(_leftWallBorder);
            DrawSprite(_rightWallBorder);
            DrawSprite(_topWallBorder);
            DrawSprite(_bottomWallBorder);

            // render balls from template
            int[,] template = Environments.GameData.BallsTemplate;
            for (int i = 0; i < Settings.TEMPLATE_ROW_BALLS; i++)
            {
                for (int j = 0; j < Settings.TEMPLATE_COL_BALLS; j++)
                {
                    if (template[i, j] == 0) continue;

                    int code = template[i, j];
                    Types.BallType type = Types.Ball.BallTypeFromCode(code);

                    Vector2 pos = Utils.Ball.GetRenderPosition(i, j);

                    DrawSprite(new Sprites.Ball(type, (int)pos.X + _shake, (int)pos.Y));
                }
            }

            if (Environments.GameData.PushCount > 0)
            {
                for (int i = 0; i < Environments.GameData.PushCount; i++)
                {
                    Environments.Global.SpriteBatch.Draw(
                        AssetTypes.Texture.Ceiling,
                        new Vector2(Settings.PLAYING_UI_LEFT_WIDTH - Settings.BALL_SIZE / 2, Settings.PLAYING_UI_TOP_HEIGHT + AssetTypes.Texture.Ceiling.Height * i),
                        null,
                        Color.White,
                        0f,
                        Vector2.Zero,
                        1f,
                        SpriteEffects.None,
                        0f
                    );
                }
            }

            foreach (Sprites.MagicCircle magicCirle in Environments.GameData.MagicCircles)
            {
                magicCirle.Draw();
            }

            _shooter.Draw();

            _homeButton.Draw();

            base.Draw();
        }

        public override void Dispose(SceneType nextScene, bool fadeOut)
        {
            MediaPlayer.Stop();

            base.Dispose(nextScene, fadeOut);
        }
    }
}
