using System;
using System.Collections.Generic;
using System.Text;

namespace game_final.Utils
{
    class Converter
    {
        /// <summary>
        /// Change radians to degrees
        /// </summary>
        /// <param name="radians"></param>
        /// <returns></returns>
        public static double RadiansToDegrees(double radians) {
            return radians * 180 / Math.PI;
        }
    }
}
