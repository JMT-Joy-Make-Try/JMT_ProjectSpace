using JMT.Core;
using JMT.Core.Tool;
using JMT.Item;
using JMT.Object;
using System;
using UnityEngine;

namespace JMT.PlayerCharacter
{
    public class PlayerInventory : MonoBehaviour, IPlayerComponent
    {
        public PlayerInventoryData PlayerInventoryData => _playerInventoryData;
        
        [SerializeField] private int _maxInventorySize = 3;
        [SerializeField] private PlayerInventoryData _playerInventoryData;
        [SerializeField] private LayerMask _whatIsBuilding;
        [SerializeField] private ItemObject _itemObject;
        private Player _player;
        
        private Collider[] _colliders = new Collider[10];

        public void Init(IPlayer player)
        {
            _itemObject.IsCollectable = false;
            _itemObject.gameObject.SetActive(false);
            _player = player as Player;
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
            
            _itemObject.gameObject.SetActive(true);
            _itemObject.SetItemType(item);
            _player.AnimatorCompo.SetBool(PlayerState.Carring, true);
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
                    _itemObject.gameObject.SetActive(false);
                    _player.AnimatorCompo.SetBool(PlayerState.Carring, false);
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

        private void Update()
        {
            int cnt = Physics.OverlapSphereNonAlloc(transform.position, 5f, _colliders, _whatIsBuilding);
            
            for (int i = 0; i < cnt; i++)
            {
                var building = _colliders[i].FindComponent<IItemReceivable>();
                if (building != null)
                {
                    if (_playerInventoryData.item != null && _playerInventoryData.count > 0)
                    {
                        if (building.ReceiveItem(_playerInventoryData.item, _playerInventoryData.count))
                        {
                            RemoveItem(_playerInventoryData.item, _playerInventoryData.count);
                        }
                    }
                }
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