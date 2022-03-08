using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace game_final.Environments
{
    class GameData
    {
        public static bool CanShoot = true;
        public static int Score = 0;
        public static bool Failed = false;
        public static bool Won = false;
        public static int PushCount = 0;
        public static int ShootCount = 0;

        public static int[,] BallsTemplate;
        public static List<Sprites.MagicCircle> MagicCircles;

        public static void Initialize()
        {
            BallsTemplate = new int[Settings.TEMPLATE_ROW_BALLS, Settings.TEMPLATE_COL_BALLS];
            for (int i = 0; i < Settings.TEMPLATE_ROW_BALLS; i++)
            {
                for (int j = 0; j < Settings.TEMPLATE_COL_BALLS; j++)
                {
                    BallsTemplate[i, j] = 0;
                }
            }

            MagicCircles = new List<Sprites.MagicCircle>();

            CanShoot = true;
            Score = 0;
            Failed = false;
            Won = false;
            PushCount = 0;
            ShootCount = 0;
        }

        public static void SetBallTemplate(int row, int col, int ballTypeCode)
        {
            BallsTemplate[row, col] = ballTypeCode;

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
                AssetTypes.Sound.BallPop.Play(.3f, 0, 0);

                foreach (Types.Vector2Int point in removePoints)
                {
                    BallsTemplate[point.Y, point.X] = 0;
                    Sprites.MagicCircle magicCircle = new Sprites.MagicCircle(AssetTypes.Texture.MagicCircle, Settings.BALL_SIZE);
                    Vector2 pos = Utils.Ball.GetRenderPosition(point.Y, point.X);
                    magicCircle.SetPosition(pos.X, pos.Y);

                    MagicCircles.Add(magicCircle);
                }

                checkWin();
                if (Won) return;
            }


            checkFailed();

            if (Failed) return;

            ShootCount++;

            if (ShootCount >= 10)
            {
                double total = 0;
                while (total <= 0.3) total += Environments.Global.GameTime.ElapsedGameTime.TotalSeconds;

                ShootCount = 0;
                PushFromTop();
            }
        }

        private static bool contains(List<Types.Vector2Int> list, Types.Vector2Int point)
        {
            return list.Exists(p => p.X == point.X && p.Y == point.Y);
        }

        public static void GenerateLevel()
        {
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < Settings.TEMPLATE_COL_BALLS; j++)
                {
                    if (i % 2 == j % 2)
                    {
                        BallsTemplate[i, j] = Utils.Ball.RandomBallCode(true);
                    }
                }
            }

            Debug.WriteLine("level generated");
        }

        public static void PushFromTop()
        {
            int[,] newTemplate = new int[Settings.TEMPLATE_ROW_BALLS, Settings.TEMPLATE_COL_BALLS];

            PushCount++;

            //for (int i = 0; i < Settings.TEMPLATE_ROW_BALLS; i++)
            //{
            //    if (i == 0)
            //    {
            //        for (int j = 0; j < Settings.TEMPLATE_COL_BALLS; j++)
            //        {
            //            if (j % 2 == PushCount % 2)
            //            {
            //                newTemplate[i, j] = Utils.Ball.RandomBallCode();
            //            }
            //        }
            //    }
            //    else
            //    {
            //        for (int j = 0; j < Settings.TEMPLATE_COL_BALLS; j++)
            //        {
            //            newTemplate[i, j] = BallsTemplate[i - 1, j];
            //        }
            //    }
            //}

            for (int i = PushCount; i < Settings.TEMPLATE_ROW_BALLS; i++)
            {
                for (int j = 0; j < Settings.TEMPLATE_COL_BALLS; j++)
                {
                    newTemplate[i, j] = BallsTemplate[i - 1, j];
                }
            }

            BallsTemplate = newTemplate;

            checkFailed();
        }

        private static void checkFailed()
        {
            bool failed = false;

            for (int i = 0; i < Settings.TEMPLATE_COL_BALLS; i++)
            {
                if (BallsTemplate[Settings.TEMPLATE_ROW_BALLS - 2, i] > 0)
                {
                    failed = true;
                    break;
                }
            }

            if (failed)
            {
                Failed = failed;

                PushCount = 0;
                ShootCount = 0;

                //for (int i = 0; i < Settings.TEMPLATE_ROW_BALLS; i++)
                //{
                //    for (int j = 0; j < Settings.TEMPLATE_COL_BALLS; j++)
                //    {
                //        BallsTemplate[i, j] = 0;
                //    }
                //}
            }
        }

        private static void checkWin()
        {
            bool hasBall = false;

            for (int i = 0; i < Settings.TEMPLATE_ROW_BALLS; i++)
            {
                for (int j = 0; j < Settings.TEMPLATE_COL_BALLS; j++)
                {
                    if (BallsTemplate[i, j] > 0)
                    {
                        hasBall = true;
                        break;
                    }
                }
            }

            if (!hasBall) Won = true;
        }

        public static void PrintTemplate()
        {
            string result = "";
            for (int i = 0; i < Settings.TEMPLATE_ROW_BALLS; i++)
            {
                for (int j = 0; j < Settings.TEMPLATE_COL_BALLS; j++)
                {
                    result += BallsTemplate[i, j] + ", ";
                }
                result += "\n";
            }
            Debug.WriteLine(result);
        }
    }
}
