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
        public event Action<int, int> OnInventoryEvent;

        public PlayerInventoryData PlayerInventoryData => _playerInventoryData;

        [field: SerializeField] public int MaxInventorySize { get; private set; } = 3;
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
            // 현재 들고있는 아이템이 없을 때
            if (_playerInventoryData.item == null)
            {
                // 매개변수로 받아온 아이템을 1개 들고있게 한다
                _playerInventoryData.item = item;
                _playerInventoryData.count = count;
            }
            // 현재 들고있는 아이템이 있을 때는 같은 종류만 걸러 count를 증가시킨다
            else if (_playerInventoryData.item == item)
            {
                _playerInventoryData.count += count;
                _playerInventoryData.count = Mathf.Clamp(_playerInventoryData.count, 0, MaxInventorySize);
            }
            // 현재 들고있는 아이템이랑 먹은 아이템이 다르거나 꽉 찼을 때는 못먹게 한다
            else
            {
                Debug.LogWarning("Inventory is full or item type mismatch.");
                return;
            }

            // 플레이어가 아이템을 듬
            OnInventoryEvent?.Invoke(PlayerInventoryData.count, MaxInventorySize);
            _itemObject.gameObject.SetActive(true);
            _itemObject.SetItemType(item);
            _player.AnimatorCompo.SetBool(PlayerState.Carring, true);
        }
        
        public ItemSO RemoveItem(ItemSO item = null, int count = 1)
        {
            // 매개변수로 받아온 아이템이 없으면 현재 들고있는 아이템으로 설정
            if (item == null) item = _playerInventoryData.item;
            // 현재 들고있는 아이템이 매개변수로 받아온 아이템과 같을 때만
            if (_playerInventoryData.item == item)
            {
                // 아이템 개수 빼기
                _playerInventoryData.count -= count;

                // 만약 개수가 0 이하로 떨어지면
                if (_playerInventoryData.count <= 0)
                {
                    // 들고있는 아이템 없애기
                    _playerInventoryData.item = null;
                    _playerInventoryData.count = 0;

                    // 플레이어가 아이템을 내려놓음
                    _itemObject.gameObject.SetActive(false);
                    _player.AnimatorCompo.SetBool(PlayerState.Carring, false);
                }
            }
            else
            {
                // 인벤토리에서 아이템을 못찾았을 때(매개변수로 받아온 아이템이 현재 들고있는 아이템과 다를 때)
                Debug.LogWarning("Item not found in inventory.");
            }

            // 이거는 뺀거 뭔지 알려고 넘겨주는거임
            OnInventoryEvent?.Invoke(PlayerInventoryData.count, MaxInventorySize);
            return _playerInventoryData.item;
        }
        
        public bool IsMaxInventorySizeReached()
        {
            return _playerInventoryData.count >= MaxInventorySize;
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
        
        public void SetMaxInventorySize(int size)
        {
            if (size < 0)
            {
                Debug.LogWarning("Max inventory size cannot be negative.");
                return;
            }
            MaxInventorySize = size;
            _playerInventoryData.count = Mathf.Clamp(_playerInventoryData.count, 0, MaxInventorySize);
            OnInventoryEvent?.Invoke(_playerInventoryData.count, MaxInventorySize);
        }
    }

    [Serializable]
    public struct PlayerInventoryData
    {
        public ItemSO item;
        public int count;
    }
}