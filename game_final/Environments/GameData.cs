using System;
using System.Collections.Generic;
using System.Text;

namespace game_final.Environments
{
    class GameData
    {
        public static int[,] BallsTemplate;
        public static List<Sprites.Ball> ShotBalls;

        public static void Initialize()
        {
            BallsTemplate = new int[Settings.TEMPLATE_ROW_BALLS, Settings.TEMPLATE_COL_BALLS];
            ShotBalls = new List<Sprites.Ball>();
        }
    }
}
