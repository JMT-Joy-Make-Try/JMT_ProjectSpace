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
            var building = TileManager.Instance.CurrentTile.CurrentBuilding as ItemBuilding;
            var workers = building.GetBuildingComponent<BuildingNPC>()._currentNpc;

            view.OpenUI();
            view.SetWorkers(workers);
            view.SetItem(building.ItemQueue);
        }

        public void CloseUI()
        {
            view.SetAllWorkersActive(false);
            view.ResetItem();
            view.CloseUI();
        }
    }
}
