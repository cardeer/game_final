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

        private float _opacity = 1f;

        public Splash()
        {
            _logo = AssetTypes.Texture.Logo;

            _logoPosition = new Vector2(Settings.WINDOW_WIDTH / 2 - _logo.Width / 2, Settings.WINDOW_HEIGHT / 2 - _logo.Height / 2);
        }

        public override void Update()
        {
            if (Environments.Global.GameTime.TotalGameTime.TotalSeconds > 1.5)
            {
                _opacity -= (float)Environments.Global.GameTime.ElapsedGameTime.TotalSeconds * 1;

                if (_opacity <= -0.3)
                {
                    Environments.Global.SetScene(Types.SceneType.MAIN_MENU);
                }
            }
        }

        public override void Draw()
        {
            Environments.Global.SpriteBatch.Draw(_logo, _logoPosition, null, Color.White * _opacity, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
