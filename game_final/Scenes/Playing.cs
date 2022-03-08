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
        private Shapes.Rectangle _bottomRect;

        private Sprites.Shooter _shooter;

        private Song _playBGM;

        private Base.Sprite _leftWall;
        private Base.Sprite _leftWallBorder;
        private Base.Sprite _rightWallBorder;
        private Base.Sprite _topWallBorder;
        private Base.Sprite _bottomWallBorder;

        private Sprites.Buttons _exitButton;

        private int _shake = 0;
        private double _shakeWait = 0;

        public Playing() : base(true) { }

        public override void LoadContent()
        {   
            //Texture
            AssetTypes.Texture.Wand = Environments.Global.Content.Load<Texture2D>("Shooter/wand");
            AssetTypes.Texture.BlueBall = Environments.Global.Content.Load<Texture2D>("Balls/blue");
            AssetTypes.Texture.BrownBall = Environments.Global.Content.Load<Texture2D>("Balls/brown");
            AssetTypes.Texture.GreenBall = Environments.Global.Content.Load<Texture2D>("Balls/green");
            AssetTypes.Texture.GreyBall = Environments.Global.Content.Load<Texture2D>("Balls/grey");
            AssetTypes.Texture.LightBlueBall = Environments.Global.Content.Load<Texture2D>("Balls/light_blue");
            AssetTypes.Texture.PurpleBall = Environments.Global.Content.Load<Texture2D>("Balls/purple");
            AssetTypes.Texture.RedBall = Environments.Global.Content.Load<Texture2D>("Balls/red");
            AssetTypes.Texture.YellowBall = Environments.Global.Content.Load<Texture2D>("Balls/yellow");
            AssetTypes.Texture.LeftWall = Environments.Global.Content.Load<Texture2D>("Scenes/Playing/left_wall");
            AssetTypes.Texture.WallBorder = Environments.Global.Content.Load<Texture2D>("Scenes/Playing/wall_border");
            AssetTypes.Texture.TopWallBorder = Environments.Global.Content.Load<Texture2D>("Scenes/Playing/top_wall_border");
            AssetTypes.Texture.BottomWallBorder = Environments.Global.Content.Load<Texture2D>("Scenes/Playing/bottom_wall_border");
            AssetTypes.Texture.MagicCircle = Environments.Global.Content.Load<Texture2D>("Effects/magic_circle");

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

            _bottomRect = new Shapes.Rectangle(maxX - minX, Settings.PLAYING_UI_BOTTOM_HEIGHT);
            _bottomRect.SetOrigin(0, _bottomRect.Height);
            _bottomRect.SetColor(new Color(255, 238, 184));
            _bottomRect.SetPosition(minX, Settings.WINDOW_HEIGHT - AssetTypes.Texture.TopWallBorder.Height);

            _leftWall = new Base.Sprite(AssetTypes.Texture.LeftWall);
            _leftWall.SetPosition(0, 0);

            _leftWallBorder = new Base.Sprite(AssetTypes.Texture.WallBorder);
            _leftWallBorder.SetPosition(_leftWall.Width, 0);

            _rightWallBorder = new Base.Sprite(AssetTypes.Texture.WallBorder);
            _rightWallBorder.SetPosition(maxX, 0);

            _topWallBorder = new Base.Sprite(AssetTypes.Texture.TopWallBorder);
            _topWallBorder.SetPosition(minX, 0);

            _bottomWallBorder = new Base.Sprite(AssetTypes.Texture.TopWallBorder);
            _bottomWallBorder.SetOrigin(0, _bottomWallBorder.Instance.Height);
            _bottomWallBorder.SetPosition(minX, Settings.WINDOW_HEIGHT);

            _exitButton = new Sprites.Buttons(AssetTypes.Texture.Button, AssetTypes.Font.PlayingButton, "EXIT", 220, 40);
            _exitButton.SetPosition((Settings.PLAYING_UI_LEFT_WIDTH - 100) / 2 - 10, Settings.WINDOW_HEIGHT - 50);

            _exitButton.Click += _exitButton_Click;

            _shooter = new Sprites.Shooter();

            //BGM
            _playBGM = AssetTypes.Sound.MusicSound;
            MediaPlayer.Play(_playBGM);

            base.Setup();
        }

        private void _exitButton_Click(object sender, EventArgs e)
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

            Environments.GameData.MagicCircles.RemoveAll(m => m.ShouldDestroy);

            foreach (Sprites.MagicCircle magicCirle in Environments.GameData.MagicCircles)
            {
                magicCirle.Update();
            }

            _exitButton.Update();

            base.Update();
        }

        public override void Draw()
        {
            DrawSprite(_bottomLine);
            DrawSprite(_bottomRect);

            DrawSprite(_leftWall);
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

            foreach (Sprites.MagicCircle magicCirle in Environments.GameData.MagicCircles)
            {
                magicCirle.Draw();
            }

            _shooter.Draw();

            _exitButton.Draw();

            base.Draw();
        }

        public override void Dispose(SceneType nextScene, bool fadeOut)
        {
            MediaPlayer.Stop();

            base.Dispose(nextScene, fadeOut);
        }
    }
}
