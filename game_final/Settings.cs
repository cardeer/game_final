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

        public const int PLAYING_UI_TOP_HEIGHT = 50 + BALL_SIZE / 2;
        public const int PLAYING_UI_BOTTOM_HEIGHT = 150;
        public const int PLAYING_UI_LEFT_WIDTH = 350;
        public const int PLAYING_UI_RIGHT_WIDTH = 100;

        public const int PLAY_AREA_TOP_PADDING = BALL_SIZE / 2  - 5;

        public const int SHOOTER_BOTTOM = 50;

        public const int BALL_SIZE = 50;
        public const int BALL_SPEED = 1000;

        public const int TEMPLATE_COL_BALLS = ((WINDOW_WIDTH - PLAYING_UI_LEFT_WIDTH - PLAYING_UI_RIGHT_WIDTH) / BALL_SIZE) * 2 - 1;
        public const int TEMPLATE_ROW_BALLS = 13;

        public static Color GUIDE_COLOR = Color.White;
    }
}
