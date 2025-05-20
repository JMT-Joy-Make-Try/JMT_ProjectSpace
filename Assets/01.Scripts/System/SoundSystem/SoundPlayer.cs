using System.Collections.Generic;
using UnityEngine;

namespace JMT.Sound
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private SoundSO soundData;
        [SerializeField] private int audioSourcePoolSize = 10;

        private List<AudioSource> _audioSources;

        private void Awake()
        {
            _audioSources = new List<AudioSource>();
            for (int i = 0; i < audioSourcePoolSize; i++)
            {
                var source = gameObject.AddComponent<AudioSource>();
                _audioSources.Add(source);
            }
        }

        public void PlaySound(string key, SoundType soundType = SoundType.SFX)
        {
            if (!soundData.sounds.TryGetValue(key, out var sound))
            {
                Debug.LogWarning($"Sound with key {key} not found.");
                return;
            }

            var availableSource = GetAvailableAudioSource();

            if (availableSource == null)
            {
                Debug.LogWarning("No available AudioSource.");
                return;
            }

            availableSource.pitch = sound.pitch;
            availableSource.volume = sound.volume;

            if (soundType == SoundType.SFX)
            {
                availableSource.PlayOneShot(sound.audioClip);
            }
            else if (soundType == SoundType.BGM)
            {
                availableSource.loop = true;
                availableSource.clip = sound.audioClip;
                availableSource.Play();
            }
        }

        private AudioSource GetAvailableAudioSource()
        {
            foreach (var source in _audioSources)
            {
                if (!source.isPlaying)
                    return source;
            }

            return null;
        }
    }


    public enum SoundType
    {
        BGM,
        SFX,
    }
}
