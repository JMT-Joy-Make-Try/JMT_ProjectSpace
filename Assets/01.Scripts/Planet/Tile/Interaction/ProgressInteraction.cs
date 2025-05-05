using JMT.Building;
using JMT.Planets.Tile;
using UnityEngine;

namespace JMT
{
    public class ProgressInteraction : TileInteraction
    {
        public override void Interaction()
        {
            BuildingBase building = GetComponentInChildren<BuildingBase>();
            Debug.Log(building);

            if (!building.IsBuilding) return;
            Debug.Log("완료되었사와요");
            building.OnCompleteEvent?.Invoke();

            TileManager.Instance.CurrentTile.RemoveInteraction();
            if (building is BaseBuilding)
            {
                TileManager.Instance.CurrentTile.AddInteraction<StationInteraction>();
                return;
            }
            if (building is LaboratoryBuilding)
            {
                TileManager.Instance.CurrentTile.AddInteraction<LaboratoryInteraction>();
                return;
            }
            TileManager.Instance.CurrentTile.AddInteraction<BuildingInteraction>();
        }
    }
}
