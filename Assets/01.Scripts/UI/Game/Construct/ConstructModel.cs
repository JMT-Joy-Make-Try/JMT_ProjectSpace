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

        public List<BuildingDataSO> SelectCategory(BuildingCategory? category = null)
        {
            List<BuildingDataSO> list = BuildingManager.Instance.GetDictionary();
            if (category != null)
                list = CategorySystem.FilteringCategory(list, category, x => x);

            return list;
        }

        public bool Build(PlanetTile tile)
        {
            if (TileManager.Instance.CurrentTile.IsRocketTile)
            {
                if (BuildingManager.Instance.CurrentBuilding == null)
                {
                    Debug.Log("읎으요");
                    return false;
                }
                if (BuildingManager.Instance.IsCurrentBuildingType(BuildingType.RocketLaunchBuilding))
                {
                    tile.EdgeEnable(false);
                    tile.EnterPreBuildRequirementState();
                    return true;
                }
                else
                {
                    GameUIManager.Instance.PopupCompo.SetActiveAutoPopup("이 타일에는 다른 건물을 건설할 수 없습니다.");
                    return false;
                }
            }
            if (BuildingManager.Instance.CurrentBuilding == null)
            {
                Debug.Log("읎으요");
                return false;
            }
            isBuild = true;
            tile.EdgeEnable(false);
            tile.EnterPreBuildRequirementState();
            return true;
        }
    }
}
