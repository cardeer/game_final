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
        public bool Effect = true;
        public Color TextColor = Color.White;
        public bool MultipleClicks = false;

        private Texture2D _texture;

        private float _effectScale = 1.2f;
        private float _maxEffectScale = 1.5f;
        private bool _atMinScale = false;
        private int _effectDirection = 1;

        private SpriteFont _font;
        private string _text;
        private Vector2 _measureString;

        private bool _playedSound = false;

        private bool _justEntered = false;
        private bool _clicked = false;

        public Buttons(Texture2D texture, SpriteFont font, string text, int width, int height)
        {
            _texture = texture;

            _font = font;

            if (text != null)
            {
                _text = text;
                _measureString = _font.MeasureString(text);
            }

            _width = width;
            _height = height;

            SetOrigin(_width / 2, _height / 2);
        }

        public Buttons(Texture2D texture, SpriteFont font, int width, int height)
        {
            _texture = texture;

            _font = font;

            _width = width;
            _height = height;

            SetOrigin(_width / 2, _height / 2);
        }

        public void SetTexture(Texture2D texture)
        {
            _texture = texture;
        }

        public void Update()
        {
            if (IsHovering)
            {
                if (!_justEntered && !_clicked)
                {
                    Environments.Global.HoveringButton = true;
                    _justEntered = true;
                }

                if (!_playedSound)
                {
                    _playedSound = true;
                    AssetTypes.Sound.ButtonHover.Play(0.2f, 0, 0);
                }

                if (Effect)
                {
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
            }
            else
            {
                _playedSound = false;
                _atMinScale = false;
                _effectDirection = 1;
                _effectScale = _maxEffectScale;

                if (_justEntered)
                {
                    _justEntered = false;
                    Environments.Global.HoveringButton = false;
                }
            }

            if (IsClicked)
            {
                AssetTypes.Sound.ButtonClick.Play(0.2f, 0, 0);

                if (!MultipleClicks)
                {
                    Environments.Global.HoveringButton = false;
                    _clicked = true;
                }

                InvokeClick(this);
            }
        }

        public void Draw()
        {
            Rectangle hitBox = HitBox;

            Environments.Global.SpriteBatch.Draw(_texture, hitBox, Color.White);

            if (IsHovering && Effect)
            {
                int newWidth = (int)(_width * _effectScale);
                int newHeight = (int)(_height * _effectScale);

                Environments.Global.SpriteBatch.Draw(_texture, new Rectangle((int)(X - Origin.X + Width / 2 - newWidth / 2), (int)(Y - Origin.Y + Height / 2 - newHeight / 2), newWidth, newHeight), Color.Gray * 0.5f);
            }

            if (!string.IsNullOrEmpty(_text))
            {
                var x = (hitBox.X + (hitBox.Width / 2)) - (_measureString.X / 2);
                var y = (hitBox.Y + (hitBox.Height / 2)) - (_measureString.Y / 2);

                Environments.Global.SpriteBatch.DrawString(_font, _text, new Vector2(x, y), TextColor);
            }
        }
    }
}
