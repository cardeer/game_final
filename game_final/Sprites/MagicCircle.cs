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
        public float MaxAngle = 90;

        public MagicCircle(int width, int height) : base(width, height)
        {
            SetOrigin(width / 2, height / 2);
            SetColor(Color.Red);
        }

        public bool ShouldDestroy
        {
            get { return Angle >= 90; }
        }

        public void Update()
        {
            Angle += 3;
        }

        public void Draw()
        {
            Environments.Global.SpriteBatch.Draw(
                Instance,
                Position,
                null,
                DrawColor * ((MaxAngle - Angle) / 90),
                Utils.Converter.DegressToRadians(Angle),
                Origin,
                Scale,
                SpriteEffects.None,
                0f
            );
        }
    }
}
