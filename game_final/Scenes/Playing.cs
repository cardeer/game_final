using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Diagnostics;
using System;

namespace game_final.Scenes
{
    class Playing : Base.UIRenderer
    {
        private Shapes.Line _leftVerticalLine;

        public Playing(SpriteBatch spriteBatch, GraphicsDevice graphics) : base(spriteBatch)
        {
            _leftVerticalLine = new Shapes.Line(graphics, Constants.REFLECT_LEFT - Settings.BALL_SIZE / 2, 0, Constants.REFLECT_LEFT - Settings.BALL_SIZE / 2, Settings.WINDOW_HEIGHT, 5, Settings.WINDOW_HEIGHT);
            _leftVerticalLine.SetColor(Color.Red);
            _leftVerticalLine.SetOrigin(2, 0);
            _leftVerticalLine.Rotation -= (float)(Math.PI / 2);
        }

        public void Draw()
        {
            DrawSprite(_leftVerticalLine);

            // render balls from template

           int[,] template = Environments.GameData.BallsTemplate;
            for (int i = 0; i < Settings.TEMPLATE_ROW_BALLS; i++)
            {
                for (int j = 0; j < Settings.TEMPLATE_COL_BALLS; j++)
                {
                    if (template[i, j] == 0) continue;

                    int code = template[i, j];
                    Types.BallType type = Utils.Ball.BallTypeFromCode(code);

                    int x = Constants.SNAP_X_PADDING + (j * Settings.BALL_SIZE / 2);
                    int y = Settings.BALL_SIZE / 2 + (i * Settings.BALL_SIZE);

                    DrawSprite(new Sprites.Ball(type, x, y));
                }
            }
        }
    }
}
