using System;
using UnityEngine;

namespace JMT.Sound
{
    [Serializable]
    public class SoundData
    {
        public AudioClip audioClip;
        [Range(0f, 1f)]
        public float volume = 1f;
        [Range(0f, 10f)]
        public float pitch = 1f;
    }
}