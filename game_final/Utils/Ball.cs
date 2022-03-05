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
        public static Texture2D GetBallTexture(Environments.Ball.BallType ballType)
        {
            switch (ballType)
            {
                case Environments.Ball.BallType.LIGHT_BLUE:
                    return AssetTypes.Texture.LightBlueBall;
                default:
                    return null;
            }
        }

        public static int BallCode(Environments.Ball.BallType ballType)
        {
            switch (ballType)
            {
                case Environments.Ball.BallType.LIGHT_BLUE:
                    return 1;
                default:
                    return -1;
            }
        }

        public static Environments.Ball.BallType BallTypeFromCode(int ballCode)
        {
            switch (ballCode)
            {
                case 1:
                    return Environments.Ball.BallType.LIGHT_BLUE;
                default:
                    return Environments.Ball.BallType.NONE;
            }
        }

        public static Vector2 GetSnappedPosition(Sprites.Ball ball)
        {
            float relativeX = ball.X - Settings.PLAYING_UI_LEFT_WIDTH;

            float x = relativeX / Constants.PLAY_WIDTH_LEFT;
            x = (float)Math.Floor(x * (Settings.TEMPLATE_COL_BALLS / 2));
            x = Math.Clamp(x, 0, (Settings.TEMPLATE_COL_BALLS / 2) - 1);

            float y = ball.Y / Constants.MAX_SNAP_Y;
            y = (float)Math.Floor(y * (Settings.TEMPLATE_ROW_BALLS));
            y = Math.Clamp(y, 0, Settings.TEMPLATE_ROW_BALLS - 1);

            return new Vector2(x, y);
        }

        public static bool ShouldSnap(Sprites.Ball ball)
        {
            int[,] template = Environments.GameData.BallsTemplate;

            int half = Settings.BALL_SIZE / 2;
            int snapX = (int)ball.SnapPoint.X * 2;
            int snapY = (int)ball.SnapPoint.Y;

            if (snapY > 0 && snapX > 0 && template[snapY - 1, snapX - 1] == 1)
            {
                //Debug.WriteLine($"top left {snapX * 2} {snapY}");
                return true;
            }
            else if (snapY > 0 && template[snapY - 1, snapX] == 1)
            {
                //Debug.WriteLine($"top {snapX * 2} {snapY}");
                return true;
            }
            else if (snapY > 0 && snapX < Settings.TEMPLATE_COL_BALLS - 1 && template[snapY - 1, snapX + 1] == 1)
            {
                //Debug.WriteLine($"top right {snapX * 2} {snapY}");
                return true;
            }
            else if (snapX > 0 && template[snapY, snapX - 1] == 1)
            {
                //Debug.WriteLine($"left {snapX * 2} {snapY}");
                return true;
            }
            else if (snapX < Settings.TEMPLATE_COL_BALLS - 1 && template[snapY, snapX + 1] == 1)
            {
                //Debug.WriteLine($"right {snapX * 2} {snapY}");
                return true;
            }

            return false;
        }

        private static Vector2 getPosFromIndex(int row, int col)
        {
            return new Vector2(col * Settings.BALL_SIZE, row * Settings.BALL_SIZE);
        }
    }
}
