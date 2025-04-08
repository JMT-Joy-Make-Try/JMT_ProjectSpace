using AYellowpaper.SerializedCollections;
using DG.Tweening;
using JMT.Core;
using JMT.Core.Tool.PoolManager;
using JMT.Core.Tool.PoolManager.Core;
using JMT.Planets.Tile;
using JMT.Planets.Tile.Items;
using System;
using UnityEngine;

namespace JMT.Object
{
    public class ItemObject : MonoBehaviour, IPoolable, ICollectable
    {
        [SerializeField] private SerializedDictionary<ItemType, ItemData> _itemSprites;
        [SerializeField] private SpriteRenderer _itemSpriteRenderer;
        [SerializeField] private float _rotationSpeed;
        [field:SerializeField] public PoolingType type { get; set; }
        public GameObject ObjectPrefab => gameObject;
        
        private MeshRenderer _meshRenderer;
        private ItemType _itemType;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshRenderer.material = Instantiate(_meshRenderer.material);
        }

        public void SetItemType(ItemType itemType)
        {
            _itemType = itemType;
            _itemSpriteRenderer.sprite = _itemSprites[itemType].sprite;
            _meshRenderer.material.SetColor("_BaseColor", _itemSprites[itemType].color);
        }

        private void Update()
        {
            float rotationValue = _rotationSpeed * Time.deltaTime;
            _itemSpriteRenderer.transform.Rotate(Vector3.up, rotationValue);
        }


        public void ResetItem()
        {
        }

        public void Collect()
        {
            InventoryManager.Instance.AddItem(_itemType, 1);
            Debug.Log("Collect Item: " + _itemType);
            PoolingManager.Instance.Push(this);
        }
    }
    
    [System.Serializable]
    public struct ItemData
    {
        public Sprite sprite;
        [ColorUsage(true, true)]
        public Color32 color;
    }
}