using AYellowpaper.SerializedCollections;
using JMT.Core.Tool.PoolManager;
using JMT.Core.Tool.PoolManager.Core;
using JMT.Planets.Tile.Items;
using UnityEngine;

namespace JMT.Object
{
    public class ItemObject : MonoBehaviour, IPoolable
    {
        [SerializeField] private SerializedDictionary<ItemType, Sprite> _itemSprites;
        [SerializeField] private SpriteRenderer _itemSpriteRenderer;
        [SerializeField] private float _rotationSpeed;
        [field:SerializeField] public PoolingType type { get; set; }
        public GameObject ObjectPrefab => gameObject;
        
        public void SetItemType(ItemType itemType)
        {
            _itemSpriteRenderer.sprite = _itemSprites[itemType];
        }

        private void Update()
        {
            float rotationSpeed = _rotationSpeed * Time.deltaTime;
            // if (rotationSpeed > 360)
            // {
            //     rotationSpeed = 0;
            // }
            _itemSpriteRenderer.transform.localRotation = Quaternion.Euler(0, rotationSpeed, 0);
        }


        public void ResetItem()
        {
        }
    }
}