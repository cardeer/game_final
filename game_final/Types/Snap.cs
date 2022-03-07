using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace game_final.Types
{
    class Snap
    {
        public bool ShouldSnap = false;
        public int SnapRow;
        public int SnapCol;

        public Snap()
        {
        }

        public Snap(bool shouldSnap, int snapRow, int snapCol)
        {
            ShouldSnap = shouldSnap;
            SnapRow = snapRow;
            SnapCol = snapCol;
        }

        public void FitPoints()
        {
            int[,] template = Environments.GameData.BallsTemplate;

            //Debug.WriteLine($"{SnapRow}, {SnapCol}");

            while (template[SnapRow, SnapCol] > 0 || (SnapRow % 2 == 0 && SnapCol % 2 == 1) || (SnapRow % 2 == 1 && SnapCol % 2 == 0))
            {
                if (SnapCol > 0 && template[SnapRow, SnapCol - 1] == 0)
                {
                    SnapCol -= 1;
                }
                else if (SnapCol < Settings.TEMPLATE_COL_BALLS - 1 && template[SnapRow, SnapCol + 1] == 0)
                {
                    SnapCol += 1;
                }
                else if (SnapRow > 0 && template[SnapRow - 1, SnapCol] == 0)
                {
                    SnapRow -= 1;
                }
                else
                {
                    SnapRow += 1;
                }
            }

            //Debug.WriteLine($"{SnapRow}, {SnapCol}");
        }
    }
}
