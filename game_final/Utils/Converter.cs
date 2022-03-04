using System;
using System.Collections.Generic;
using System.Text;

namespace game_final.Utils
{
    class Converter
    {
        public static double RadiansToDegrees(double radians) {
            return radians * 180 / Math.PI;
        }
    }
}
