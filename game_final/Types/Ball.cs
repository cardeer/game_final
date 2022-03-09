using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework.Graphics;

namespace game_final.Types
{
    enum BallType
    {
        BLUE,
        LIGHT_BLUE,
        GREEN,
        GREY,
        PURPLE,
        RED,
        NONE
    }

    static class Ball
    {
        public static int TotalTypes = 6;
        public static Texture2D GetBallTexture(BallType ballType)
        {
            switch (ballType)
            {
                case BallType.BLUE:
                    return AssetTypes.Texture.BlueBall;
                case BallType.LIGHT_BLUE:
                    return AssetTypes.Texture.LightBlueBall;
                case BallType.GREEN:
                    return AssetTypes.Texture.GreenBall;
                case BallType.GREY:
                    return AssetTypes.Texture.GreyBall;
                case BallType.PURPLE:
                    return AssetTypes.Texture.PurpleBall;
                case BallType.RED:
                    return AssetTypes.Texture.RedBall;
                default:
                    return null;
            }
        }

        public static int BallCode(BallType ballType)
        {
            switch (ballType)
            {
                case BallType.BLUE:
                    return 1;
                case BallType.LIGHT_BLUE:
                    return 2;
                case BallType.GREEN:
                    return 3;
                case BallType.GREY:
                    return 4;
                case BallType.PURPLE:
                    return 5;
                case BallType.RED:
                    return 6;
                default:
                    return -1;
            }
        }

        public static BallType BallTypeFromCode(int ballCode)
        {
            switch (ballCode)
            {
                case 1:
                    return BallType.BLUE;
                case 2:
                    return BallType.LIGHT_BLUE;
                case 3:
                    return BallType.GREEN;
                case 4:
                    return BallType.GREY;
                case 5:
                    return BallType.PURPLE;
                case 6:
                    return BallType.RED;
                default:
                    return BallType.NONE;
            }
        }
    }
}
