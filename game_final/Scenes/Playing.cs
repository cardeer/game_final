using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Diagnostics;

namespace game_final.Scenes
{
    class Playing : Base.UIRenderer
    {
        private Shapes.Line _rightVerticalLine;

        public Playing(SpriteBatch spriteBatch, GraphicsDevice graphics) : base(spriteBatch)
        {
            _rightVerticalLine = new Shapes.Line(graphics, Constants.REFLECT_LEFT - Settings.BALL_SIZE / 2, 0, Constants.REFLECT_LEFT - Settings.BALL_SIZE / 2, Settings.WINDOW_HEIGHT, 5, Settings.WINDOW_HEIGHT);
            _rightVerticalLine.SetColor(Color.Red);
            _rightVerticalLine.SetOrigin(2, 0);
        }

        public void Draw()
        {
            DrawSprite(_rightVerticalLine);
            DrawSprite(new Sprites.Ball(Environments.Ball.BallType.LIGHT_BLUE, 100, 100));

            // render balls from template
            //int[,] template = Environments.GameData.BallsTemplate;
            //for (int i = 0; i < Settings.TEMPLATE_ROW_BALLS; i++)
            //{
            //    for (int j = 0; j < Settings.TEMPLATE_COL_BALLS; j++)
            //    {
            //        if (template[i, j] == 0) continue;

            //        int code = template[i, j];
            //        Environments.Ball.BallType type = Utils.Ball.BallTypeFromCode(code);

            //        int x = Constants.SNAP_X_PADDING + (j * Settings.BALL_SIZE);
            //    }
            //}
        }
    }
}
