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
    }
}
