using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace JMT.Sound
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private SerializedDictionary<SoundKey, SoundData> soundData = new();
        
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlaySound(SoundKey key, SoundType soundType = SoundType.SFX)
        {
            if (soundData.TryGetValue(key, out var sound))
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

    public enum SoundKey
    {
        
    }
}
