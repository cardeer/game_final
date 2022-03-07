using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace game_final.Sprites
{
    class MagicCircle : Base.Sprite
    {
        public float Angle = 0;
        public float MaxAngle = 180;

        public MagicCircle(Texture2D texture, int size) : base(texture, size)
        {
            SetOrigin(texture.Width / 2, texture.Height / 2);
            SetColor(Color.Pink, false);
        }

        public bool ShouldDestroy
        {
            get { return Angle >= MaxAngle; }
        }

        public void Update()
        {
            Angle += 180 * (float)Environments.Global.GameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw()
        {
            Environments.Global.SpriteBatch.Draw(
                Instance,
                Position,
                null,
                DrawColor * ((MaxAngle - Angle) / MaxAngle),
                Utils.Converter.DegressToRadians(Angle),
                Origin,
                Scale * ((MaxAngle - Angle) / MaxAngle * 2),
                SpriteEffects.None,
                0f
            );
        }
    }
}
