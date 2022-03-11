using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace game_final.Environments
{
    class GameData
    {
        public static bool CanShoot = true;
        public static int Score = 0;
        public static int Combo = 0;
        public static bool Failed = false;
        public static bool Won = false;
        public static int PushCount = 0;
        public static int ShootCount = 0;
        public static bool DataReady = true;

        public static int Level = 1;

        public static int[,] BallsTemplate;
        public static List<Sprites.MagicCircle> MagicCircles;
        public static List<Sprites.Ball> BallsToDrop;

        public static bool ChallengeMode = false;

        // time in seconds
        public static float TimeLeft = 5 * 60;

        private static int _scoreMultiply = 0;

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
            BallsToDrop = new List<Sprites.Ball>();

            CanShoot = true;
            Score = 0;
            Combo = 0;
            Failed = false;
            Won = false;
            PushCount = 0;
            ShootCount = 0;
            DataReady = true;

            TimeLeft = Level == 1 ? 120 : Level == 2 ? 90 : 60;
            _scoreMultiply = (Level == 1 ? 100 : Level == 2 ? 200 : 300) * (ChallengeMode ? 2 : 1);
        }

        public static void SetBallTemplate(int row, int col, int ballTypeCode)
        {
            DataReady = false;

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

                Combo++;
                Score += removePoints.Count * _scoreMultiply * Combo;

                dropBalls();

                checkWin();
                if (Won) return;
            }
            else
            {
                AssetTypes.Sound.BallSnap.Play(0.5f, 0, 0);
                Combo = 0;
            }


            checkFailed();

            if (Failed) return;

            ShootCount++;

            if (ShootCount >= MaxShoot)
            {
                double total = 0;
                while (total <= 1) total += Environments.Global.GameTime.ElapsedGameTime.TotalSeconds;

                ShootCount = 0;
                PushFromTop();
            }

            DataReady = true;
        }

        public static int MaxShoot
        {
            get { return Level == 1 ? 10 : Level == 2 ? 6 : 4; }
        }

        private static bool contains(List<Types.Vector2Int> list, Types.Vector2Int point)
        {
            return list.Exists(p => p.X == point.X && p.Y == point.Y);
        }

        public static void GenerateLevel()
        {
            for (int i = 0; i < (Level != 3 ? 5 : 4); i++)
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
            AssetTypes.Sound.CeilingDown.Play();

            int[,] newTemplate = new int[Settings.TEMPLATE_ROW_BALLS, Settings.TEMPLATE_COL_BALLS];

            PushCount++;

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
                MediaPlayer.Stop();
                Utils.Sound.PlayLoseSound();
                Failed = failed;
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

            if (!hasBall)
            {
                MediaPlayer.Stop();
                Utils.Sound.PlayWinSound();
                Won = true;

                if (ChallengeMode)
                {
                    Score *= (int)Math.Ceiling(TimeLeft);
                }
            }
        }

        public static void PrintTemplate()
        {
            string result = "  ";
            for (int i = 0; i < Settings.TEMPLATE_COL_BALLS; i++)
            {
                if (i < 10)
                {
                    result += i + "  ";
                }
                else
                {
                    result += i + " ";
                }
            }
            result += "\n";

            for (int i = 0; i < Settings.TEMPLATE_ROW_BALLS; i++)
            {
                result += i + " ";
                for (int j = 0; j < Settings.TEMPLATE_COL_BALLS; j++)
                {
                    result += BallsTemplate[i, j] + ", ";
                }
                result += "\n";
            }
            Debug.WriteLine(result);
        }

        private static void dropBalls()
        {
            int[,] stickBalls = new int[Settings.TEMPLATE_ROW_BALLS, Settings.TEMPLATE_COL_BALLS];

            Queue<Types.Vector2Int> toCheck = new Queue<Types.Vector2Int>();

            for (int i = 0; i < Settings.TEMPLATE_COL_BALLS; i++)
            {
                if (BallsTemplate[PushCount, i] > 0) toCheck.Enqueue(new Types.Vector2Int(i, PushCount));
            }

            while (toCheck.Count > 0)
            {
                Types.Vector2Int point = toCheck.Dequeue();

                int row = point.Y;
                int col = point.X;

                if (stickBalls[row, col] != 1)
                {
                    stickBalls[row, col] = 1;

                    //Debug.WriteLine($"({point.Y}, {point.X})");

                    // top left
                    if (row > 0 && col > 0 && BallsTemplate[row - 1, col - 1] > 0)
                    {
                        toCheck.Enqueue(new Types.Vector2Int(col - 1, row - 1));
                    }

                    // top right
                    if (row > 0 && col + 1 < Settings.TEMPLATE_COL_BALLS && BallsTemplate[row - 1, col + 1] > 0)
                    {
                        toCheck.Enqueue(new Types.Vector2Int(col + 1, row - 1));
                    }

                    // left
                    if (col - 2 >= 0 && BallsTemplate[row, col - 2] > 0)
                    {
                        toCheck.Enqueue(new Types.Vector2Int(col - 2, row));
                    }

                    // right
                    if (col + 2 < Settings.TEMPLATE_COL_BALLS && BallsTemplate[row, col + 2] > 0)
                    {
                        toCheck.Enqueue(new Types.Vector2Int(col + 2, row));
                    }

                    // bottom left
                    if (row + 1 < Settings.TEMPLATE_ROW_BALLS && col > 0 && BallsTemplate[row + 1, col - 1] > 0)
                    {
                        toCheck.Enqueue(new Types.Vector2Int(col - 1, row + 1));
                    }

                    // bottom right
                    if (row + 1 < Settings.TEMPLATE_ROW_BALLS && col + 1 < Settings.TEMPLATE_COL_BALLS && BallsTemplate[row + 1, col + 1] > 0)
                    {
                        toCheck.Enqueue(new Types.Vector2Int(col + 1, row + 1));
                    }
                }
            }

            int dropCount = 0;

            for (int i = PushCount; i < Settings.TEMPLATE_ROW_BALLS; i++)
            {
                for (int j = 0; j < Settings.TEMPLATE_COL_BALLS; j++)
                {
                    // if (i, j) is a stick ball
                    if (stickBalls[i, j] == 1 || BallsTemplate[i, j] == 0) continue;

                    bool stick = false;

                    // top left
                    if (i > 0 && j > 0 && stickBalls[i - 1, j - 1] == 1)
                    {
                        stick = true;
                    }

                    // top right
                    if (i > 0 && j + 1 < Settings.TEMPLATE_COL_BALLS && stickBalls[i - 1, j + 1] == 1)
                    {
                        stick = true;
                    }

                    // left
                    if (j - 2 >= 0 && stickBalls[i, j - 2] == 1)
                    {
                        stick = true;
                    }

                    // right
                    if (j + 2 < Settings.TEMPLATE_COL_BALLS && stickBalls[i, j + 2] == 1)
                    {
                        stick = true;
                    }

                    // bottom left
                    if (i + 1 < Settings.TEMPLATE_ROW_BALLS && j > 0 && stickBalls[i + 1, j - 1] == 1)
                    {
                        stick = true;
                    }

                    // bottom right
                    if (i + 1 < Settings.TEMPLATE_ROW_BALLS && j + 1 < Settings.TEMPLATE_COL_BALLS && stickBalls[i + 1, j + 1] == 1)
                    {
                        stick = true;
                    }

                    if (!stick)
                    {
                        Vector2 pos = Utils.Ball.GetRenderPosition(i, j);
                        Sprites.Ball ball = new Sprites.Ball(Types.Ball.BallTypeFromCode(BallsTemplate[i, j]), (int)pos.X, (int)pos.Y, true);
                        ball.StartY = (int)pos.Y;
                        BallsToDrop.Add(ball);

                        BallsTemplate[i, j] = 0;
                        dropCount++;
                    }
                }
            }

            Score += dropCount * _scoreMultiply * Combo;
        }

        public static bool IsEndGame()
        {
            return Won || Failed;
        }
    }
}
