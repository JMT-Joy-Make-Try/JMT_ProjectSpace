using AYellowpaper.SerializedCollections;
using JMT.Agent;
using JMT.Core.Tool;
using JMT.Planets.Tile.Items;
using System;
using UnityEngine;

namespace JMT.PlayerCharacter
{
    public class PlayerStat : MonoBehaviour, IPlayerComponent
    {
        [SerializeField] private SerializedDictionary<ItemType, PlayerStatData> _interactTime;
        private Player _player;
        
        
        public void Init(IPlayer player)
        {
            _player = player as Player;
        }
        
        public void AddStatModifier(ItemType itemType, StatModifier modifier)
        {
            if (_interactTime.TryGetValue(itemType, out var statData))
            {
                statData.AddModifier(modifier);
            }
            else
            {
                Debug.LogWarning($"ItemType {itemType} not found in PlayerStatData.");
            }
        }
        
        public void RemoveStatModifier(ItemType itemType, StatModifier modifier)
        {
            if (_interactTime.TryGetValue(itemType, out var statData))
            {
                statData.RemoveModifier(modifier);
            }
            else
            {
                Debug.LogWarning($"ItemType {itemType} not found in PlayerStatData.");
            }
        }
        
        public float GetInteractTime(ItemType itemType)
        {
            if (_interactTime.TryGetValue(itemType, out var statData))
            {
                return statData.GetValue();
            }
            else
            {
                Debug.LogWarning($"ItemType {itemType} not found in PlayerStatData.");
                return 0f; // or throw an exception based on your design choice
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                AddStatModifier(ItemType.Dust, new StatModifier(StatModifierType.Addition, 1f));
            }
        }
    }
}