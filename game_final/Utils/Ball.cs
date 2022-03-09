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
            float relativeX = ball.X - Settings.PLAYING_UI_LEFT_WIDTH - Settings.BALL_SIZE / 4;
            float relativeY = ball.Y - Settings.PLAYING_UI_TOP_HEIGHT - Settings.PLAY_AREA_TOP_PADDING - Settings.BALL_SIZE / 2;

            float x = relativeX / Constants.PLAY_WIDTH_LEFT;
            x = (float)(x * (Settings.TEMPLATE_COL_BALLS - 1));
            x = Math.Clamp(x, 0, Settings.TEMPLATE_COL_BALLS - 1);

            float y = relativeY / Constants.MAX_SNAP_Y;
            y = (float)(y * (Settings.TEMPLATE_ROW_BALLS));
            y = Math.Clamp(y, 0, Settings.TEMPLATE_ROW_BALLS - 1);

            return new Vector2(x, y);
        }

        public static Types.Snap ShouldSnap(Sprites.Ball ball)
        {
            int[,] template = Environments.GameData.BallsTemplate;

            int half = Settings.BALL_SIZE / 2;
            int size = Settings.BALL_SIZE;

            float posY = ball.SnapPoint.Y;
            float posX = ball.SnapPoint.X;

            int roundY = (int)Math.Round(posY);
            int roundX = (int)Math.Round(posX);

            ball.PreviousSnap = ball.CurrentSnap;

            int pushState = Environments.GameData.PushCount % 2;

            if (pushState == 0)
            {
                if (roundX == 0 && roundY % 2 != roundX % 2)
                {
                    roundX++;
                }
                else if (roundX == Settings.TEMPLATE_COL_BALLS - 1 && roundY % 2 != roundX % 2)
                {
                    roundX--;
                }
            }
            else
            {
                if (roundX == 0 && roundY % 2 == roundX % 2)
                {
                    roundX++;
                }
                else if (roundX == Settings.TEMPLATE_COL_BALLS - 1 && roundY % 2 == roundX % 2)
                {
                    roundX--;
                }
            }

            ball.CurrentSnap = new Types.Vector2Int(roundX, roundY);

            Vector2 pos = GetPosFromIndex(posY, posX);
            Vector2 left = GetPosFromIndex(roundY, roundX - 1);
            Vector2 right = GetPosFromIndex(roundY, roundX + 1);
            Vector2 right2 = GetPosFromIndex(roundY, roundX + 2);
            Vector2 left2 = GetPosFromIndex(roundY, roundX - 2);
            Vector2 topLeft = GetPosFromIndex(roundY - 1, roundX - 1);
            Vector2 top = GetPosFromIndex(roundY - 1, roundX);
            Vector2 topRight = GetPosFromIndex(roundY - 1, roundX + 1);

            Types.Snap result = new Types.Snap();
            result.SnapRow = roundY;
            result.SnapCol = roundX;

            //if (template[roundY, roundX] > 0)
            //{
            //    if (ball.Unit.X > 0 || ball.Unit.X < 0)
            //    {
            //        if (Math.Ceiling(pos.X) - pos.X <= pos.X - Math.Floor(pos.X) && roundX + 2 < Settings.TEMPLATE_COL_BALLS) roundX += 2;
            //        else if (roundX - 2 >= 0) roundX -= 2;
            //    }
            //}

            if (roundY <= Environments.GameData.PushCount)
            {
                result.SnapRow = Environments.GameData.PushCount;
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
            else if (ball.Unit.X < 0 && roundX - 2 >= 0 && template[roundY, roundX - 2] > 0 && pos.X - size <= left2.X + half)
            {
                result.ShouldSnap = true;
            }
            else if (ball.Unit.X > 0 && roundX + 2 < Settings.TEMPLATE_COL_BALLS && template[roundY, roundX + 2] > 0 && pos.X + size >= right2.X - half)
            {
                result.ShouldSnap = true;
            }

            if (result.ShouldSnap)
            {
                ball.Destroy();
                result.FitPoints(ball);
            }

            return result;
        }

        public static Vector2 GetPosFromIndex(float row, float col)
        {
            return new Vector2(col * Settings.BALL_SIZE, row * Settings.BALL_SIZE);
        }

        public static int RandomBallCode(bool newTemplate = false)
        {
            if (!newTemplate)
            {
                int[,] template = Environments.GameData.BallsTemplate;
                List<int> ballCodesInTemplate = new List<int>();

                for (int i = 0; i < Settings.TEMPLATE_ROW_BALLS; i++)
                {
                    for (int j = 0; j < Settings.TEMPLATE_COL_BALLS; j++)
                    {
                        if (template[i, j] == 0) continue;

                        int code = template[i, j];

                        if (!ballCodesInTemplate.Contains(code))
                        {
                            ballCodesInTemplate.Add(code);
                        }
                    }
                }
                int randomIndex = new Random().Next(0, ballCodesInTemplate.Count - 1);
                return ballCodesInTemplate[randomIndex];
            }

            return new Random().Next(1, 8);
        }

        public static Vector2 GetRenderPosition(int row, int col)
        {
            int x = Constants.SNAP_X_PADDING + Settings.BALL_SIZE / 4 + (col * Settings.BALL_SIZE / 2);
            int y = Settings.PLAYING_UI_TOP_HEIGHT + Settings.PLAY_AREA_TOP_PADDING + Settings.BALL_SIZE / 2 + (row * Settings.BALL_SIZE);

            return new Vector2(x, y);
        }
    }
}
