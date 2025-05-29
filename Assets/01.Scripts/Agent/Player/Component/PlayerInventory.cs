using JMT.Item;
using System;
using UnityEngine;

namespace JMT.PlayerCharacter
{
    public class PlayerInventory : MonoBehaviour, IPlayerComponent
    {
        public Player Player { get; private set; }
        public PlayerInventoryData PlayerInventoryData => _playerInventoryData;
        
        [SerializeField] private int _maxInventorySize = 3;
        [SerializeField] private PlayerInventoryData _playerInventoryData;

        public void Init(Player player)
        {
            Player = player;
        }
        
        public void AddItem(ItemSO item, int count = 1)
        {
            if (_playerInventoryData.item == null)
            {
                _playerInventoryData.item = item;
                _playerInventoryData.count = count;
            }
            else if (_playerInventoryData.item == item)
            {
                _playerInventoryData.count += count;
                _playerInventoryData.count = Mathf.Clamp(_playerInventoryData.count, 0, _maxInventorySize);
            }
            else
            {
                Debug.LogWarning("Inventory is full or item type mismatch.");
            }
        }
        
        public ItemSO RemoveItem(ItemSO item = null, int count = 1)
        {
            if (item == null) item = _playerInventoryData.item;
            if (_playerInventoryData.item == item)
            {
                _playerInventoryData.count -= count;
                if (_playerInventoryData.count <= 0)
                {
                    _playerInventoryData.item = null;
                    _playerInventoryData.count = 0;
                }
            }
            else
            {
                Debug.LogWarning("Item not found in inventory.");
            }
            
            return _playerInventoryData.item;
        }
        
        public bool IsMaxInventorySizeReached()
        {
            return _playerInventoryData.count >= _maxInventorySize;
        }
    }

    [Serializable]
    public struct PlayerInventoryData
    {
        public ItemSO item;
        public int count;
    }
}