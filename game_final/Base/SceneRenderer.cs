using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_final.Base
{
    abstract class SceneRenderer
    {
        private bool _ready = false;

        public bool IsReady
        {
            get { return _ready; }
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
        public abstract void Setup();
        public abstract void Update();
        public abstract void Draw();
        public virtual void Dispose()
        {
            _ready = false;
        }
    }
}
