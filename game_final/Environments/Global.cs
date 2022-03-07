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

        public static MouseState CurrentMouseState;
        public static MouseState PreviousMouseState;

        public static Base.SceneRenderer CurrentScene;
        public static Types.SceneType CurrentSceneType;

        public static ContentManager Content;
        public static SpriteBatch SpriteBatch;
        public static GraphicsDevice Graphics;

        public static float Elapsed
        {
            get { return (float)GameTime.ElapsedGameTime.TotalSeconds; }
        }

        public static void SetScene(Types.SceneType type)
        {
            if (CurrentScene != null)
            {
                CurrentScene.Dispose();
            }

            if (CurrentScene == null || CurrentSceneType != type)
            {
                CurrentSceneType = type;
                switch (type)
                {
                    case Types.SceneType.SPLASH:
                        CurrentScene = new Scenes.Splash();
                        break;
                    case Types.SceneType.MAIN_MENU:
                        CurrentScene = new Scenes.MainMenu();
                        break;
                    case Types.SceneType.IN_GAME:
                        CurrentScene = new Scenes.Playing();
                        break;
                }
            }
        }
    }
}
