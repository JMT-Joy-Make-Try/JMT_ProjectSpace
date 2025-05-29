using AYellowpaper.SerializedCollections;
using DG.Tweening;
using JMT.Agent;
using JMT.Core;
using JMT.Core.Tool.PoolManager;
using JMT.Core.Tool.PoolManager.Core;
using JMT.Item;
using JMT.Planets.Tile;
using JMT.Planets.Tile.Items;
using JMT.UISystem;
using System;
using UnityEngine;

namespace JMT.Object
{
    public class ItemObject : MonoBehaviour, IPoolable, ICollectable
    {
        //[SerializeField] private SerializedDictionary<ItemSO, ItemData> _itemSprites;
        [SerializeField] private SpriteRenderer _itemSpriteRenderer;
        [SerializeField] private float _rotationSpeed;
        [field:SerializeField] public PoolingType type { get; set; }
        public GameObject ObjectPrefab => gameObject;
        
        private MeshRenderer _meshRenderer;
        private ItemSO _itemSO;
        public bool IsCollectable { get; set; } = true;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshRenderer.material = Instantiate(_meshRenderer.material);
        }

        public void SetItemType(ItemSO itemType)
        {
            _itemSO = itemType;
            _itemSpriteRenderer.sprite = itemType.ItemData.Icon;
            _meshRenderer.material.SetColor("_BaseColor", itemType.ItemData.Color);
        }

        private void Update()
        {
            float rotationValue = _rotationSpeed * Time.deltaTime;
            _itemSpriteRenderer.transform.Rotate(Vector3.up, rotationValue);
        }


        public void ResetItem()
        {
            IsCollectable = true;
        }

        public void Collect()
        {
            if (!IsCollectable) return;
            var inventory = AgentManager.Instance.Player.InventoryCompo;
            if (inventory.IsMaxInventorySizeReached())
            {
                Debug.LogWarning("Inventory is full. Cannot collect item: " + _itemSO);;
                return;
            }
            AgentManager.Instance.Player.InventoryCompo.AddItem(_itemSO);
            Debug.Log("Collect Item: " + _itemSO);
            PoolingManager.Instance.Push(this);
        }
    }
    
    [System.Serializable]
    public struct ItemData
    {
        public Sprite Icon;
        [ColorUsage(true, true)]
        public Color32 Color;
    }
}