using System;
using System.Collections.Generic;
using System.Text;

namespace game_final.Utils
{
    static class Sound
    {
        public static void PlayStartSound()
        {
            int code = new Random().Next(1, 3);

            switch (code)
            {
                case 1:
                    AssetTypes.Sound.Play1.Play(1f, 0, 0);
                    break;
                case 2:
                    AssetTypes.Sound.Play2.Play(1f, 0, 0);
                    break;
                default:
                    AssetTypes.Sound.Play1.Play();
                    break;
            }
        }

        public static void PlayWinSound()
        {
            int code = new Random().Next(1, 3);

            switch (code)
            {
                case 1:
                    AssetTypes.Sound.Win1.Play(1f, 0, 0);
                    break;
                case 2:
                    AssetTypes.Sound.Win2.Play(1f, 0, 0);
                    break;
                default:
                    AssetTypes.Sound.Win1.Play();
                    break;
            }
        }

        public static void PlayLoseSound()
        {
            int code = new Random().Next(1, 5);

            switch (code)
            {
                case 1:
                    AssetTypes.Sound.Lose1.Play(1f, 0, 0);
                    break;
                case 2:
                    AssetTypes.Sound.Lose2.Play(1f, 0, 0);
                    break;
                case 3:
                    AssetTypes.Sound.Lose3.Play(1f, 0, 0);
                    break;
                case 4:
                    AssetTypes.Sound.Lose4.Play(1f, 0, 0);
                    break;
                default:
                    AssetTypes.Sound.Lose4.Play();
                    break;
            }
        }
    }
}
