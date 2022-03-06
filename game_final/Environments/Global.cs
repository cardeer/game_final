using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace game_final.Environments
{
    static class Global
    {
        public static GameTime GameTime;

        public static MouseState CurrentMouseState;
        public static MouseState PreviousMouseState;

        public static Base.SceneRenderer CurrentScene;

        public static SpriteBatch SpriteBatch;
        public static GraphicsDevice Graphics;

        public static float Elapsed
        {
            get { return (float)GameTime.ElapsedGameTime.TotalSeconds; }
        }
    }
}
