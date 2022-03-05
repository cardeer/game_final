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
        public const int PLAY_HALF_WIDTH = REFLECT_RIGHT - REFLECT_CENTER_X;
    }
}
