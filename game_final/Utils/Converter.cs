using System;
using System.Collections.Generic;
using System.Text;

namespace game_final.Utils
{
    class Converter
    {

        public static float DegressToRadians(double degrees) {
            return (float) (degrees * Math.PI / 180);
        }

        public static float DegressToRadians(float degrees)
        {
            return (float)(degrees * Math.PI / 180);
        }

        public static float RadiansToDegrees(float radians) {
            return (float)(radians * 180 / Math.PI);
        }

        public static float RadiansToDegrees(double radians)
        {
            return (float)(radians * 180 / Math.PI);
        }
    }
}
