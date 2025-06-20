using JMT.Core;
using JMT.Core.Tool;
using JMT.Item;
using JMT.Object;
using JMT.UISystem;
using System;
using UnityEngine;

namespace JMT.PlayerCharacter
{
    public class PlayerInventory : MonoBehaviour, IPlayerComponent
    {
        public event Action<ItemSO, int> OnInventoryEvent;

        public PlayerInventoryData PlayerInventoryData => _playerInventoryData;

        [field: SerializeField] public int MaxInventorySize { get; private set; } = 3;
        [SerializeField] private PlayerInventoryData _playerInventoryData;
        [SerializeField] private LayerMask _whatIsBuilding;
        [SerializeField] private ItemObject _itemObject;
        [SerializeField] private float _itemAddDelay = 1f;
        private Player _player;
        
        private Collider[] _colliders = new Collider[10];
        private bool _isItemAddActive = false;
        private float _currentItemAddTime = 0f;

        public void Init(IPlayer player)
        {
            _itemObject.IsCollectable = false;
            _itemObject.gameObject.SetActive(false);
            _player = player as Player;
            GameUIManager.Instance.InteractCompo.OnClickEvent += SendItem;
        }

        private void OnDestroy()
        {
            if (GameUIManager.Instance == null) return;
            GameUIManager.Instance.InteractCompo.OnClickEvent -= SendItem;
        }

        public void AddItem(ItemSO item, int count = 1)
        {
            _isItemAddActive = false;
            // 현재 들고있는 아이템이 없을 때
            if (_playerInventoryData.item == null)
            {
                // 매개변수로 받아온 아이템을 1개 들고있게 한다
                Debug.Log(count + "개 아이템 받기");
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
            
            Debug.Log(_playerInventoryData.count + "개 아이템");

            // 플레이어가 아이템을 듬
            OnInventoryEvent?.Invoke(item, PlayerInventoryData.count);
            _itemObject.gameObject.SetActive(true);
            _itemObject.SetItemType(item);
            _player.AnimatorCompo.SetBool(PlayerState.Caring, true);
        }
        
        public ItemSO RemoveItem(ItemSO item = null, int count = 1, bool isItemCountZeroNull = true)
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
                    if (isItemCountZeroNull)
                    {
                        _playerInventoryData.item = null;
                    }
                    _playerInventoryData.count = 0;

                    // 플레이어가 아이템을 내려놓음
                    _itemObject.gameObject.SetActive(false);
                    _player.AnimatorCompo.SetBool(PlayerState.Caring, false);
                }
            }
            else
            {
                // 인벤토리에서 아이템을 못찾았을 때(매개변수로 받아온 아이템이 현재 들고있는 아이템과 다를 때)
                Debug.LogWarning("Item not found in inventory.");
            }

            // 이거는 뺀거 뭔지 알려고 넘겨주는거임
            OnInventoryEvent?.Invoke(null, PlayerInventoryData.count);
            return _playerInventoryData.item;
        }
        
        public void ResetItem()
        {
            _playerInventoryData.item = null;
            _playerInventoryData.count = 0;
            _itemObject.gameObject.SetActive(false);
            _player.AnimatorCompo.SetBool(PlayerState.Caring, false);
            OnInventoryEvent?.Invoke(null, PlayerInventoryData.count);
        }
        
        public bool IsMaxInventorySizeReached()
        {
            return _playerInventoryData.count >= MaxInventorySize;
        }
        
        public bool IsPlayerHoldingItem()
        {
            return _playerInventoryData.item != null && _playerInventoryData.count > 0;
        }

        public void SendItem()
        {
            if (_player.TileFindingCompo.RayHit.collider != null)
            {
                var building = _player.TileFindingCompo.RayHit.collider.FindComponent<IItemReceivable>();
                if (building != null && _playerInventoryData.item != null && _playerInventoryData.count > 0)
                {
                    if (building.ReceiveItem(_playerInventoryData.item, _playerInventoryData.count))
                    {
                        RemoveItem(count: _playerInventoryData.count);
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
            OnInventoryEvent?.Invoke(null, _playerInventoryData.count);
        }
    }

    [Serializable]
    public struct PlayerInventoryData
    {
        public ItemSO item;
        public int count;
    }
}