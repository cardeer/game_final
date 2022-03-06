﻿using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace game_final.Environments
{
    class GameData
    {
        public static int[,] BallsTemplate;
        public static List<Sprites.Ball> ShotBalls;
        public static bool CanShoot = true;

        public static void Initialize()
        {
            BallsTemplate = new int[Settings.TEMPLATE_ROW_BALLS, Settings.TEMPLATE_COL_BALLS] {
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            };
            ShotBalls = new List<Sprites.Ball>();
        }

        public static void SetBallTemplate(int row, int col, int ballTypeCode)
        {
            BallsTemplate[row, col] = ballTypeCode;

            bool failed = false;

            for (int i = 0; i < Settings.TEMPLATE_COL_BALLS; i++)
            {
                if (BallsTemplate[Settings.TEMPLATE_ROW_BALLS - 1, i] > 1)
                {
                    failed = true;
                    break;
                }
            }

            if (failed)
            {
                for (int i = 0; i < Settings.TEMPLATE_ROW_BALLS; i++)
                {
                    for (int j = 0; j < Settings.TEMPLATE_COL_BALLS; j++)
                    {
                        BallsTemplate[i, j] = 0;
                    }
                }
            }

            List<Types.Vector2Int> removePoints = new List<Types.Vector2Int>();
            removePoints.Add(new Types.Vector2Int(col, row));

            Queue<Types.Vector2Int> queue = new Queue<Types.Vector2Int>();
            queue.Enqueue(new Types.Vector2Int(col, row));

            while (queue.Count > 0)
            {
                Types.Vector2Int currentPoint = queue.Dequeue();
                int currentRow = currentPoint.Y;
                int currentCol = currentPoint.X;

                if (currentCol > 1 && BallsTemplate[currentRow, currentCol - 2] == ballTypeCode)
                {
                    Types.Vector2Int newPoint = new Types.Vector2Int(currentCol - 2, currentRow);
                    if (!contains(removePoints, newPoint))
                    {
                        removePoints.Add(newPoint);
                        queue.Enqueue(newPoint);
                    }
                }

                if (currentCol < Settings.TEMPLATE_COL_BALLS - 2 && BallsTemplate[currentRow, currentCol + 2] == ballTypeCode)
                {
                    Types.Vector2Int newPoint = new Types.Vector2Int(currentCol + 2, currentRow);
                    if (!contains(removePoints, newPoint))
                    {
                        removePoints.Add(newPoint);
                        queue.Enqueue(newPoint);
                    }
                }

                if (currentRow > 0 && currentCol > 0 && BallsTemplate[currentRow - 1, currentCol - 1] == ballTypeCode)
                {
                    Types.Vector2Int newPoint = new Types.Vector2Int(currentCol - 1, currentRow - 1);
                    if (!contains(removePoints, newPoint))
                    {
                        removePoints.Add(newPoint);
                        queue.Enqueue(newPoint);
                    }
                }

                if (currentRow > 0 && currentCol < Settings.TEMPLATE_COL_BALLS - 1 && BallsTemplate[currentRow - 1, currentCol + 1] == ballTypeCode)
                {
                    Types.Vector2Int newPoint = new Types.Vector2Int(currentCol + 1, currentRow - 1);
                    if (!contains(removePoints, newPoint))
                    {
                        removePoints.Add(newPoint);
                        queue.Enqueue(newPoint);
                    }
                }

                if (currentRow < Settings.TEMPLATE_ROW_BALLS - 1 && currentCol > 0 && BallsTemplate[currentRow + 1, currentCol - 1] == ballTypeCode)
                {
                    Types.Vector2Int newPoint = new Types.Vector2Int(currentCol - 1, currentRow + 1);
                    if (!contains(removePoints, newPoint))
                    {
                        removePoints.Add(newPoint);
                        queue.Enqueue(newPoint);
                    }
                }

                if (currentRow < Settings.TEMPLATE_ROW_BALLS - 1 && currentCol < Settings.TEMPLATE_COL_BALLS - 1 && BallsTemplate[currentRow + 1, currentCol + 1] == ballTypeCode)
                {
                    Types.Vector2Int newPoint = new Types.Vector2Int(currentCol + 1, currentRow + 1);
                    if (!contains(removePoints, newPoint))
                    {
                        removePoints.Add(newPoint);
                        queue.Enqueue(newPoint);
                    }
                }
            }

            if (removePoints.Count >= 3)
            {
                foreach (Types.Vector2Int point in removePoints)
                {
                    BallsTemplate[point.Y, point.X] = 0;
                }
            }

            CanShoot = true;
        }

        private static bool contains(List<Types.Vector2Int> list, Types.Vector2Int point)
        {
            return list.Exists(p => p.X == point.X && p.Y == point.Y);
        }
    }
}
