using FlowerDeer.Utility;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace FlowerDeer.Manager
{
    public enum SceneType { TITLE, INTRO, GAME, GAMEOVER }

    public class SceneLoadManager : Singleton<SceneLoadManager>
    {
        private Dictionary<SceneType, string> sceneDictionary = new Dictionary<SceneType, string>()
        {
            { SceneType.TITLE, "Title" },
            { SceneType.INTRO, "Intro" },
            { SceneType.GAME, "Game" },
            { SceneType.GAMEOVER, "GameOver" },
        };

        public void LoadScene(SceneType scene)
        {
            SceneManager.LoadScene(sceneDictionary[scene]);
        }
    }
}