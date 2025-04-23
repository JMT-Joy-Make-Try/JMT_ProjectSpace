using JMT.Building.Component;
using JMT.Planets.Tile;
using UnityEngine;

namespace JMT.UISystem.Building
{
    public class ManageController : PanelUI
    {
        [SerializeField] private ManageView view;

        public override void OpenUI()
        {
            base.OpenUI();
            var workers = TileManager.Instance.CurrentTile.CurrentBuilding.GetBuildingComponent<BuildingNPC>()._currentNpc;
            Debug.Log("workers.Count : " + workers.Count);
            view.SetWorkers(workers);
        }

        public override void CloseUI()
        {
            base.CloseUI();
            view.SetAllWorkersActive(false);
        }
    }
}
