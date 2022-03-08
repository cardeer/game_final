using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework.Graphics;

namespace game_final.Types
{
    enum BallType
    {
        NONE,
        BLUE,
        BROWN,
        GREEN,
        GREY,
        LIGHT_BLUE,
        PURPLE,
        RED,
        YELLOW,
    }

    static class Ball
    {
        public static int TotalTypes = 8;
        public static Texture2D GetBallTexture(BallType ballType)
        {
            switch (ballType)
            {
                case BallType.BLUE:
                    return AssetTypes.Texture.BlueBall;
                case BallType.BROWN:
                    return AssetTypes.Texture.BrownBall;
                case BallType.GREEN:
                    return AssetTypes.Texture.GreenBall;
                case BallType.GREY:
                    return AssetTypes.Texture.GreyBall;
                case BallType.LIGHT_BLUE:
                    return AssetTypes.Texture.LightBlueBall;
                case BallType.PURPLE:
                    return AssetTypes.Texture.PurpleBall;
                case BallType.RED:
                    return AssetTypes.Texture.RedBall;
                case BallType.YELLOW:
                    return AssetTypes.Texture.YellowBall;
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
                case BallType.BROWN:
                    return 2;
                case BallType.GREEN:
                    return 3;
                case BallType.GREY:
                    return 4;
                case BallType.LIGHT_BLUE:
                    return 5;
                case BallType.PURPLE:
                    return 6;
                case BallType.RED:
                    return 7;
                case BallType.YELLOW:
                    return 8;
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
                    return BallType.BROWN;
                case 3:
                    return BallType.GREEN;
                case 4:
                    return BallType.GREY;
                case 5:
                    return BallType.LIGHT_BLUE;
                case 6:
                    return BallType.PURPLE;
                case 7:
                    return BallType.RED;
                case 8:
                    return BallType.YELLOW;
                default:
                    return BallType.NONE;
            }
        }
    }
}
