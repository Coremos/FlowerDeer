using UnityEngine;

namespace FlowerDeer.Manager
{

    public class TitleManager : MonoBehaviour
    {
        [SerializeField] GameObject creditsWindow;
        public void OnClickStartButton()
        {
            SceneLoadManager.Instance.LoadScene(SceneType.INTRO);
        }

        public void OnClickSettingsButton()
        {

        }

        public void OnClickCreditsButton()
        {
            creditsWindow.SetActive(true);
        }

        public void OnClickExitButton()
        {
            Application.Quit();
        }
    }

}