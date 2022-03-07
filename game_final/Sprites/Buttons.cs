using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace game_final.Sprites
{
	class Buttons : Types.Component
	{
		private Texture2D _buttonTex;

		private SpriteFont _font;

		private bool _isHovering;

		private MouseState _currentMouse;
		private MouseState _previousMouse;

		public event EventHandler Click;

		public bool Clicked { get; private set; }

		public Color PenColor { get; set; }

		public Vector2 Position { get; set; }

		public Rectangle Rectangle
		{
			get 
			{
				return new Rectangle((int)Position.X, (int)Position.Y, _buttonTex.Width / 2, _buttonTex.Height / 2);
			}
		}

		public string Text { get; set; }

		public Buttons(Texture2D texture, SpriteFont font)
		{
			_buttonTex = texture;
			_font = font;
			PenColor = Color.Black;
		}

		public override void Draw()
		{
			var color = Color.White;

			if (_isHovering)
			{
				color = Color.Gray;
			}
			
			Environments.Global.SpriteBatch.Draw(_buttonTex, Rectangle, color);

			if (!string.IsNullOrEmpty(Text))
			{
				var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
				var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);

				Environments.Global.SpriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColor);
			}
		}

		public override void Update()
		{
			_previousMouse = _currentMouse;
			_currentMouse = Mouse.GetState();

			var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);
			_isHovering = false;

			if (mouseRectangle.Intersects(Rectangle))
			{
				_isHovering = true;

				if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
				{
					Click?.Invoke(this, new EventArgs());
				}
			}
		}
	}
}
