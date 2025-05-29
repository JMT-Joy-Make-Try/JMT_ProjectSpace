using JMT.Item;
using System;
using UnityEngine;

namespace JMT.PlayerCharacter
{
    public class PlayerInventory : MonoBehaviour, IPlayerComponent
    {
        public Player Player { get; private set; }
        
        [SerializeField] private int _maxInventorySize = 3;
        [SerializeField] private PlayerInventoryData _playerInventoryData;
        
        [Header("Debug")]
        [SerializeField] private ItemSO _debugItem;
        [SerializeField] private ItemSO _debugItem2;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                AddItem(_debugItem);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                AddItem(_debugItem2);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                AddItem(_debugItem, -1);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                AddItem(_debugItem2, -1);
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                Debug.Log($"Item: {_playerInventoryData.item?.name}, Count: {_playerInventoryData.count}");
            }
        }

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
                
                if (_playerInventoryData.count <= 0)
                {
                    _playerInventoryData.item = null;
                }
            }
            else
            {
                Debug.LogWarning("Inventory is full or item type mismatch.");
            }
        }
    }

    [Serializable]
    public struct PlayerInventoryData
    {
        public ItemSO item;
        public int count;
    }
}