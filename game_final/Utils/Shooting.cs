using Microsoft.Xna.Framework;

using System;
using System.Diagnostics;

namespace game_final.Utils
{
    static class Shooting
    {
        public static void GetSnappedPosition(Sprites.Ball ball)
        {
            float relativeX = ball.X - Settings.PLAYING_UI_LEFT_WIDTH;

            float x = relativeX / Constants.PLAY_WIDTH_LEFT;
            x = (float)Math.Round(x * Settings.TEMPLATE_COL_BALLS);
            x = Math.Clamp(x, 0, Settings.TEMPLATE_COL_BALLS - 1);
            float y = 0;

            SetBallTemplate((int)x, (int)y, Utils.Ball.BallCode(ball.Type));
        }

        public static void SetBallTemplate(int x, int y, int ballTypeCode)
        {
            Environments.GameData.BallsTemplate[y, x] = ballTypeCode;
        }
    }
}
