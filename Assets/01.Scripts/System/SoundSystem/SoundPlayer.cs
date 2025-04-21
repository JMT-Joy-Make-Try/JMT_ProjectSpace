using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace JMT.Sound
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private SoundSO soundData;
        
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlaySound(string key, SoundType soundType = SoundType.SFX)
        {
            if (soundData.sounds.TryGetValue(key, out var sound))
            {
                _audioSource.pitch = sound.pitch;
                _audioSource.volume = sound.volume;
                if (soundType == SoundType.SFX)
                {
                    _audioSource.PlayOneShot(sound.audioClip);
                }
                else if (soundType == SoundType.BGM)
                {
                    _audioSource.loop = true;
                    _audioSource.clip = sound.audioClip;
                    _audioSource.Play();
                }
            }
            else
            {
                Debug.LogWarning($"Sound with key {key} not found.");
            }
        }
    }

    public enum SoundType
    {
        BGM,
        SFX,
    }
}
