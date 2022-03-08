using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using System.Diagnostics;

namespace game_final.Sprites
{
	class MenuButtons : Base.Object
	{
		private Texture2D _buttonTex;
		private Texture2D _buttonHoverEffect;

		private float _effectScale = 1.2f;
		private float _maxEffectScale = 1.5f;
		private bool _atMinScale = false;
		private int _effectDirection = 1;

		private SpriteFont _font;
		private string _text;
		private Vector2 _measureString;

		private bool _playedSound = false;

		public MenuButtons(Texture2D texture, SpriteFont font, string text, int width, int height)
		{
			_buttonTex = texture;

			_font = font;
			
			if (text != null)
            {
				_text = text;
				_measureString = _font.MeasureString(text);
			}

			_width = width;
			_height = height;

			SetOrigin(_width / 2, _height / 2);

			_buttonHoverEffect = texture;
		}

		public void Draw()
		{
			Rectangle hitBox = HitBox;
			
			Environments.Global.SpriteBatch.Draw(_buttonTex, hitBox, Color.White);

			if (IsHovering)
			{
				if (!_playedSound)
                {
					_playedSound = true;
					AssetTypes.Sound.ButtonHover.Play(0.2f, 0, 0);
				}

				int newWidth = (int)(_width * _effectScale);
				int newHeight = (int)(_height * _effectScale);
				Environments.Global.SpriteBatch.Draw(_buttonHoverEffect, new Rectangle((int)(X - newWidth / 2), (int)(Y - newHeight / 2), newWidth, newHeight), Color.Gray * 0.5f);

				if (!_atMinScale)
                {
					_effectScale -= 3f * (float)Environments.Global.GameTime.ElapsedGameTime.TotalSeconds;

					if (_effectScale < 1)
					{
						_atMinScale = true;
					}
				}
				else
                {
					_effectScale += 1f * (float)Environments.Global.GameTime.ElapsedGameTime.TotalSeconds * _effectDirection;

					if (_effectScale > 1.2 || _effectScale < 1)
                    {
						_effectDirection *= -1;
					}
				}
			}
			else
            {
				_playedSound = false;
				_atMinScale = false;
				_effectDirection = 1;
				_effectScale = _maxEffectScale;
            }

			if (!string.IsNullOrEmpty(_text))
            {
				var x = (hitBox.X + (hitBox.Width / 2)) - (_measureString.X / 2);
				var y = (hitBox.Y + (hitBox.Height / 2)) - (_measureString.Y / 2);

				Environments.Global.SpriteBatch.DrawString(_font, _text, new Vector2(x, y), Color.White);
			}
		}

		public void Update()
		{
			if (IsClicked)
			{
				AssetTypes.Sound.ButtonClick.Play(0.2f, 0, 0);
				InvokeClick(this);
			}
		}
	}
}
