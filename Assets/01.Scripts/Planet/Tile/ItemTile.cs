using JMT.Item;
using JMT.Object;
using JMT.Planets.Tile.Items;
using System;
using UnityEngine;

namespace JMT.Planets.Tile
{
    public class ItemTile : TouchableObject
    {
        [Header("Item Tile")]
        [SerializeField] private ItemSO _itemType;
        [SerializeField] private int _itemCount = 0;
        
        [Header("Touch Count")]
        [SerializeField] private int _touchCount = 0;
        
        private int _currentTileTouchCount = 0;

        private void Awake()
        {
            OnClick += OnTileClickHandler;
        }
        
        private void OnDestroy()
        {
            OnClick -= OnTileClickHandler;
        }

        private void OnTileClickHandler()
        {
            _currentTileTouchCount++;
            if (_currentTileTouchCount >= _touchCount)
            {
                BreakTile();
            }
            Debug.Log($"Tile Clicked! {_touchCount}");
        }
        
        private void BreakTile()
        {
            Debug.Log("Tile Broken!");
            InventoryManager.Instance.AddItem(_itemType, _itemCount);
        }
    }
}