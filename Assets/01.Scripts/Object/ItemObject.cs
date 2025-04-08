using AYellowpaper.SerializedCollections;
using DG.Tweening;
using JMT.Core.Tool.PoolManager;
using JMT.Core.Tool.PoolManager.Core;
using JMT.Planets.Tile.Items;
using System;
using UnityEngine;

namespace JMT.Object
{
    public class ItemObject : MonoBehaviour, IPoolable
    {
        [SerializeField] private SerializedDictionary<ItemType, ItemData> _itemSprites;
        [SerializeField] private SpriteRenderer _itemSpriteRenderer;
        [SerializeField] private float _rotationSpeed;
        [field:SerializeField] public PoolingType type { get; set; }
        public GameObject ObjectPrefab => gameObject;
        
        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshRenderer.material = Instantiate(_meshRenderer.material);
        }

        private void Start()
        {
            SetItemType(ItemType.OxygenTank);
        }

        public void SetItemType(ItemType itemType)
        {
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
    }
    
    [System.Serializable]
    public struct ItemData
    {
        public Sprite sprite;
        [ColorUsage(true, true)]
        public Color32 color;
    }
}