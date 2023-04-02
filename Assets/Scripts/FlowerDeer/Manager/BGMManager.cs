using FlowerDeer.Utility;
using UnityEngine;

namespace FlowerDeer.Manager
{
    [RequireComponent(typeof(AudioSource))]
    public class BGMManager : Singleton<BGMManager>
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip[] clips;

        private int index;

        protected override void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void FixedUpdate()
        {
            if (audioSource.clip == null)
            {
                PlayNext();
                return;
            }
            if (Mathf.Clamp01(audioSource.time / audioSource.clip.length) == 1.0f)
            {
                PlayNext();
                return;
            }
        }

        private void PlayNext()
        {
            audioSource.clip = clips[index++];
            audioSource.Play();
        }
    }
}