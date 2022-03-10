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
        private Base.Sprite _heart;
        private Base.Sprite _mana;
        private Base.Sprite _hourglass;
        private Base.Sprite _timeBoard;

        private Sprites.Buttons _UI_replay;
        private Sprites.Buttons _UI_home;
        private Sprites.IconButton _homeButton;
        private Sprites.IconButton _replayButton;
        private Sprites.IconButton _muteButton;

        private Sprites.Text _timeTex;
        private Sprites.Text _scoreTex;
        private Sprites.Text _ScorePointTex;
        private Sprites.Text _levelTex;
        private Sprites.Text _currentLevel;
        private Sprites.Text _hp;
        private Sprites.Text _mp;

        private Base.Sprite _winBoard;
        private Base.Sprite _loseBoard;

        private int _shake = 0;
        private double _shakeWait = 0;

        public static bool _isPlayingBGM;

        public Playing() : base(true) { }

        public override void LoadContent()
        {
            //ObjectTexture
            AssetTypes.Texture.Wand = Environments.Global.Content.Load<Texture2D>("Shooter/wand");
            AssetTypes.Texture.ShooterArrow = Environments.Global.Content.Load<Texture2D>("Shooter/arrow");
            AssetTypes.Texture.WinBoard = Environments.Global.Content.Load<Texture2D>("Dialogs/Board_Win");
            AssetTypes.Texture.LoseBoard = Environments.Global.Content.Load<Texture2D>("Dialogs/Board_Lose");

            //BallTexture
            AssetTypes.Texture.BlueBall = Environments.Global.Content.Load<Texture2D>("Balls/blue_slime");
            AssetTypes.Texture.LightBlueBall = Environments.Global.Content.Load<Texture2D>("Balls/lightBlue_slime");
            AssetTypes.Texture.GreenBall = Environments.Global.Content.Load<Texture2D>("Balls/green_slime");
            AssetTypes.Texture.GreyBall = Environments.Global.Content.Load<Texture2D>("Balls/grey_slime");
            AssetTypes.Texture.PurpleBall = Environments.Global.Content.Load<Texture2D>("Balls/purple_slime");
            AssetTypes.Texture.RedBall = Environments.Global.Content.Load<Texture2D>("Balls/red_slime");

            //Left Wall Texture
            AssetTypes.Texture.LeftWall = Environments.Global.Content.Load<Texture2D>("Scenes/Playing/left_wall");
            AssetTypes.Texture.WallPaper = Environments.Global.Content.Load<Texture2D>("Scenes/Playing/wallPaper");
            AssetTypes.Texture.Scoreboard = Environments.Global.Content.Load<Texture2D>("Scenes/Playing/scoreboard");
            AssetTypes.Texture.WallBorder = Environments.Global.Content.Load<Texture2D>("Scenes/Playing/wall_border");
            AssetTypes.Texture.TopWallBorder = Environments.Global.Content.Load<Texture2D>("Scenes/Playing/top_wall_border");
            AssetTypes.Texture.MagicCircle = Environments.Global.Content.Load<Texture2D>("Effects/magic_circle");
            AssetTypes.Texture.PlayBackground = Environments.Global.Content.Load<Texture2D>("Scenes/Playing/background");
            AssetTypes.Texture.Ceiling = Environments.Global.Content.Load<Texture2D>("Scenes/Playing/ceiling");
            AssetTypes.Texture.Hourglass = Environments.Global.Content.Load<Texture2D>("Scenes/Playing/hourglass");
            AssetTypes.Texture.TimeBoard = Environments.Global.Content.Load<Texture2D>("Scenes/Playing/timeBoard");

            //CharacterInfoBoard
            AssetTypes.Texture.InfoBoard = Environments.Global.Content.Load<Texture2D>("Scenes/Playing/board_clean");
            AssetTypes.Texture.CharacterWindow = Environments.Global.Content.Load<Texture2D>("Scenes/Playing/Character_window");
            AssetTypes.Texture.Megumin = Environments.Global.Content.Load<Texture2D>("Scenes/Playing/megumin");
            AssetTypes.Texture.Heart = Environments.Global.Content.Load<Texture2D>("Scenes/Playing/heart");
            AssetTypes.Texture.Mana = Environments.Global.Content.Load<Texture2D>("Scenes/Playing/manaPotion");

            //Sound
            AssetTypes.Sound.MusicSound = Environments.Global.Content.Load<Song>("Sounds/PlayBGM");
        }

        public override void Setup()
        {
            Environments.GameData.Initialize();
            Environments.GameData.GenerateLevel();

            int minX = Constants.REFLECT_LEFT - Settings.BALL_SIZE / 2;
            int maxX = (Settings.WINDOW_WIDTH - Settings.PLAYING_UI_RIGHT_WIDTH) + (Settings.BALL_SIZE / 2);

            //Texture
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

            _heart = new Base.Sprite(AssetTypes.Texture.Heart, 80);
            _heart.SetOrigin(0, _infoBoard.Height);
            _heart.SetPosition(Settings.WINDOW_WIDTH - 250, Settings.WINDOW_HEIGHT - AssetTypes.Texture.InfoBoard.Height + 20);

            _mana = new Base.Sprite(AssetTypes.Texture.Mana, 80);
            _mana.SetOrigin(0, _infoBoard.Height);
            _mana.SetPosition(Settings.WINDOW_WIDTH - 250, Settings.WINDOW_HEIGHT - AssetTypes.Texture.InfoBoard.Height + 50);

            _leftWall = new Base.Sprite(AssetTypes.Texture.LeftWall);
            _leftWall.SetPosition(0, 0);

            _wallpaper = new Base.Sprite(AssetTypes.Texture.WallPaper);
            _wallpaper.SetPosition(0, 0);

            _scoreboard = new Base.Sprite(AssetTypes.Texture.Scoreboard, 180);
            _scoreboard.SetPosition(35, 150);

            _leftWallBorder = new Base.Sprite(AssetTypes.Texture.WallBorder);
            _leftWallBorder.SetPosition(_leftWall.Width, 0);

            _rightWallBorder = new Base.Sprite(AssetTypes.Texture.WallBorder);
            _rightWallBorder.SetPosition(maxX, 0);

            _topWallBorder = new Base.Sprite(AssetTypes.Texture.TopWallBorder);
            _topWallBorder.SetPosition(minX, 0);

            _bottomWallBorder = new Base.Sprite(AssetTypes.Texture.TopWallBorder);
            _bottomWallBorder.SetOrigin(0, _bottomWallBorder.Instance.Height);
            _bottomWallBorder.SetPosition(minX, Settings.WINDOW_HEIGHT);

            _hourglass = new Base.Sprite(AssetTypes.Texture.Hourglass, 80);
            _hourglass.SetOrigin(_hourglass.Width / 2, _hourglass.Height / 2);
            _hourglass.SetPosition((_wallpaper.Width / 2) - 35, (_wallpaper.Height / 2) + 50);

            _timeBoard = new Base.Sprite(AssetTypes.Texture.TimeBoard, 150);
            _timeBoard.SetOrigin(_hourglass.Width / 2, _hourglass.Height / 2);
            _timeBoard.SetPosition(_hourglass.Position.X - 5, _hourglass.Position.Y + 200);

            _shooter = new Sprites.Shooter();

            //Text
            _timeTex = new Sprites.Text(AssetTypes.Font.PlayingButton, "00 : 00");
            _timeTex.Color = Color.White;
            _timeTex.Position = new Vector2(_timeBoard.Position.X + 5, _timeBoard.Position.Y - 50);

            _scoreTex = new Sprites.Text(AssetTypes.Font.UIFont, "SCORE");
            _scoreTex.Color = Color.Brown;
            _scoreTex.Position = new Vector2(_wallpaper.Width / 2 - 45, 120);

            _ScorePointTex = new Sprites.Text(AssetTypes.Font.UIFont, "999999999");
            _ScorePointTex.Position = new Vector2(70, 170);

            _levelTex = new Sprites.Text(AssetTypes.Font.UIFont, "LEVEL");
            _levelTex.Color = Color.Brown;
            _levelTex.Position = new Vector2(_wallpaper.Width / 2 - 45, 240);

            _hp = new Sprites.Text(AssetTypes.Font.PlayingButton, "HP");
            _hp.Color = Color.White;
            _hp.Position = new Vector2(Settings.WINDOW_WIDTH - 290, Settings.WINDOW_HEIGHT - AssetTypes.Texture.InfoBoard.Height + 5);

            _mp = new Sprites.Text(AssetTypes.Font.PlayingButton, "MP");
            _mp.Color = Color.White;
            _mp.Position = new Vector2(Settings.WINDOW_WIDTH - 290, Settings.WINDOW_HEIGHT - AssetTypes.Texture.InfoBoard.Height + 40);

            string currentTex = "";
            Color c = Color.Black;
            if (Environments.Global.Level == 1)
            {
                currentTex = "EASY";
                c = Color.LightGreen;
            }
            else if (Environments.Global.Level == 2) 
            {
                currentTex = "NORMAL";
                c = Color.Yellow;
            }
            else if (Environments.Global.Level == 3) 
            {
                currentTex = "HARD";
                c = Color.OrangeRed;
            }
            _currentLevel = new Sprites.Text(AssetTypes.Font.UIFont, currentTex);
            _currentLevel.Color = c;
            _currentLevel.Position = new Vector2(_wallpaper.Width / 2 - 50, 290);

            //Button
            _homeButton = new Sprites.IconButton(AssetTypes.Texture.IconHome, AssetTypes.Font.PlayingButton, 50, 50);
            _homeButton.SetPosition((_leftWall.Width / 2) + 60, 370);
            _homeButton.Effect = false;

            _replayButton = new Sprites.IconButton(AssetTypes.Texture.IconReplay, AssetTypes.Font.PlayingButton, 50, 50);
            _replayButton.SetPosition((_leftWall.Width / 2), 370);
            _replayButton.Effect = false;
            _replayButton.MultipleClicks = true;

            _muteButton = new Sprites.IconButton(AssetTypes.Texture.IconMute, AssetTypes.Font.PlayingButton, 50, 50);
            _muteButton.SetPosition((_leftWall.Width / 2) - 60, 370);
            _muteButton.Effect = false;
            _muteButton.MultipleClicks = true;

            _UI_replay = new Sprites.Buttons(AssetTypes.Texture.Button, AssetTypes.Font.SpriteFont, "REPLAY", 200, 50);
            _UI_replay.SetPosition(Settings.WINDOW_WIDTH / 2 - 100, 500);
            _UI_replay.TextColor = Color.White;
            _UI_replay.Effect = false;

            _UI_home = new Sprites.Buttons(AssetTypes.Texture.Button, AssetTypes.Font.SpriteFont, "HOME", 200, 50);
            _UI_home.SetPosition(Settings.WINDOW_WIDTH / 2 + 100, 500);
            _UI_home.TextColor = Color.White;
            _UI_home.Effect = false;

            _homeButton.Click += _homeButton_Click;
            _replayButton.Click += _replayButton_Click;
            _muteButton.Click += _muteButton_Click;
            _UI_replay.Click += _replayButton_Click;
            _UI_home.Click += _homeButton_Click;

            //Dialog
            _winBoard = new Base.Sprite(AssetTypes.Texture.WinBoard, 700);
            _winBoard.SetOrigin(_winBoard.Instance.Width / 2, _winBoard.Instance.Height / 2);
            _winBoard.SetPosition(Settings.WINDOW_WIDTH / 2, Settings.WINDOW_HEIGHT / 2);

            _loseBoard = new Base.Sprite(AssetTypes.Texture.LoseBoard, 700);
            _loseBoard.SetOrigin(_loseBoard.Instance.Width / 2, _loseBoard.Instance.Height / 2);
            _loseBoard.SetPosition(Settings.WINDOW_WIDTH / 2, Settings.WINDOW_HEIGHT / 2);

            //BGM
            _playBGM = AssetTypes.Sound.MusicSound;
            MediaPlayer.Volume = Settings.PLAYING_BGM_VOLUME;
            MediaPlayer.Play(_playBGM);
            MediaPlayer.IsRepeating = true;
            _isPlayingBGM = true;

            base.Setup();
        }

        private void _homeButton_Click(object sender, EventArgs e)
        {
            Environments.Scene.SetScene(Types.SceneType.MAIN_MENU, true);
        }

        private void _replayButton_Click(object sender, EventArgs e)
        {
            Environments.GameData.Initialize();
            Environments.GameData.GenerateLevel();
        }

        private void _muteButton_Click(object sender, EventArgs e)
        {
            if (_isPlayingBGM)
            {
                MediaPlayer.Volume = 0f;
                _isPlayingBGM = false;
                _muteButton.SetTexture(AssetTypes.Texture.IconUnmute);
            }
            else
            {
                MediaPlayer.Volume = Settings.PLAYING_BGM_VOLUME;
                _isPlayingBGM = true;
                _muteButton.SetTexture(AssetTypes.Texture.IconMute);
            }
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

                    if (_shakeWait >= (Environments.GameData.ShootCount == 8 ? .1f : .05f))
                    {
                        _shakeWait = 0;
                        _shake = -_shake;
                    }
                }

                _shooter.Update();
            }

            Environments.GameData.MagicCircles.RemoveAll(m => m.ShouldDestroy);
            Environments.GameData.BallsToDrop.RemoveAll(b => b.Y >= Settings.WINDOW_HEIGHT + Settings.BALL_SIZE / 2);

            foreach (Sprites.MagicCircle magicCirle in Environments.GameData.MagicCircles)
            {
                magicCirle.Update();
            }

            foreach (Sprites.Ball ball in Environments.GameData.BallsToDrop)
            {
                ball.SetPosition(ball.X, (int)(ball.Y + Environments.Global.GameTime.ElapsedGameTime.TotalSeconds * Settings.BALL_SPEED));
            }

            _homeButton.Update();
            _replayButton.Update();
            _muteButton.Update();

            _UI_home.CanClick = Environments.GameData.Won || Environments.GameData.Failed;
            _UI_home.Update();

            _UI_replay.CanClick = Environments.GameData.Won || Environments.GameData.Failed;
            _UI_replay.Update();

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
            DrawSprite(_heart);
            DrawSprite(_mana);

            DrawSprite(_leftWall);
            DrawSprite(_wallpaper);
            DrawSprite(_scoreboard);
            DrawSprite(_leftWallBorder);
            DrawSprite(_rightWallBorder);
            DrawSprite(_topWallBorder);
            DrawSprite(_bottomWallBorder);
            DrawSprite(_hourglass);
            DrawSprite(_timeBoard);

            //Text
            _timeTex.Draw();
            _scoreTex.Draw();
            _ScorePointTex.Draw();
            _levelTex.Draw();
            _currentLevel.Draw();
            _hp.Draw();
            _mp.Draw();

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

            _shooter.Draw();

            foreach (Sprites.Ball ball in Environments.GameData.BallsToDrop)
            {
                Environments.Global.SpriteBatch.Draw(
                    ball.Instance,
                    ball.Position,
                    null,
                    ball.DrawColor * (1 - (float)((ball.Position.Y - ball.StartY) / 500)),
                    0f,
                    ball.Origin,
                    ball.Scale,
                    SpriteEffects.None,
                    0f
                );
            }

            foreach (Sprites.MagicCircle magicCirle in Environments.GameData.MagicCircles)
            {
                magicCirle.Draw();
            }

            _homeButton.Draw();
            _replayButton.Draw();
            _muteButton.Draw();

            if (Environments.GameData.Won)
            {
                _winBoard.Draw();
                _UI_replay.Draw();
                _UI_home.Draw();
            }
            else if (Environments.GameData.Failed)
            {
                _loseBoard.Draw();
                _UI_replay.Draw();
                _UI_home.Draw();
            }

            base.Draw();
        }

        public override void Dispose(SceneType nextScene, bool fadeOut)
        {
            MediaPlayer.Stop();

            base.Dispose(nextScene, fadeOut);
        }
    }
}
