using TMPro;
using UnityEngine;

namespace FlowerDeer.Manager
{
    public class GameOverSceneManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        public void Start()
        {
            int score = 0;
            if (ScoreSyncManager.Instance != null) score = ScoreSyncManager.Instance.Score;
            scoreText.text = string.Concat("Score = ", score.ToString());
        }

        public void OnClickPlayButton()
        {
            SceneLoadManager.Instance.LoadScene(SceneType.GAME);
        }
    }
}