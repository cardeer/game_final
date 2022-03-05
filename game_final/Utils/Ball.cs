using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework.Graphics;

namespace game_final.Utils
{
    static class Ball
    {
        public static Texture2D GetBallTexture(Environments.Ball.BallType ballType) { 
            switch (ballType)
            {
                case Environments.Ball.BallType.LIGHT_BLUE:
                    return AssetTypes.Texture.LightBlueBall;
                default:
                    return null;
            }
        }

        public static int BallCode(Environments.Ball.BallType ballType)
        {
            switch (ballType)
            {
                case Environments.Ball.BallType.LIGHT_BLUE:
                    return 1;
                default:
                    return -1;
            }
        }

        public static Environments.Ball.BallType BallTypeFromCode(int ballCode)
        {
            switch (ballCode)
            {
                case 1:
                    return Environments.Ball.BallType.LIGHT_BLUE;
                default:
                    return Environments.Ball.BallType.NONE;
            }
        }
    }
}
