using JMT.Building;
using JMT.Core.Manager;
using JMT.Planets.Tile;
using System.Collections.Generic;
using UnityEngine;

namespace JMT.UISystem.Interact
{
    public class ConstructModel
    {
        private bool isBuild = false;
        public bool IsBuild => isBuild;

        public void SetIsBuild(bool isTrue) => isBuild = isTrue;

        private PVCBuilding pvcObject;

        public List<BuildingDataSO> SelectCategory(BuildingCategory? category = null)
        {
            List<BuildingDataSO> list = BuildingManager.Instance.GetDictionary();
            if (category != null)
                list = CategorySystem.FilteringCategory(list, category, x => x);

            return list;
        }

        public bool Build(PlanetTile tile, PVCBuilding pvcObject)
        {
            if (BuildingManager.Instance.CurrentBuilding == null)
            {
                Debug.Log("읎으요");
                return false;
            }
            if (!GameUIManager.Instance.InventoryCompo.CalculateItem(BuildingManager.Instance.CurrentBuilding.buildingLevel[0].NeedItems)) return false;

            isBuild = true;
            tile.EdgeEnable(false);
            tile.Build(BuildingManager.Instance.CurrentBuilding, pvcObject);
            return true;
        }
    }
}
