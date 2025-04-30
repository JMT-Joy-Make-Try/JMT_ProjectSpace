using UnityEngine;
using AYellowpaper.SerializedCollections;
using EditorAttributes;
using JMT.Agent;
using JMT.Item;
using JMT.Core.Manager;
using JMT.Object;
using JMT.Planets.Tile;
using System;

namespace JMT.Building
{
    public class VillageBuilding : BuildingBase
    {
        [SerializeField] private SerializedDictionary<ItemSO, int> _needItems;
        [SerializeField] private int _npcCount;
        
        [SerializeField] private ItemSO _item;
        
        public SerializedDictionary<ItemSO, int> NeedItems => _needItems;

        private Vector3 _spawnPos;
        private VisibilityTracker _visibilityTracker;

        private bool _isSpawnEnd;

        protected override void Awake()
        {
            base.Awake();
            _visibilityTracker = GetComponentInChildren<VisibilityTracker>();
            _visibilityTracker.OnInvisibleCallback += HandleVisibility;
        }
        
        private void OnDestroy()
        {
            _visibilityTracker.OnInvisibleCallback -= HandleVisibility;
        }

        private void HandleVisibility()
        {
            if (_isSpawnEnd)
            {
                Destroy(this.gameObject);
                Debug.LogError("VillageBuilding destroyed");
            }
        }

        private void Start()
        {
            _spawnPos = BuildingManager.Instance.BaseBuilding.transform.position;
        }
        
        public void GiveItem(ItemSO item, int amount)
        {
            if (!_needItems.ContainsKey(item))
            {
                Debug.LogError($"Item {item} not found in need items.");
                return;
            }
            _needItems[item] -= amount;
            if (_needItems[item] <= 0)
            {
                _needItems.Remove(item);
            }
            if (_needItems.Count <= 0)
            {
                for (int i = 0; i < _npcCount; i++)
                {
                    AgentManager.Instance.AddNpc();
                }
                GetPlanetTile().RemoveInteraction();
                GetPlanetTile().AddInteraction<NoneInteraction>();
                _isSpawnEnd = true;
            }
        }
    }
}