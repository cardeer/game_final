using System;
using System.Collections.Generic;
using System.Text;

namespace game_final.Environments
{
    class Scene
    {
        public static Base.SceneRenderer CurrentScene;
        public static Types.SceneType CurrentSceneType;

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

                CurrentScene.LoadContent();
                CurrentScene.Setup();

                CurrentScene.Ready();
            }
        }
    }
}
