using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_final.Scenes
{
    class Splash : Base.SceneRenderer
    {
        private Texture2D _logo;
        private Vector2 _logoPosition;

        private float _opacity = 0f;
        private bool _isFadingOut = false;
        private float _waitTime = 0;

        public override void LoadContent()
        {
            AssetTypes.Texture.Logo = Environments.Global.Content.Load<Texture2D>("logo");
        }

        public override void Setup()
        {
            _logo = AssetTypes.Texture.Logo;

            _logoPosition = new Vector2(Settings.WINDOW_WIDTH / 2 - _logo.Width / 2, Settings.WINDOW_HEIGHT / 2 - _logo.Height / 2);
        }

        public override void Update()
        {
            if (!_isFadingOut && _waitTime < 1)
            {
                _waitTime += (float)Environments.Global.GameTime.ElapsedGameTime.TotalSeconds;
            }
            if (!_isFadingOut && _waitTime >= 1)
            {
                _opacity += (float)Environments.Global.GameTime.ElapsedGameTime.TotalSeconds * 1;

                if (_opacity >= 1)
                {
                    _isFadingOut = true;
                    _waitTime = 0;
                }
            }

            if (_isFadingOut && _waitTime < 1)
            {
                _waitTime += (float)Environments.Global.GameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (_isFadingOut && _waitTime >= 1)
            {
                _opacity -= (float)Environments.Global.GameTime.ElapsedGameTime.TotalSeconds * 1;

                if (_opacity <= -0.3)
                {
                    Environments.Scene.SetScene(Types.SceneType.MAIN_MENU);
                }
            }
        }

        public override void Draw()
        {
            Environments.Global.SpriteBatch.Draw(_logo, _logoPosition, null, Color.White * _opacity, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
