using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace game_final
{
    class Sprite
    {
        protected Texture2D _sprite;
        private Vector2 _position;
        protected int _width, _height;
        protected Color[] _color;

        public float Rotation = 0f;

        protected Sprite(GraphicsDevice graphics, int width, int height) {
            _position = new Vector2(0, 0);
            _width = width;
            _height = height;

            _sprite = new Texture2D(graphics, width, height);

            _color = new Color[width * height];
        }

        public Texture2D Instance {
            get { return _sprite; }
            set { _sprite = value; }
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public float X
        {
            get { return _position.X; }
            set { _position.X = value; }
        }

        public float Y {
            get { return _position.Y; }
            set { _position.Y = value; }
        }

        public Vector2 Center {
            get { return new Vector2(_width / 2, _height / 2); }
        }

        public void SetColor(Color color)
        {
            for (int i = 0; i < _color.Length; i++) {
                _color[i] = color;
            }

            _sprite.SetData(_color);
        }

        public void SetPosition(float x, float y)
        {
            _position.X = x;
            _position.Y = y;
        }
    }
}
