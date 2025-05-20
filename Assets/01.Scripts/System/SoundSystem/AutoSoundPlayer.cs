﻿using System;
using UnityEngine;

namespace JMT.Sound
{
    public class AutoSoundPlayer : MonoBehaviour
    {
        [SerializeField] private SoundPlayer _soundPlayer;
        [SerializeField] private string soundKey;
        [SerializeField] private SoundType soundType = SoundType.SFX;
        [SerializeField] private bool _isStartOnAwake = true;

        private void Awake()
        {
            if (_soundPlayer == null)
                _soundPlayer = GetComponent<SoundPlayer>();
            if (_soundPlayer == null)
            {
                Debug.LogError("SoundPlayer component not found on this GameObject.");
            }
        }
        
        private void Start()
        {
            if (!_isStartOnAwake)
                return;
            if (_soundPlayer != null)
            {
                _soundPlayer.PlaySound(soundKey, soundType);
            }
            else
            {
                Debug.LogError("SoundPlayer component is not initialized.");
            }
        }

        public void PlaySound()
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