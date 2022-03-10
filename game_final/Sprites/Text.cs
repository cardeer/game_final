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

            _measure = _font.MeasureString(text);
        }
    }
}
