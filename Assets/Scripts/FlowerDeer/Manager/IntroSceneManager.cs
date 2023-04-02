using UnityEngine;

namespace FlowerDeer.Manager
{
    public class IntroSceneManager : MonoBehaviour
    {
        [SerializeField] IntroDialogue introDialogue;

        private void Start()
        {
            introDialogue.StartIntro(OnCompleteShowingMessage);
        }

        public void OnClickSkipButton()
        {
            SceneLoadManager.Instance.LoadScene(SceneType.GAME);
        }

        private void OnCompleteShowingMessage()
        {
            TimerManager.Instance.StartTimer(5.0f, OnClickSkipButton);
        }
    }
}