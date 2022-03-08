using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Diagnostics;

namespace game_final.Base
{
    abstract class SceneRenderer
    {
        private bool _ready = false;
        private bool _disposing = false;

        private float _opacity = 0f;
        private Shapes.Rectangle _fadeRect;

        private Types.SceneType _nextScene;

        private bool _fadeIn = false;
        private bool _fadingIn = true;

        public bool IsReady
        {
            get { return _ready; }
        }

        public SceneRenderer() { }

        public SceneRenderer(bool fadeIn)
        {
            _fadeIn = fadeIn;
            _opacity = 1f;
        }

        protected void DrawSprite(Base.Sprite sprite)
        {
            Environments.Global.SpriteBatch.Draw(
                sprite.Instance,
                sprite.Position,
                null,
                sprite.DrawColor,
                sprite.Rotation,
                sprite.Origin,
                sprite.Scale,
                SpriteEffects.None,
                0f
            );
        }
        public void Ready()
        {
            _ready = true;
        }

        public abstract void LoadContent();

        public virtual void Setup()
        {
            _fadeRect = new Shapes.Rectangle(Settings.WINDOW_WIDTH, Settings.WINDOW_HEIGHT);
            _fadeRect.SetColor(Color.Black);
            _fadeRect.SetPosition(0, 0);
        }

        public virtual void Update()
        {
            if (_fadeIn && _fadingIn)
            {
                _opacity -= (float)Environments.Global.GameTime.ElapsedGameTime.TotalSeconds * 1;
                
                if (_opacity <= 0)
                {
                    _opacity = 0f;
                    _fadingIn = false;
                }
            }

            if (_disposing)
            {
                _opacity += (float)Environments.Global.GameTime.ElapsedGameTime.TotalSeconds * 1;

                if (_opacity >= 1)
                {
                    Environments.Scene.SetScene(_nextScene);
                }
            }
        }

        public virtual void Draw()
        {
            if (_fadeRect != null)
                Environments.Global.SpriteBatch.Draw(_fadeRect.Instance, _fadeRect.Position, null, _fadeRect.Color * _opacity, _fadeRect.Rotation, _fadeRect.Origin, _fadeRect.Scale, SpriteEffects.None, 0f);
        }

        public virtual void Dispose(Types.SceneType nextScene, bool fadeOut)
        {
            _disposing = true;
            _nextScene = nextScene;

            if (!fadeOut)
                _ready = false;
        }
    }
}
