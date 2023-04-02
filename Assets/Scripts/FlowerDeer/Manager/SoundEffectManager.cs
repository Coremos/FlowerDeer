using FlowerDeer.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace FlowerDeer.Manager
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundEffectManager : Singleton<SoundEffectManager>
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip[] clips;

        private Dictionary<string, AudioClip> clipDictionary;

        protected override void Awake()
        {
            base.Awake();

            InitializeAudioSourceComponent();
            InitializeClipDictionary();
        }

        private void InitializeAudioSourceComponent()
        {
            if (audioSource == null)
            {
                TryGetComponent(out audioSource);
            }
        }

        private void InitializeClipDictionary()
        {
            clipDictionary = new Dictionary<string, AudioClip>();

            for (int index = 0; index < clips.Length; index++)
            {
                clipDictionary.Add(clips[index].name, clips[index]);
            }
        }

        public void PlaySoundOnClick(string name)
        {
            PlaySound(name);
        }

        public void PlaySound(string name, float volume = 1.0f, float pitch = 1.0f)
        {
            if (!clipDictionary.ContainsKey(name)) return;

            audioSource.pitch = pitch;
            audioSource.PlayOneShot(clipDictionary[name], volume);
        }
    }
}