using FlowerDeer.Utility;
using UnityEngine;

namespace FlowerDeer.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private RainMaker rainMaker;

        public int Score
        {
            set
            {
                score = value;
                UIManager.Instance.OnChangedScore(score);
            }

            get => score;
        }

        private int score;

        public void GameOver()
        {
            SceneLoadManager.Instance.LoadScene(SceneType.GAMEOVER);
        }
    }
}