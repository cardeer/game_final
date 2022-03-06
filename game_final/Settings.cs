using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace game_final
{
    static class Settings
    {
        public const int WINDOW_WIDTH = 1000;
        public const int WINDOW_HEIGHT = 900;

        public const int MAX_GUIDE_LENGTH = 600;

        public const int PLAYING_UI_LEFT_WIDTH = 400;
        public const int PLAYING_UI_RIGHT_WIDTH = BALL_SIZE / 2;

        public const int BALL_SIZE = 50;
        public const int BALL_SPEED = 20;

        public const int TEMPLATE_COL_BALLS = 20;
        public const int TEMPLATE_ROW_BALLS = 15;

        public static Color GUIDE_COLOR = Color.White;
    }
}
