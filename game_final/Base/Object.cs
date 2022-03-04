using Microsoft.Xna.Framework;

namespace game_final.Base
{
    class Object
    {
        public Vector2 Position;
        public float Rotation = 0f;

        protected int _width, _height;

        public float X
        {
            get { return Position.X; }
            set { Position.X = value; }
        }

        public float Y
        {
            get { return Position.Y; }
            set { Position.Y = value; }
        }

        public int Width
        {
            get { return _width; }
        }

        public int Height
        {
            get { return _height; }
        }

        public void SetPosition(float x, float y)
        {
            Position.X = x;
            Position.Y = y;
        }
    }
}
