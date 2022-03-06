using System;
using System.Collections.Generic;
using System.Text;

namespace game_final
{
    static class Constants
    {
        public const int REFLECT_LEFT = Settings.PLAYING_UI_LEFT_WIDTH;
        public const int REFLECT_RIGHT = Settings.WINDOW_WIDTH - Settings.PLAYING_UI_RIGHT_WIDTH;
        public const int REFLECT_CENTER_X = (Settings.PLAYING_UI_LEFT_WIDTH + (Settings.WINDOW_WIDTH - Settings.PLAYING_UI_RIGHT_WIDTH)) / 2;

        public const int PLAY_HALF_X = REFLECT_RIGHT - REFLECT_CENTER_X;
        public const int PLAY_WIDTH_LEFT = Settings.WINDOW_WIDTH - Settings.PLAYING_UI_RIGHT_WIDTH - Settings.PLAYING_UI_LEFT_WIDTH;
        public const int SNAP_X_PADDING = REFLECT_LEFT + (PLAY_WIDTH_LEFT - ((Settings.TEMPLATE_COL_BALLS / 2)) * Settings.BALL_SIZE) / 2;

        public const int MAX_SNAP_Y = (Settings.BALL_SIZE / 2) + (Settings.TEMPLATE_ROW_BALLS - 1) * Settings.BALL_SIZE;
    }
}
