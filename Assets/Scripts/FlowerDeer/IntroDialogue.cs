using FlowerDeer.Manager;
using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

namespace FlowerDeer
{
    public class IntroDialogue : MonoBehaviour
    {
        private const float TEXT_COOLDOWN = 0.05f;

        [SerializeField] private TextMeshProUGUI textBox;

        private string text;
        private Coroutine showMessageRoutine;

        private void Awake()
        {
            text = textBox.text;
        }

        public void StartIntro(Action onComplete)
        {
            StartShowingMessage(text, onComplete);
        }

        private void StartShowingMessage(string message, Action onComplete)
        {
            if (showMessageRoutine != null) StopCoroutine(showMessageRoutine);
            showMessageRoutine = StartCoroutine(ShowMessage(message, onComplete));
        }

        private void DisplayMessage(string message)
        {
            textBox.text = message;
            SoundEffectManager.Instance.PlaySound("Tick");
        }

        private IEnumerator ShowMessage(string message, Action onComplete)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int index = 0;
            int length = message.Length;
            float time;

            DisplayMessage("");
            while (index < length)
            {
                time = 0.0f;
                var lastChar = ' ';
                while (time < TEXT_COOLDOWN)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
                stringBuilder.Append(lastChar = message[index++]);
                if (lastChar != ' ')
                {
                    DisplayMessage(stringBuilder.ToString());
                }
                yield return null;
            }
            onComplete?.Invoke();
            yield return null;
        }
    }
}