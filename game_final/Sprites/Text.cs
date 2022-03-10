using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_final.Sprites
{
    class Text : Base.Object
    {
        public Color Color = Color.Black;

        private SpriteFont _font;
        private string _text;

        private Vector2 _measure;

        public Text(SpriteFont font, string text)
        {
            _font = font;
            _text = text;

            measureString();
        }

        public void SetText(string text)
        {
            _text = text;
            measureString();
        }

        private void measureString()
        {
            _measure = _font.MeasureString(_text);

            _width = (int)_measure.X;
            _height = (int)_measure.Y;
        }

        public void Draw()
        {
            Environments.Global.SpriteBatch.DrawString(
                _font,
                _text,
                Position,
                Color,
                Rotation,
                Origin,
                Scale,
                SpriteEffects.None,
                LayerDepth
            );
        }
    }
}
