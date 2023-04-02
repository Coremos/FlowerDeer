using FlowerDeer.Utility;
using TMPro;
using UnityEngine;

namespace FlowerDeer
{
    public class ScoreIndicator : MonoBehaviour
    {
        public TextMeshProUGUI ScoreText;
        public CustomObjectPool ParentPool;

        public void SetInstance()
        {
            ParentPool.SetInstance(gameObject);
        }
    }
}