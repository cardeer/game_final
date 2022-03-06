using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Diagnostics;
using System;

namespace game_final.Scenes
{
    class Playing : Base.SceneRenderer
    {
        private Shapes.Line _leftVerticalLine;
        private Shapes.Line _rightVerticalLine;
        private Shapes.Line _topLine;
        private Shapes.Line _bottomLine;
        private Sprites.Shooter _shooter;

        public Playing()
        {
            Environments.GameData.Initialize();
            Environments.GameData.GenerateLevel();

            int minX = Constants.REFLECT_LEFT - Settings.BALL_SIZE / 2;
            int maxX = (Settings.WINDOW_WIDTH - Settings.PLAYING_UI_RIGHT_WIDTH) + (Settings.BALL_SIZE / 2);

            _leftVerticalLine = new Shapes.Line(minX, 0, minX, Settings.WINDOW_HEIGHT, 5, Settings.WINDOW_HEIGHT);
            _leftVerticalLine.SetColor(Color.Red);
            _leftVerticalLine.SetOrigin(2, 0);
            _leftVerticalLine.Rotation -= (float)(Math.PI / 2);

            _rightVerticalLine = new Shapes.Line(maxX, 0, maxX, Settings.WINDOW_HEIGHT, 5, Settings.WINDOW_HEIGHT);
            _rightVerticalLine.SetColor(Color.Red);
            _rightVerticalLine.SetOrigin(2, 0);
            _rightVerticalLine.Rotation -= (float)(Math.PI / 2);

            _bottomLine = new Shapes.Line(minX, Settings.WINDOW_HEIGHT - Settings.PLAYING_UI_BOTTOM_HEIGHT, maxX, Settings.WINDOW_HEIGHT - Settings.PLAYING_UI_BOTTOM_HEIGHT, 5, Constants.PLAY_WIDTH_LEFT + Settings.BALL_SIZE);
            _bottomLine.SetColor(Color.Red);
            _bottomLine.SetOrigin(2, 0);
            _bottomLine.Rotation -= (float)(Math.PI / 2);

            _topLine = new Shapes.Line(minX, Settings.PLAYING_UI_TOP_HEIGHT - Settings.PLAY_AREA_TOP_PADDING, maxX, Settings.PLAYING_UI_TOP_HEIGHT - Settings.PLAY_AREA_TOP_PADDING, 5, Constants.PLAY_WIDTH_LEFT + Settings.BALL_SIZE);
            _topLine.SetColor(Color.Red);
            _topLine.SetOrigin(2, 0);
            _topLine.Rotation -= (float)(Math.PI / 2);

            _shooter = new Sprites.Shooter();
        }

        public override void Update()
        {
            _shooter.Update();
        }

        public override void Draw()
        {
            DrawSprite(_leftVerticalLine);
            DrawSprite(_rightVerticalLine);
            DrawSprite(_topLine);
            DrawSprite(_bottomLine);
            _shooter.Draw();

            // render balls from template
            int[,] template = Environments.GameData.BallsTemplate;
            for (int i = 0; i < Settings.TEMPLATE_ROW_BALLS; i++)
            {
                for (int j = 0; j < Settings.TEMPLATE_COL_BALLS; j++)
                {
                    if (template[i, j] == 0) continue;

                    int code = template[i, j];
                    Types.BallType type = Types.Ball.BallTypeFromCode(code);

                    int x = Constants.SNAP_X_PADDING + (j * Settings.BALL_SIZE / 2);
                    int y = Settings.PLAYING_UI_TOP_HEIGHT + Settings.PLAY_AREA_TOP_PADDING + (i * Settings.BALL_SIZE);

                    DrawSprite(new Sprites.Ball(type, x, y));
                }
            }
        }
    }
}
