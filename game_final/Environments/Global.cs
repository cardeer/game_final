using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace game_final.Environments
{
    static class Global
    {
        public static GameTime GameTime;

        public static int Level = 2;

        public static MouseState CurrentMouseState;
        public static MouseState PreviousMouseState;

        public static ContentManager Content;
        public static SpriteBatch SpriteBatch;
        public static GraphicsDevice Graphics;

        public static float Elapsed
        {
            get { return (float)GameTime.ElapsedGameTime.TotalSeconds; }
        }
    }
}
