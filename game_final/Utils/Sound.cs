using System;
using System.Collections.Generic;
using System.Text;

namespace game_final.Utils
{
    static class Sound
    {
        static void PlayStartSound()
        {
            int code = new Random().Next(1, 4);

            switch (code)
            {
                case 1:
                    AssetTypes.Sound.Play1.Play();
                    break;
                case 2:
                    AssetTypes.Sound.Play2.Play();
                    break;
                case 3:
                    AssetTypes.Sound.Play2.Play();
                    break;
            }
        }

        static void PlayWinSound()
        {
            int code = new Random().Next(1, 4);

            switch (code)
            {
                case 1:
                    AssetTypes.Sound.Win1.Play();
                    break;
                case 2:
                    AssetTypes.Sound.Win2.Play();
                    break;
                case 3:
                    AssetTypes.Sound.Win3.Play();
                    break;
            }
        }

        static void PlayLoseSound()
        {
            int code = new Random().Next(1, 4);

            switch (code)
            {
                case 1:
                    AssetTypes.Sound.Lose1.Play();
                    break;
                case 2:
                    AssetTypes.Sound.Lose2.Play();
                    break;
                case 3:
                    AssetTypes.Sound.Lose3.Play();
                    break;
            }
        }
    }
}
