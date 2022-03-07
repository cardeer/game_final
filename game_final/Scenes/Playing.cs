using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Diagnostics;
using System;

namespace game_final.Scenes
{
    class Playing : Base.SceneRenderer
    {
        private Shapes.Line _bottomLine;
        private Shapes.Rectangle _bottomRect;

        private Sprites.Shooter _shooter;

        private Base.Sprite _leftWall;
        private Base.Sprite _leftWallBorder;
        private Base.Sprite _rightWallBorder;
        private Base.Sprite _topWallBorder;
        private Base.Sprite _bottomWallBorder;

        private int _shake = 0;
        private double _shakeWait = 0;

        public override void LoadContent()
        {
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

            _shooter = new Sprites.Shooter();
        }

        public override void Update()
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

            Environments.GameData.MagicCircles.RemoveAll(m => m.ShouldDestroy);

            foreach (Sprites.MagicCircle magicCirle in Environments.GameData.MagicCircles)
            {
                magicCirle.Update();
            }
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
        }

        public override void Dispose()
        {
            AssetTypes.Texture.Wand.Dispose();
            AssetTypes.Texture.BlueBall.Dispose();
            AssetTypes.Texture.BrownBall.Dispose();
            AssetTypes.Texture.GreenBall.Dispose();
            AssetTypes.Texture.GreyBall.Dispose();
            AssetTypes.Texture.LightBlueBall.Dispose();
            AssetTypes.Texture.PurpleBall.Dispose();
            AssetTypes.Texture.RedBall.Dispose();
            AssetTypes.Texture.YellowBall.Dispose();
            AssetTypes.Texture.LeftWall.Dispose();
            AssetTypes.Texture.WallBorder.Dispose();
            AssetTypes.Texture.TopWallBorder.Dispose();
            AssetTypes.Texture.BottomWallBorder.Dispose();

            base.Dispose();
        }
    }
}
