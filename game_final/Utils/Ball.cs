using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_final.Utils
{
    static class Ball
    {
        public static Vector2 GetSnappedPosition(Sprites.Ball ball)
        {
            float relativeX = ball.X - Settings.PLAYING_UI_LEFT_WIDTH;

            float x = relativeX / Constants.PLAY_WIDTH_LEFT;
            x = (float)(x * (Settings.TEMPLATE_COL_BALLS - 1));
            x = Math.Clamp(x, 0, Settings.TEMPLATE_COL_BALLS - 1);

            float y = ball.Y / Constants.MAX_SNAP_Y;
            y = (float)(y * (Settings.TEMPLATE_ROW_BALLS - 1));
            y = Math.Clamp(y, 0, Settings.TEMPLATE_ROW_BALLS - 1);

            return new Vector2(x, y);
        }

        public static Types.Snap ShouldSnap(Sprites.Ball ball)
        {
            int[,] template = Environments.GameData.BallsTemplate;

            int half = Settings.BALL_SIZE / 2;

            float posY = ball.SnapPoint.Y;
            float posX = ball.SnapPoint.X;

            int roundY = (int)Math.Ceiling(posY);
            int roundX = (int)Math.Round(posX);

            Vector2 pos = getPosFromIndex(posY, posX);
            Vector2 left = getPosFromIndex(roundY, roundX - 1);
            Vector2 right = getPosFromIndex(roundY, roundX + 1);
            Vector2 topLeft = getPosFromIndex(roundY - 1, roundX - 1);
            Vector2 top = getPosFromIndex(roundY - 1, roundX);
            Vector2 topRight = getPosFromIndex(roundY - 1, roundX + 1);

            Types.Snap result = new Types.Snap();
            result.SnapRow = roundY;
            result.SnapCol = roundX;

            if (roundY == 0)
            {
                result.ShouldSnap = true;
            }
            else if (roundY > 0 && roundX > 0 && template[roundY - 1, roundX - 1] > 0 && pos.Y - half <= topLeft.Y + half && pos.X - half <= topLeft.X + half)
            {
                result.ShouldSnap = true;
            }
            else if (roundY > 0 && template[roundY - 1, roundX] > 0 && pos.Y - half <= top.Y + half)
            {
                result.ShouldSnap = true;
            }
            else if (roundY > 0 && roundX < Settings.TEMPLATE_COL_BALLS - 1 && template[roundY - 1, roundX + 1] > 0 && pos.Y - half <= topRight.Y + half && pos.X + half >= topRight.X - half)
            {
                result.ShouldSnap = true;
            }
            else if (roundX > 0 && template[roundY, roundX - 1] > 0 && pos.X - half <= left.X + half)
            {
                result.ShouldSnap = true;
            }
            else if (roundX < Settings.TEMPLATE_COL_BALLS - 1 && template[roundY, roundX + 1] > 0 && pos.X + half >= right.X - half)
            {
                result.ShouldSnap = true;
            }

            if (result.ShouldSnap) result.FitPoints();

            return result;
        }

        private static Vector2 getPosFromIndex(float row, float col)
        {
            return new Vector2(col * Settings.BALL_SIZE, row * Settings.BALL_SIZE);
        }

        public static Types.BallType RandomBallType()
        {
            return Types.BallType.NONE;
        }
    }
}
