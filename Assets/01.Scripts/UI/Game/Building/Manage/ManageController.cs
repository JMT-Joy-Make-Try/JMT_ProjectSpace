using JMT.Building;
using JMT.Building.Component;
using JMT.Planets.Tile;
using UnityEngine;

namespace JMT.UISystem.Building
{
    public class ManageController : MonoBehaviour, IOpenablePanel
    {
        [SerializeField] private ManageView view;

        public void OpenUI()
        {
            var curBuilding = TileManager.Instance.CurrentTile.CurrentBuilding;
            var workers = curBuilding.GetBuildingComponent<BuildingNPC>()._currentNpc;

            view.OpenUI();
            view.SetWorkers(workers);
            
            if (curBuilding is ItemBuilding itemBuilding)
                view.SetItem(itemBuilding.ItemQueue);
        }

        public void CloseUI()
        {
            view.SetAllWorkersActive(false);
            view.ResetItem();
            view.CloseUI();
        }
    }
}
