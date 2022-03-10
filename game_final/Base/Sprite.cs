using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace game_final.Base
{
    class Sprite : Object
    {
        protected Color[] _colorArray;
        protected Color _color = Color.Transparent;
        public Sprite(int width, int height)
        {
            _width = width;
            _height = height;

            Instance = new Texture2D(Environments.Global.Graphics, width, height);

            _colorArray = new Color[Instance.Width * Instance.Height];
        }

        public Sprite(Texture2D texture, int size)
        {
            Instance = texture;

            Scale = (float)size / texture.Width;

            _width = size;
            _height = (int)(texture.Height * Scale);

            _colorArray = new Color[Instance.Width * Instance.Height];
        }

        public Sprite(Texture2D texture)
        {
            Instance = texture;

            _width = texture.Width;
            _height = texture.Height;

            _colorArray = new Color[Instance.Width * Instance.Height];
        }

        public Color Color
        {
            get { return _color; }
        }

        public Color DrawColor {
            get { return _color != Color.Transparent ? _color : Color.White; }
        }

        public void SetColor(Color color, bool reTexture = true)
        {
            _color = color;

            if (reTexture)
            {
                for (int i = 0; i < _colorArray.Length; i++)
                {
                    _colorArray[i] = color;
                }

                Instance.SetData(_colorArray);
            }
        }

        public void Draw()
        {
            Environments.Global.SpriteBatch.Draw(
                Instance,
                Position,
                null,
                DrawColor,
                Rotation,
                Origin,
                Scale,
                SpriteEffects.None,
                LayerDepth
            );
        }
    }
}
