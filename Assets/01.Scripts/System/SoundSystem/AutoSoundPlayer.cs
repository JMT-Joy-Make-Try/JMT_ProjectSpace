using System;
using UnityEngine;

namespace JMT.Sound
{
    public class AutoSoundPlayer : MonoBehaviour
    {
        private SoundPlayer _soundPlayer;
        [SerializeField] private string soundKey;
        [SerializeField] private SoundType soundType = SoundType.SFX;

        private void Awake()
        {
            _soundPlayer = GetComponent<SoundPlayer>();
            if (_soundPlayer == null)
            {
                Debug.LogError("SoundPlayer component not found on this GameObject.");
            }
        }
        
        private void Start()
        {
            if (_soundPlayer != null)
            {
                _soundPlayer.PlaySound(soundKey, soundType);
            }
            else
            {
                Debug.LogError("SoundPlayer component is not initialized.");
            }
        }
    }
}