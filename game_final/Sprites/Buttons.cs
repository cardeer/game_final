using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using System.Diagnostics;

namespace game_final.Sprites
{
	class Buttons : Base.Object
	{
		private Texture2D _buttonTex;

		private SpriteFont _font;

		public Color PenColor { get; set; }

		public string Text { get; set; }

		private bool _isHovering = false;

		public Buttons(Texture2D texture, SpriteFont font)
		{
			_buttonTex = texture;

			_width = texture.Width / 2;
			_height = texture.Height / 2;

			_font = font;
			PenColor = Color.Black;
		}

		public void Draw()
		{
			Color color = Color.White;

			Rectangle hitBox = HitBox;

			if (IsHovering)
			{
				color = Color.Gray;
			}
			
			Environments.Global.SpriteBatch.Draw(_buttonTex, hitBox, color);

			if (!string.IsNullOrEmpty(Text))
			{
				var x = (hitBox.X + (hitBox.Width / 2)) - (_font.MeasureString(Text).X / 2);
				var y = (hitBox.Y + (hitBox.Height / 2)) - (_font.MeasureString(Text).Y / 2);

				Environments.Global.SpriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColor);
			}
		}

		public void Update()
		{
			_isHovering = IsHovering;

			if (IsClicked)
			{
				InvokeClick(this);
			}
		}
	}
}
