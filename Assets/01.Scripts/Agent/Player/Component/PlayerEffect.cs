using AYellowpaper.SerializedCollections;
using JMT.Effect;
using JMT.Planets.Tile.Items;
using UnityEngine;

namespace JMT.PlayerCharacter
{
    public class PlayerEffect : MonoBehaviour, IPlayerComponent
    {
        [SerializeField] private SerializedDictionary<string, EffectPlayer> _effects;
        
        public void Init(IPlayer player)
        {
            
        }
        
        public void PlayEffect(string itemType)
        {
            if (_effects.TryGetValue(itemType, out var effectPlayer))
            {
                effectPlayer.PlayEffect();
            }
            else
            {
                Debug.LogWarning($"Effect for item type {itemType} not found.");
            }
        }
        
        public void StopEffect(string itemType)
        {
            if (_effects.TryGetValue(itemType, out var effectPlayer))
            {
                effectPlayer.StopEffect();
            }
            else
            {
                Debug.LogWarning($"Effect for item type {itemType} not found.");
            }
        }
    }
}