using JMT.Building.Component;
using JMT.Core;
using JMT.Item;
using JMT.Planet.Tile;
using JMT.Planets.Tile;
using JMT.UISystem;
using UnityEngine;

namespace JMT.Building
{
    public class BaseBuilding : BuildingBase, IItemReceivable
    {
        [SerializeField] private Transform visual, brokenVisual;
        [SerializeField] private FactoryBuilding factoryBuilding;
        
        private BuildingBuilder _buildingBuilder;
        private BuildingLevel _buildingLevel;
        
        protected override void Awake()
        {
            base.Awake();
            _buildingBuilder = GetBuildingComponent<BuildingBuilder>();
            _buildingLevel = GetBuildingComponent<BuildingLevel>();
        }

        protected override void AddEvents()
        {
            base.AddEvents();
            _buildingBuilder.OnCompleteEvent += HandleCompleteEvent;
            _buildingLevel.OnLevelChanged += HandleLevelChanged;
        }
        
        protected override void RemoveEvents()
        {
            base.RemoveEvents();
            _buildingBuilder.OnCompleteEvent -= HandleCompleteEvent;
            _buildingLevel.OnLevelChanged -= HandleLevelChanged;
        }

        private void HandleLevelChanged(int level)
        {
            if (level == 1)
            {
                var curTilePos = GetPlanetTile().Position;
                var tile = TileManager.Instance.GetTile(curTilePos.x - 10, curTilePos.y);
                tile.ChangeInteraction<FactoryInteraction>();
                Instantiate(factoryBuilding, tile.TileInteraction.transform);
            }
        }

        private void HandleCompleteEvent()
        {
            FixStation();
            GetBuildingComponent<BuildingAnimator>().SetAnimation(true);
        }


        public void FixStation()
        {
            visual.gameObject.SetActive(true);
            brokenVisual.gameObject.SetActive(false);
        }

        public bool ReceiveItem(ItemSO item, int amount)
        {
            if (!_buildingBuilder.IsBuildingComplete) return false;
            BuildingUIManager.Instance.StorageCompo.AddItem(item, amount);
            return true;
        }
    }
}