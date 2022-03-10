using System;
using System.Collections.Generic;
using System.Text;

namespace game_final.Types
{
    class Vector2Int
    {
        public int X;
        public int Y;

        public Vector2Int(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"Vector2Int{{X: {X}, Y: {Y}}}";
        }
    }
}
