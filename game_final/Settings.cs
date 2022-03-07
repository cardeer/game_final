using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace game_final
{
    static class Settings
    {
        public const string GAME_NAME = "My Mana Ran Out, I'm Going to Shoot Slimes in a Different World";

        public const int WINDOW_WIDTH = 1000;
        public const int WINDOW_HEIGHT = 900;

        public const int PLAYING_UI_TOP_HEIGHT = 50;
        public const int PLAYING_UI_BOTTOM_HEIGHT = 150;
        public const int PLAYING_UI_LEFT_WIDTH = 350 + BALL_SIZE / 2;
        public const int PLAYING_UI_RIGHT_WIDTH = 100 + BALL_SIZE / 2;

        public const int PLAY_AREA_TOP_PADDING = 10;

        public const int SHOOTER_BOTTOM = 70;

        public const int BALL_SIZE = 50;
        public const int BALL_SPEED = 1500;

        public const int TEMPLATE_COL_BALLS = ((WINDOW_WIDTH - PLAYING_UI_LEFT_WIDTH - PLAYING_UI_RIGHT_WIDTH) / BALL_SIZE) * 2 - 1;
        public const int TEMPLATE_ROW_BALLS = 13;

        public const int MAX_GUIDE_LENGTH = 500;
        public static Color GUIDE_COLOR = Color.White;
        public static Color REFLEFCT_GUIDE_COLOR = Color.Red;

        public static float MIN_SHOOTER_ANGLE = 25;
    }
}
