using FlowerDeer.Utility;
using UnityEngine;

namespace FlowerDeer.Manager
{
    public class ScoreSyncManager : Singleton<ScoreSyncManager>
    {
        public int Score;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }

        public void Update()
        {
            if (GameManager.Instance != null)
            {
                Score = GameManager.Instance.Score;
            }
        }
    }
}