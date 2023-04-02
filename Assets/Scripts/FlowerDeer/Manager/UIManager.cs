using FlowerDeer.Utility;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlowerDeer
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private Image hpImage;
        [SerializeField] private CustomObjectPool scoreIndicatorPool;
        private int lastScore;
        private Coroutine scoreUpdateCoroutine;

        internal void OnChangedScore(int score)
        {
            if (scoreUpdateCoroutine != null) StopCoroutine(scoreUpdateCoroutine);
            scoreUpdateCoroutine = StartCoroutine(UpdateScore(score));
        }

        public void SetHPPercentage(float value)
        {
            hpImage.fillAmount = value;
        }

        internal void ShowScoreIndicator(Vector3 position, int score)
        {
            var indicator = scoreIndicatorPool.GetInstance();
            indicator.TryGetComponent<ScoreIndicator>(out var scoreIndicator);
            indicator.transform.parent = scoreIndicatorPool.transform;
            indicator.transform.position = Camera.main.WorldToScreenPoint(position);
            var frontOperator = (score > 0) ? "+" : "";
            scoreIndicator.ParentPool = scoreIndicatorPool;
            scoreIndicator.ScoreText.text = string.Concat(frontOperator, score.ToString());
        }

        private IEnumerator UpdateScore(int score)
        {
            float lerp = 0.0f;

            while (lastScore != score)
            {
                lerp += Time.deltaTime;
                lastScore = (int)Mathf.Lerp(lastScore, score, lerp);
                scoreText.text = string.Concat("Score = ", lastScore.ToString());
                yield return null;
            }
        }
    }
}