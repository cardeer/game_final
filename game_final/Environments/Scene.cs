using System;
using System.Collections.Generic;
using System.Text;

namespace game_final.Environments
{
    class Scene
    {
        public static Scenes CurrentScene = Scenes.IN_GAME;
        public enum Scenes
        {
            MENU,
            IN_GAME,
        };
    }
}
