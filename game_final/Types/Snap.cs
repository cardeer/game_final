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

        private bool correctPosition()
        {
            if (Environments.GameData.BallsTemplate[SnapRow, SnapCol] > 0) return false;
            return (Environments.GameData.PushCount % 2 == 0 && SnapRow % 2 == SnapCol % 2) || (Environments.GameData.PushCount % 2 == 1 && SnapRow % 2 != SnapCol % 2);
        }

        public void FitPoints(Sprites.Ball ball)
        {
            //int[,] template = Environments.GameData.BallsTemplate;

            Debug.WriteLine($"snap from: {SnapRow}, {SnapCol} {ball.Unit.X}");
            Environments.GameData.PrintTemplate();

            int[,] template = Environments.GameData.BallsTemplate;

            Vector2 pos = Utils.Ball.GetPosFromIndex(ball.SnapPoint.Y, ball.SnapPoint.X);
            Vector2 right = Utils.Ball.GetPosFromIndex(SnapRow, SnapCol + 1);
            Vector2 left = Utils.Ball.GetPosFromIndex(SnapRow, SnapCol - 1);

            Debug.WriteLine($"pos: {pos.X}, left: {left.X}, right: {right.X}");

            while (template[SnapRow, SnapCol] > 0 || !correctPosition())
            {
                if (template[SnapRow, SnapCol] > 0)
                {
                    SnapRow++;
                    continue;
                }
                else if (SnapCol == Settings.TEMPLATE_COL_BALLS - 1 && template[SnapRow, SnapCol - 1] > 0)
                {
                    SnapRow++;
                    continue;
                }
                else if (SnapCol == 0 && template[SnapRow, SnapCol + 1] > 0)
                {
                    SnapRow++;
                    continue;
                }
                else if (SnapCol - 1 >= 0 && SnapCol + 1 < Settings.TEMPLATE_COL_BALLS && template[SnapRow, SnapCol - 1] > 0 && template[SnapRow, SnapCol + 1] > 0)
                {
                    SnapRow++;
                    continue;
                }

                if (ball.Unit.X > 0 && !correctPosition())
                {
                    if (template[SnapRow, SnapCol] > 0 && SnapCol - 1 >= 0) SnapCol--;

                    if (SnapCol - 1 >= 0 && template[SnapRow, SnapCol - 1] > 0)
                    {
                        if (SnapCol + 1 < Settings.TEMPLATE_COL_BALLS && template[SnapRow, SnapCol + 1] == 0)
                        {
                            SnapCol++;
                        }
                    }
                    else if (SnapCol + 1 < Settings.TEMPLATE_COL_BALLS && template[SnapRow, SnapCol + 1] > 0)
                    {
                        if (SnapCol - 1 >= 0 && template[SnapRow, SnapCol - 1] == 0)
                        {
                            SnapCol--;
                        }
                    }
                    else if (pos.X - left.X < right.X - pos.X)
                    {
                        if (SnapCol - 1 >= 0 && template[SnapRow, SnapCol - 1] == 0)
                        {
                            SnapCol--;
                        }
                        else if (SnapCol + 1 < Settings.TEMPLATE_COL_BALLS && template[SnapRow, SnapCol + 1] == 0)
                        {
                            SnapCol++;
                        }
                    }
                    else if (right.X - pos.X < pos.X - left.X)
                    {
                        if (SnapCol + 1 < Settings.TEMPLATE_COL_BALLS && template[SnapRow, SnapCol + 1] == 0)
                        {
                            SnapCol++;
                        }
                        else if (SnapCol - 1 >= 0 && template[SnapRow, SnapCol - 1] == 0)
                        {
                            SnapCol--;
                        }
                    }
                    else if (SnapCol == 0)
                    {
                        SnapCol++;
                    }
                    else if (SnapCol == Settings.TEMPLATE_COL_BALLS - 1)
                    {
                        SnapCol--;
                    }
                }
                else if (ball.Unit.X < 0 && !correctPosition())
                {
                    if (template[SnapRow, SnapCol] > 0 && SnapCol + 1 < Settings.TEMPLATE_COL_BALLS) SnapCol++;

                    if (SnapCol + 1 < Settings.TEMPLATE_COL_BALLS && template[SnapRow, SnapCol + 1] > 0)
                    {
                        if (SnapCol - 1 >= 0 && template[SnapRow, SnapCol - 1] == 0)
                        {
                            SnapCol--;
                        }
                    }
                    else if (SnapCol - 1 >= 0 && template[SnapRow, SnapCol - 1] > 0)
                    {
                        if (SnapCol + 1 < Settings.TEMPLATE_COL_BALLS && template[SnapRow, SnapCol + 1] == 0)
                        {
                            SnapCol++;
                        }
                    }
                    else if (pos.X - left.X < right.X - pos.X)
                    {
                        if (SnapCol - 1 >= 0 && template[SnapRow, SnapCol - 1] == 0)
                        {
                            SnapCol--;
                        }
                        else if (SnapCol + 1 < Settings.TEMPLATE_COL_BALLS && template[SnapRow, SnapCol + 1] == 0)
                        {
                            SnapCol++;
                        }
                    }
                    else if (right.X - pos.X < pos.X - left.X)
                    {
                        if (SnapCol + 1 < Settings.TEMPLATE_COL_BALLS && template[SnapRow, SnapCol + 1] == 0)
                        {
                            SnapCol++;
                        }
                        else if (SnapCol - 1 >= 0 && template[SnapRow, SnapCol - 1] == 0)
                        {
                            SnapCol--;
                        }
                    }
                    else if (SnapCol == 0)
                    {
                        SnapCol++;
                    }
                    else if (SnapCol == Settings.TEMPLATE_COL_BALLS - 1)
                    {
                        SnapCol--;
                    }
                }
            }

            //int currentPushState = Environments.GameData.PushCount % 2;

            //bool wrongCol = (currentPushState == 0 && SnapRow % 2 != SnapCol % 2) || (currentPushState == 1 && SnapRow % 2 == SnapCol % 2);

            //if (Environments.GameData.PushCount % 2 == 0)
            //{
            //    while (template[SnapRow, SnapCol] > 0 || (SnapRow % 2 != SnapCol % 2))
            //    {
            //        if (SnapCol > 0 && template[SnapRow, SnapCol - 1] == 0)
            //        {
            //            SnapCol -= 1;
            //            if (template[SnapRow, SnapCol] > 0)
            //            {
            //                SnapCol += 2;
            //            }
            //        }
            //        else if (SnapCol < Settings.TEMPLATE_COL_BALLS - 1 && template[SnapRow, SnapCol + 1] == 0)
            //        {
            //            SnapCol += 1;
            //            if (template[SnapRow, SnapCol] > 0)
            //            {
            //                SnapCol -= 2;
            //            }
            //        }
            //        else
            //        {
            //            SnapRow += 1;
            //        }
            //    }
            //}
            //else
            //{
            //    while (template[SnapRow, SnapCol] > 0 || (SnapRow % 2 == SnapCol % 2))
            //    {
            //        if (SnapCol > 0 && template[SnapRow, SnapCol - 1] == 0)
            //        {
            //            SnapCol -= 1;
            //            if (template[SnapRow, SnapCol] > 0)
            //            {
            //                SnapCol += 2;
            //            }
            //        }
            //        else if (SnapCol < Settings.TEMPLATE_COL_BALLS - 1 && template[SnapRow, SnapCol + 1] == 0)
            //        {
            //            SnapCol += 1;
            //            if (template[SnapRow, SnapCol] > 0)
            //            {
            //                SnapCol -= 2;
            //            }
            //        }
            //        else
            //        {
            //            SnapRow += 1;
            //        }
            //    }
            //}

            Debug.WriteLine($"snap to: {SnapRow}, {SnapCol}");
        }
    }
}
