using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace JMT.Sound
{
    [CreateAssetMenu(fileName = "SoundSO", menuName = "SO/SoundSO")]
    public class SoundSO : ScriptableObject
    {
        public SerializedDictionary<string, SoundData> sounds;
    }
}